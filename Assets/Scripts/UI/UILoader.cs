using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{

    public GameManager gm;

    public void LoadMainMenu()
    {
        gm.gameOver.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene(0);
    }
}
