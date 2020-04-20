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
    public TileManager tileManager;
    public Cart cart;
    public GameManager gameManager;

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
        if(cart.positionIndex >= (int) cart.positions.Count * 0.75)
        {
            tileManager.GenTiles(10);
            GenPath();
        }
    }

    [Button]
    public void GenPath()
    {
        // path.m_Waypoints = new CinemachineSmoothPath.Waypoint[0];
        Vector3 currentPosition = cart.transform.position;
        bool hasEmplaced = false;
        tileManager.GetPlayerTileIndex(player);
        
        
        Debug.Log("Calling Gen Path");
        List<Vector3> eligiblePositions = new List<Vector3>();

        for(int i = 0; i < tileManager.activeTiles.Count; i++)
        {
            Tile t = tileManager.activeTiles[i];

            // watch this <= may need to be simply <
            if(t.index < player.currentTileIndex)
            {
                continue;
            }

            if(!hasEmplaced)
            {
                eligiblePositions.Add(t.end.position);
                hasEmplaced = true;
            }
            else
            {
                eligiblePositions.Add(t.begin.position);
                eligiblePositions.Add(t.end.position);
            }

            
            if(eligiblePositions.Count >= numPathPoints)
            {
                break;
            }

        }

        List<Vector3> uniques = ClearDuplicates(eligiblePositions);

        // path.m_Waypoints = new CinemachineSmoothPath.Waypoint[uniques.Count];

        cart.ResetTrack(uniques);
        
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

            else if(uniques[i].z < player.transform.position.z)
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
