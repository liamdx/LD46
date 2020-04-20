using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public void DoDisable()
    {
        this.gameObject.SetActive(false);
    }
}
