using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform begin;
    public Transform end;
    public Vector3 offset;

    public GameObject spawnContainer;

    public List<Transform> spawns;

    public int index;
    private void Awake()
    {
        index = -1;
    }
    private void Start() {

        foreach(Transform t in spawnContainer.transform)
        {
            spawns.Add(t);
        }
    }
    
}
