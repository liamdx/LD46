using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyButtons;
public class EnemyManager : MonoBehaviour
{

    private Pooler pooler;
    public Cart cart;
    public PlayerScript player;
    public TileManager tileManager;
    public int maxActiveEnemies;
    public List<string> enemyTags;

    public List<Enemy> activeEnemies;

    public float spawnFrequency = 20.0f;

    private float internalCounter = 0.0f;

    // Start is called before the first frame update
    public void Awake()
    {
        pooler = GetComponent<Pooler>();
        tileManager = GetComponent<TileManager>();
        activeEnemies = new List<Enemy>();
        internalCounter = 0.0f;
        spawnFrequency = 20.0f;
    }
    private void LateUpdate()
    {
        if(internalCounter >= spawnFrequency)
        {
            SpawnEnemies(5);
            internalCounter = 0.0f;
            spawnFrequency = 10.0f;
        }
        else
        {
            internalCounter += Time.deltaTime;
        }
    }

    [Button]
    public void Spawn10Enemies()
    {
        SpawnEnemies(10);
    }

    public void SpawnEnemies(int amount)
    {
        if (activeEnemies.Count <= maxActiveEnemies)
        {
            List<Vector3> SpawnLocations = GetSpawnLocations(amount);
            int numToSpawn = SpawnLocations.Count > amount ? amount : SpawnLocations.Count;

            for (int i = 0; i < numToSpawn; i++)
            {
                if (activeEnemies.Count <= maxActiveEnemies)
                {
                    int index = (int)Random.value * enemyTags.Count;

                    GameObject go = pooler.SpawnFromPool(enemyTags[index], Vector3.zero, Quaternion.identity);
                    Enemy e = go.GetComponent<Enemy>();

                    activeEnemies.Add(e);
                    int randomIndex = (int)Random.Range(0, SpawnLocations.Count - 1);
                    go.transform.position = SpawnLocations[randomIndex] + e.offset;

                    SpawnLocations.RemoveAt(randomIndex);

                    go.SetActive(true);
                    go.transform.parent = cart.gameObject.transform;
                    go.transform.localEulerAngles = Vector3.zero;

                    e.player = player;
                    e.enemyManager = this;
                }


            }
        }
    }

    private List<Vector3> GetSpawnLocations(int amount)
    {
        List<Vector3> locations = new List<Vector3>();

        List<int> eligibleTileIndices = GetTileIndices();
        List<Tile> eligibleTiles = GetTilesFromIndices(eligibleTileIndices);

        foreach(Tile t in eligibleTiles)
        {
            foreach(Transform trans in t.spawns)
            {
                locations.Add(trans.position);
            }
        }

        return locations;

    }

    private List<int> GetTileIndices()
    {
        List<int> tileIndices = new List<int>();
        tileIndices.Add(player.currentTileIndex);
        for(int i = 0; i < 3; i++)
        {
            tileIndices.Add(player.currentTileIndex + (i + 1));
            tileIndices.Add(player.currentTileIndex - (i + 1));
        }
        
        return tileIndices;
    }

    public List<Tile> GetTilesFromIndices(List<int> indices)
    {
        List<Tile> tiles = new List<Tile>();

        for(int i = 0; i < indices.Count; i++)
        {
            foreach(Tile t in tileManager.activeTiles)
            {
                if (t.index == indices[i])
                {
                    tiles.Add(t);
                }
            }
        }

        return tiles;
    }

    public void RemoveActiveEnemy(Enemy e)
    {
        int indexToRemove = -1;
        for(int i = 0; i < activeEnemies.Count; i++)
        {
            if(activeEnemies[i] == e)
            {
                indexToRemove = i;
            }
        }

        if(indexToRemove >= 0)
        {
            activeEnemies.RemoveAt(indexToRemove);
        }
    }
    
}
