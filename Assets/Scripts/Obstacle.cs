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
        hitInstance.start();
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
    }

}
