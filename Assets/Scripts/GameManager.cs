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
    void Awake()
    {
        pathGenerator = GetComponent<PathGenerator>();
        tileManager = GetComponent<TileManager>();
        enemyManager = GetComponent<EnemyManager>();
        uiManager = GetComponent<UIManager>();

    }

    public void Start()
    {
        tileManager.gameManager = this;
        pathGenerator.gameManager = this;
        pathGenerator.tileManager = tileManager;

        tileManager.GenTiles(30);
        pathGenerator.GenPath();
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

}
