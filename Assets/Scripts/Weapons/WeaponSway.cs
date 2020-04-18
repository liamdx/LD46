using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    PlayerScript player;

    public float amount;
    public float maxAmount;
    public float smoothAmount;
    public bool active;

    private Vector3 initPosition;

    public float movementX;
    public float movementY;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.localPosition;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            

            movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
            movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

            Vector3 finalPosition = new Vector3(movementX * amount, movementY * amount, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, initPosition + finalPosition, Time.deltaTime * smoothAmount);
        }

    }
}