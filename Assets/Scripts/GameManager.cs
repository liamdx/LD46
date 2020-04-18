using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public TileManager tileManager;
    public PathGenerator pathGenerator;
    public PlayerScript player;
    void Awake()
    {
        pathGenerator = GetComponent<PathGenerator>();
        tileManager = GetComponent<TileManager>();
        LinkGameManager();
    }

    public void Start()
    {
        tileManager.gameManager = this;
        pathGenerator.gameManager = this;
        pathGenerator.tileManager = tileManager;

        tileManager.GenTiles(30);
        pathGenerator.GenPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LinkGameManager()
    {
        
    }
}
