using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    public PlayerScript player;

    public Animator anim;

    public float hitDistance;

    private float internalSpaceBetweenShots = 0.0f;
    public float spaceBetweenShots = 1.0f;

    public bool canShoot = true;

    public Vector3 lastRotation;

    bool animatingLastFrame = false;

    [FMODUnity.EventRef]
    public string ShootEvent = "";

    FMOD.Studio.EventInstance shootInstance;

    public void Start()
    {
        shootInstance = FMODUnity.RuntimeManager.CreateInstance(ShootEvent);
        anim.enabled = false;

    }

    public void LateUpdate()
    {


        if (internalSpaceBetweenShots > 0.0f)
        {
            internalSpaceBetweenShots -= Time.deltaTime;
        }

        if (internalSpaceBetweenShots <= 0.0f)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        if(animatingLastFrame)
        {
            transform.localEulerAngles = lastRotation;
            animatingLastFrame = false;
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            

            Debug.Log("Shooting");
            // bulletEffect.Play();
            shootInstance.start();

            anim.enabled = true;
            anim.SetFloat("attack", Random.value);

            Vector3 p1 = player.cameraObject.transform.position;
            Vector3 p2 = p1 + player.cameraObject.transform.up;
            //if (Physics.Raycast(player.cameraObject.transform.position, player.cameraObject.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            // Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float maxDistance, int layerMask
            RaycastHit[] hits = Physics.SphereCastAll(p1, 3.0f, player.cameraObject.transform.forward, 100000);

            foreach (RaycastHit hit in hits)
            {

                if (hit.collider.gameObject.CompareTag("Obstacle"))
                {
                    // do obstacle
                    Obstacle o = hit.collider.gameObject.GetComponent<Obstacle>();
                    Debug.DrawRay(player.cameraObject.transform.position, player.cameraObject.transform.TransformDirection(Vector3.forward), Color.red, 2.0f);
                    
                    Vector3 dir = player.gameObject.transform.position - o.transform.position;
                    float magnitude = dir.magnitude;

                    if (magnitude <= hitDistance)
                    {

                        if (o != null)
                        {
                            o.DoDestroy();
                        }

                        break;
                    }
                }

                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    // do enemy
                    Skeleton e = hit.collider.gameObject.GetComponent<Skeleton>();

                    Vector3 dir = player.gameObject.transform.position - e.transform.position;
                    float magnitude = dir.magnitude;

                    if (magnitude <= hitDistance)
                    {

                        if (e != null)
                        {
                            e.Hurt(60);
                        }
                        break;
                    }
                }
            }
            internalSpaceBetweenShots = spaceBetweenShots;
        }
    }

    public void DisableAnim()
    {
        anim.SetFloat("attack", -1.0f);
        anim.StopPlayback();
        anim.enabled = false;

        transform.localEulerAngles = lastRotation;
        animatingLastFrame = true;
    }



}
