using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using EasyButtons;
using System.Collections.Generic;
using System.Linq;

public class PathGenerator : MonoBehaviour
{
    public int numPathPoints;

    private PlayerScript player;
    private TileManager tileManager;
    public CinemachineSmoothPath path;
    public CinemachineDollyCart cart;
    public GameManager gameManager;

    public Vector3 cartOffset;

    public float error;


    
    // Start is called before the first frame update
    public void Start()
    {
        player = gameManager.player;
        tileManager = gameManager.tileManager;
    }

    // Update is called once per frame
    public void Update()
    {
                
    }

    [Button]
    public void GenPath()
    {
        path.m_Waypoints = new CinemachineSmoothPath.Waypoint[0];
        tileManager.GetPlayerTileIndex(player);
        if(player.currentTileIndex > tileManager.highestActiveTile)
        {
            tileManager.GenTiles(20);
        }
        
        Debug.Log("Calling Gen Path");
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

        List<Vector3> uniques = ClearDuplicates(eligiblePositions);
        path.m_Waypoints = new CinemachineSmoothPath.Waypoint[uniques.Count];

        for (int i = 0; i < uniques.Count; i++)
        {
            path.m_Waypoints[i].position = uniques[i] + cartOffset;
            path.m_Waypoints[i].roll = 0.0f;
        }
        path.runInEditMode = true;
        eligiblePositions.Clear();
        uniques.Clear();

    }

    List<Vector3> ClearDuplicates(List<Vector3> points)
    {
        List<Vector3> uniques = points.Distinct().ToList();
        List<int> indicesToRemove = new List<int>();
        Vector3 lastPoint = Vector3.zero;

        for(int i = 0; i < uniques.Count; i++)
        {
            // if the distance between the current two points is below the acceptable error
            float distance = (uniques[i] - lastPoint).magnitude;
            if(distance < error)
            {
                indicesToRemove.Add(i);
            }
            lastPoint = uniques[i];
        }

        List<Vector3> removes = new List<Vector3>();
        foreach(int i in indicesToRemove)
        {
            removes.Add(uniques[i]);
        }

        List<Vector3> finals = uniques.Except(removes).ToList();
        return finals;

    }
}
