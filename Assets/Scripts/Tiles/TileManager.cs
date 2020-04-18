using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
public class TileManager : MonoBehaviour
{
    public GameManager gameManager;
    private PlayerScript player;
    private Pooler pooler;

    // doesnt exist until we spawn first wave of tiles
    private Tile lastSpawnedTile = null;

    // pooler stuff
    private List<string> tileTags;
    public List<Tile> activeTiles;

    int tileIndex = 1;
    int highestActiveTile = -1;


    private void Awake() {
        pooler = GetComponent<Pooler>();
        activeTiles = new List<Tile>();
    }

    private void Start()
    {
        player = gameManager.player;
    }

    [Button]
    private void Gen10Tiles()
    {
        GenTiles(10);
    }

    private void GenTiles(int numToGen)
    {

        for (int i = 0; i < numToGen; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            // do rotation stuff here if necessary

            if (lastSpawnedTile != null)
            {
                spawnPosition = lastSpawnedTile.end.position;
            }

            GameObject tile = pooler.SpawnFromPool("debug", spawnPosition, Quaternion.Euler(45, 0, 0));
            tile.SetActive(true);
            lastSpawnedTile = tile.GetComponent<Tile>();
            
            if (InActiveTiles(lastSpawnedTile))
            {
                RemoveTileFromActives(lastSpawnedTile);
            }
            lastSpawnedTile.index = tileIndex;

            if(tileIndex > highestActiveTile) { highestActiveTile = tileIndex; }
            tileIndex++;
            tile.transform.position = spawnPosition + lastSpawnedTile.offset;
            tile.transform.eulerAngles = lastSpawnedTile.gameObject.transform.eulerAngles;
            activeTiles.Add(lastSpawnedTile);
        }
    }


    bool InActiveTiles(Tile tile)
    {
        for(int i =0; i < activeTiles.Count; i++)
        {
            Tile t = activeTiles[i];

            if(tile.index == t.index)
            {
                return true;
            }
        }
        return false;
    }

    void RemoveTileFromActives(Tile tile)
    {
        int indexToRemove = -1;

        for (int i = 0; i < activeTiles.Count; i++)
        {
            Tile t = activeTiles[i];

            if (tile.index == t.index)
            {
                indexToRemove = i;
                break;
            }
        }

        if(indexToRemove >= 0)
        {
            activeTiles.RemoveAt(indexToRemove);
        }
    }

    void GetPlayerTileIndex(PlayerScript ps)
    {
        Vector3 psPosition = ps.gameObject.transform.position;

        float bestDistance = 100000.0f;
        int index = -1;

        foreach(Tile t in activeTiles)
        {
            float tileDistance = (t.gameObject.transform.position - psPosition).magnitude;
            if(tileDistance < bestDistance)
            {
                bestDistance = tileDistance;
                index = t.index;
            }
        }

        if (tileIndex >= 0)
        {
            ps.currentTileIndex = index;
        }
    }



}
