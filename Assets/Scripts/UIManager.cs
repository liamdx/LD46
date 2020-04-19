using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    public PlayerScript player;

    private string healthPrefix = "Health : ";

    private string ammoPrefix = "Bullets : ";

    private string scorePrefix = "Score : ";

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText;


    private bool activeLastFrame = false;
    public void LateUpdate()
    {
        if(!activeLastFrame)
        {
            // do ui
            healthText.text = BuildHealthText();
            ammoText.text = BuildAmmoText();
            scoreText.text = BuildScoreText();
        }
        else
        {
            activeLastFrame = false;
        }
    }

    private string BuildHealthText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(healthPrefix);
        sb.Append(player.GetHealth());
        return sb.ToString();
    }

    private string BuildScoreText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(scorePrefix);
        sb.Append(player.GetScore());
        return sb.ToString();
    }

    private string BuildAmmoText()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(ammoPrefix);
        sb.Append(player.shotgun.currentAmmo);
        sb.Append(" / 2");
        return sb.ToString();
    }
}
