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

    public bool canShoot = true;


    public void LateUpdate()
    {
        if(currentAmmo <= 0)
        {
            Reload();
        }
        if(internalReloadTime > 0.0f)
        {
            internalReloadTime -= Time.deltaTime;
        }

        if(internalSpaceBetweenShots > 0.0f)
        {
            internalSpaceBetweenShots -= Time.deltaTime;   
        }

        if(internalReloadTime <= 0.0f && internalSpaceBetweenShots <= 0.0f)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }
    public void Shoot()
    {
        if (canShoot)
        {
            Debug.Log("Shooting");
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
            currentAmmo -= 1;
            internalSpaceBetweenShots = spaceBetweenShots;
        }

    }

    public void Reload()
    {
        currentAmmo = 2;
        internalReloadTime = reloadTime;
    }

}
