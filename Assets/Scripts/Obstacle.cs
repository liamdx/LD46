using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public ParticleSystem explosionEffect;
    public Vector3 offset;

    public void DoDestroy()
    {
        explosionEffect.Play();
        this.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // player do damage
            PlayerScript ps = other.gameObject.GetComponent<PlayerScript>();
            ps.Hurt(5);
            DoDestroy();

        }

        if(other.CompareTag("Enemy"))
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.Hurt(5);
            DoDestroy();
        }
    }

}
