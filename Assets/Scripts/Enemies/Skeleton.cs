using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;

public class Skeleton : Enemy
{
    public SurfSway boardSway;

    private Vector3 randomDirection;

    public GameObject skeletonObject;

    public Animator anim;

    public float attackDistance = 3.0f;

    [FMODUnity.EventRef]
    public string HurtEvent = "";
    [FMODUnity.EventRef]
    public string DeathEvent = "";

    FMOD.Studio.EventInstance hurtInstance;
    FMOD.Studio.EventInstance deathInstance;

    Vector3 targetPoint;

    private void Start()
    {
        boardSway = GetComponentInChildren<SurfSway>();
        randomDirection = GenRandomDirection();
        anim = GetComponentInChildren<Animator>();

        hurtInstance = FMODUnity.RuntimeManager.CreateInstance(HurtEvent);
        deathInstance = FMODUnity.RuntimeManager.CreateInstance(DeathEvent);
    }

    public void Update()
    {
        if(targetPoint == Vector3.zero )
        {
            targetPoint = enemyManager.tileManager.GetRandomPointInFrontOfPlayer();
        }

      
        Vector3 dir = (targetPoint - transform.localPosition);

        if(dir.magnitude < 0.3f) { targetPoint = enemyManager.tileManager.GetRandomPointInFrontOfPlayer(); }

        dir.y = 0;
        dir.Normalize();
        transform.localPosition += dir * Time.deltaTime * internalSpeed;
        boardSway.movementX = dir.x;
        boardSway.movementY = dir.z;


    }
    public override void Death()
    {
        isDead = true;
        // do something
        deathInstance.start();
        Debug.Log("Skeleton Died");
        this.gameObject.SetActive(false);
        player.score.KillSkeleton();
    }

    private Vector3 GenRandomDirection()
    {
        Vector3 dir = new Vector3();

        dir.x = Random.Range(-1.0f, 1.0f) * 3.0f;
        dir.z = Random.Range(-1.0f, 1.0f) * 3.0f;
        dir.y = 0;

        return dir;
    }

    public void Hurt(int amount)
    {
        hurtInstance.start();
        health.health -= amount;
    }

    public void StopAttack()
    {
        anim.SetBool("attack", false);
    }
}
