using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClearHighScores : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(ClearScores);
    }

    public void ClearScores()
    {
        PlayerPrefs.DeleteAll();
    }
}
