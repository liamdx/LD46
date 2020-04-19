using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health;

    public void DoDamage(int amount)
    {
        health -= amount;
    }

    public void AddHealth(int amount)
    {
        health += amount;
    }
}
