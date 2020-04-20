using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNext : MonoBehaviour
{
    public GameObject nextToActivate;

    public void ActivateNextObject()
    {
        nextToActivate.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
