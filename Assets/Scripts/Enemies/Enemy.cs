using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Vector3 offset;
    public float barrierLimit;

    public HealthSystem health;

    public float speed;
    public int damage;

    protected float internalSpeed;

    public PlayerScript player;
    public EnemyManager enemyManager;
    public bool isDead = false;

    

    private void OnEnable()
    {
        isDead = false;
        health.health = 100;
        internalSpeed = speed * Random.Range(0.7f, 0.98f);

    }

    void Awake()
    {
        health = new HealthSystem();
    }

    public void LateUpdate()
    {
        if(health.health <= 0)
        {
            enemyManager.RemoveActiveEnemy(this);
            Death();
        }
    }

    public abstract void Death();

    

    public void DistanceFromAllies()
    {
        foreach(Enemy e in enemyManager.activeEnemies)
        {
            if(e == this)
            {
                continue;
            }
            Vector3 dirToEnemy = e.gameObject.transform.position - transform.position;
            float distanceToEnemy = dirToEnemy.magnitude;
            dirToEnemy.y = 0.0f;

            if (distanceToEnemy < 2.0f) ;
            {
                transform.localPosition -= dirToEnemy.normalized * Time.deltaTime;
            }

        }

       
    }
}
