using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TileManager tileManager;
    public PathGenerator pathGenerator;
    public PlayerScript player;
    public EnemyManager enemyManager;
    public UIManager uiManager;

    [FMODUnity.EventRef]
    public string MusicEvent = "";
    [FMODUnity.EventRef]
    public string GameOverEvent = "";


    FMOD.Studio.EventInstance music;
    public FMOD.Studio.EventInstance gameOver;

    void Awake()
    {
        music = FMODUnity.RuntimeManager.CreateInstance(MusicEvent);
        gameOver = FMODUnity.RuntimeManager.CreateInstance(GameOverEvent);
        pathGenerator = GetComponent<PathGenerator>();
        tileManager = GetComponent<TileManager>();
        enemyManager = GetComponent<EnemyManager>();
        uiManager = GetComponent<UIManager>();

    }

    public void Start()
    {
        music.start();
        tileManager.gameManager = this;
        pathGenerator.gameManager = this;
        pathGenerator.tileManager = tileManager;

        tileManager.GenTiles(30);
        pathGenerator.GenPath();
    }

    public void GameOver()
    {
        // FMODUnity.RuntimeManager.MuteAllEvents(true);
        music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        gameOver.start();
        Debug.Log("Game Over");
        player.Death();
        uiManager.Die();
    }

}
