using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public PlayerScript player;

    public int numBullets = 12;
    public int clipSize = 2;
    public int currentAmmo = 2;

    public ParticleSystem shotEffect;
    public ParticleSystem bulletEffect;

    public int layerMask;

    public float spaceBetweenShots = 0.5f;
    public float reloadTime = 1.0f;

    private float internalSpaceBetweenShots = 0.0f;
    private float internalReloadTime = 0.0f;

    private bool canShoot = false;

    public void LateUpdate()
    {
        if(internalReloadTime > 0.0f)
        {
            internalReloadTime -= Time.deltaTime;
        }

        if(internalSpaceBetweenShots > 0.0f)
        {
            internalSpaceBetweenShots -= Time.deltaTime;   
        }
    }
    public void Shoot()
    {
        if (canShoot)
        {

            shotEffect.Play();
            // bulletEffect.Play();

            RaycastHit hit;

            if (Physics.Raycast(player.cameraObject.transform.position, player.cameraObject.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("Obstacle"))
                {
                    // do obstacle
                    Obstacle o = hit.collider.gameObject.GetComponent<Obstacle>();
                    Debug.DrawRay(player.cameraObject.transform.position, player.cameraObject.transform.TransformDirection(Vector3.forward), Color.red, 2.0f);

                    if (o != null)
                    {
                        o.DoDestroy();
                    }
                }

                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    // do enemy
                }
            }
        }

    }

}
