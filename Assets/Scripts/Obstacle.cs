using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public ParticleSystem explosionEffect;

    public void DoDestroy()
    {
        explosionEffect.Play();
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // player do damage
        }
    }

}
