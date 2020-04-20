using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string Music = "";

    public UpdateHighscores scores;

    FMOD.Studio.EventInstance musicInstance;
    private void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(Music);
        musicInstance.start();
        scores.UpdateScores();
    }
    public void PlayGame()
    {
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}