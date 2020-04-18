using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public int maxPositions = 20;
    public List<Vector3> positions;
    int positionIndex = 0;
    public float speed = 1.0f;
    public float error = 0.5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentDestination = positions[positionIndex];
        Vector3 direction = (currentDestination - transform.position);
        direction.Normalize();

        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * speed);

        if(Mathf.Abs((currentDestination - transform.position).magnitude) < error)
        {
            positionIndex++;
        }
    }

    public void ResetTrack(List<Vector3> newPoints)
    {
        positionIndex = 0;
        positions.Clear();
        positions = newPoints.GetRange(0, newPoints.Count);
        Debug.Log("Added new positions");
    }
}
