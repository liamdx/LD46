using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PathGenerator : MonoBehaviour
{
    public int numPathPoints;

    private PlayerScript player;
    private TileManager tileManager;
    public CinemachineSmoothPath path;
    public CinemachineDollyCart cart;
    public GameManager gameManager;


    
    // Start is called before the first frame update
    void Start()
    {
        path.m_Waypoints = new CinemachineSmoothPath.Waypoint[0];
        player = gameManager.player;
        tileManager = gameManager.tileManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenPath()
    {
        List<Vector3> eligiblePositions = new List<Vector3>();

        for(int i = 0; i < tileManager.activeTiles.Count; i++)
        {
            Tile t = tileManager.activeTiles[i];

            // watch this <= may need to be simply <
            if(t.index <= player.currentTileIndex)
            {
                continue;
            }

            eligiblePositions.Add(t.begin.position);
            eligiblePositions.Add(t.end.position);

            if(eligiblePositions.Count >= numPathPoints)
            {
                break;
            }

        }

        path.m_Waypoints = new CinemachineSmoothPath.Waypoint[eligiblePositions.Count];

        for(int i = 0; i < eligiblePositions.Count; i++)
        {
            path.m_Waypoints[i].position = eligiblePositions[i];
        }
    }
}
