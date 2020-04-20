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
    public Cart cart;

    [FMODUnity.EventRef]
    public string MusicEvent = "";
    [FMODUnity.EventRef]
    public string GameOverEvent = "";


    float airThing;

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
        airThing = 1.0f;

    }

    public void LateUpdate()
    {
        if(player.inAir)
        {
            airThing -= Time.deltaTime * 3.0f;
            if(airThing < 0.0f) { airThing = 0.0f; }
            music.setParameterByName("Air", airThing);
        }
        else
        {
            airThing += Time.deltaTime * 3.0f;
            if (airThing > 1.0f) { airThing = 1.0f; }
            music.setParameterByName("Air", airThing);
        }
        if(tileManager.inHell)
        {
            music.setParameterByName("Snow.Hell", 1.0f);
        }
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
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        player.Death();
        gameOver.start();
        uiManager.Die();
        cart.speed = 0.0f;
    }

}
