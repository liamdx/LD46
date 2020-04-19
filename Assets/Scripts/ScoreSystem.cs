using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private PlayerScript player;

    public int SkeletonScore;
    public int BatScore;

    public int score;

    private float airModifier = 1.0f;

    private void Start()
    {
        player = GetComponent<PlayerScript>();
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void KillSkeleton()
    {
        SortAirModifier();
        score += SkeletonScore;
    }

    public void KillBat()
    {
        SortAirModifier();
        score += BatScore;
    }

    public void AddScore(int amount)
    {
        score += amount;
    }


    void SortAirModifier()
    {
        if (player.inAir)
        {
            airModifier = 1.5f;
        }
        else
        {
            airModifier = 1.0f;
        }
    }
}
