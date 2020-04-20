using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public ParticleSystem explosionEffect;
    public Vector3 offset;
    [FMODUnity.EventRef]
    public string HitEvent = "";

    FMOD.Studio.EventInstance hitInstance;

    private void Awake()
    {
        hitInstance = FMODUnity.RuntimeManager.CreateInstance(HitEvent);
    }

    public void DoDestroy()
    {
        explosionEffect.gameObject.transform.parent = null;
        explosionEffect.Play();
        this.gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // player do damage
            hitInstance.start();
            PlayerScript ps = other.gameObject.GetComponent<PlayerScript>();
            ps.Hurt(5);
            DoDestroy();

        }

        if (other.CompareTag("Enemy"))
        {
            // player do damage
            Skeleton ps = other.gameObject.GetComponent<Skeleton>();
            ps.HurtNoSound(5);
            DoDestroy();

        }
    }

}
