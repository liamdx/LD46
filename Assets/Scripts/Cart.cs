using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public int maxPositions = 20;
    public List<Vector3> positions;
    public int positionIndex = 0;
    public float speed = 1.0f;
    public float error = 0.5f;

    public Vector3 offset;

    public float offsetLimit;

    // Update is called once per frame
    void Update()
    {
        OffsetCheck();
        Vector3 currentDestination = positions[positionIndex] + offset;
        Vector3 direction = (currentDestination - transform.position);
        direction.Normalize();

        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * speed);

        if(Mathf.Abs((currentDestination - transform.position).magnitude) < error)
        {
            positionIndex++;
        }
    }


    private void OffsetCheck()
    {
        if (Mathf.Abs(offset.x) > offsetLimit)
        {
            if(offset.x < 0)
            {
                offset.x = -offsetLimit;
            }
            else
            {
                offset.x = offsetLimit;
            }
        }
    }

    public void ResetTrack(List<Vector3> newPoints)
    {
        positions.Clear();
        positions = newPoints.GetRange(0, newPoints.Count);
        positionIndex = 0;
        Debug.Log("Added new positions");
    }
}
