using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
public class TileManager : MonoBehaviour
{
    public GameManager gameManager;
    public PlayerScript player;
    private Pooler pooler;
    public EnemyManager enemyManager;
    public ParticleSystem snow;

    // doesnt exist until we spawn first wave of tiles
    private Tile lastSpawnedTile = null;

    // pooler stuff
    public List<string> mountainTileTags;
    public List<string> hellTileTags;
    public List<string> obstacleTags;
    public List<string> rampTags;
    public List<Tile> activeTiles;

    int tileIndex = 1;
   public  int highestActiveTile = -1;
    private Vector3 initialPosition;

    public float hellTimer = 30.0f;

    public bool inHell;
    public void Awake() {
        initialPosition = transform.position;
        pooler = GetComponent<Pooler>();
        activeTiles = new List<Tile>();
        enemyManager = null;
    }

    public void Start()
    {
        player = gameManager.player;
        
    }


    [Button]
    public void Gen10Tiles()
    {
        GenTiles(10);
    }

    private void LateUpdate()
    {
        GetPlayerTileIndex(player);
        while (player.currentTileIndex > (int)highestActiveTile * 0.75)
        {
            GenTiles(1);
        }

        if (hellTimer > 0.0f)
        {
            inHell = false;
            hellTimer -= Time.deltaTime;
        }
        else
        {
            inHell = true;
            snow.Stop();
            
        }

    }


    public void GenTiles(int numToGen)
    {
        List<string> tileTags;
        if (inHell)
        {
            tileTags = hellTileTags;
        }
        else
        {
            tileTags = mountainTileTags;
        }
        
        if(enemyManager == null)
        {
            enemyManager = GetComponent<EnemyManager>();
        }

        for (int i = 0; i < numToGen; i++)
        {
            Vector3 spawnPosition = initialPosition;
            // do rotation stuff here if necessary

            if (lastSpawnedTile != null)
            {
                spawnPosition = lastSpawnedTile.end.position; 
            }

            int randomIndex = (int)Random.Range(0, tileTags.Count);

            GameObject tile = pooler.SpawnFromPool(tileTags[randomIndex], spawnPosition, Quaternion.Euler(45, 0, 0));
            tile.SetActive(true);
            lastSpawnedTile = tile.GetComponent<Tile>();
            SpawnObstacles(lastSpawnedTile);
            SpawnRamps(lastSpawnedTile);
            
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

    private void SpawnObstacles(Tile t)
    {
        for (int i = 0; i < t.spawns.Count; i++) 
        {
            float chance = Random.Range(0.0f, 1.0f);
            if(chance > 0.66f)
            {
                Transform trans = t.spawns[i];
                int randomIndex = (int)Random.Range(0, obstacleTags.Count);
                GameObject go = pooler.SpawnFromPool(obstacleTags[randomIndex], trans.position, trans.rotation);

                Obstacle obstacle = go.GetComponent<Obstacle>();
                go.SetActive(true);
                go.transform.position = trans.position + obstacle.offset;
                go.transform.localEulerAngles += new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f);

            }
        }
    }

    private void SpawnRamps(Tile t)
    {
        for (int i = 0; i < t.spawns.Count; i++)
        {
            float chance = Random.Range(0.0f, 1.0f);
            if (chance > 0.75f)
            {
                Transform trans = t.spawns[i];
                int randomIndex = (int)Random.Range(0, rampTags.Count);
                GameObject go = pooler.SpawnFromPool(rampTags[randomIndex], trans.position, trans.rotation);

                Ramp ramp = go.GetComponent<Ramp>();
                go.SetActive(true);
                go.transform.position = trans.position + ramp.offset;
                break;

            }
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

    public void GetPlayerTileIndex(PlayerScript ps)
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

    public Vector3 GetRandomPointInFrontOfPlayer()
    {
        int index = player.currentTileIndex;

        

        foreach(Tile tile in activeTiles)
        {
            if (tile.index == index)
            {
                Tile t = tile;
                int randomIndex = (int)Random.Range(0, t.spawns.Count);
                return t.spawns[randomIndex].position;

            }
        }

        return Vector3.zero;
        
    }

    public Tile GetTile(int index)
    {
        foreach(Tile t in activeTiles)
        {
            if(t.index == index)
            {
                return t;
            }
        }

        return null;
    }



}
