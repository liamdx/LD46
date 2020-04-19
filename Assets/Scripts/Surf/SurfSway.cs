using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfSway : MonoBehaviour
{
    PlayerScript player;

    public float amount;
    public float maxAmount;
    public float smoothAmount;
    public float rotationMultiplier = 1.0f;
    public bool active;


    private Vector3 initPosition;
    private Quaternion initRotation;

    public float movementX;
    public float movementY;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.localPosition;
        initRotation = transform.localRotation;
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {


            movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
            movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

            Vector3 finalPosition = new Vector3(movementX * amount,0, movementY * amount);
            Quaternion finalRotation = Quaternion.Euler(0.0f, movementX * amount * rotationMultiplier, movementY * amount * rotationMultiplier * 0.25f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, initPosition + finalPosition, Time.deltaTime * smoothAmount);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, initRotation * finalRotation, Time.deltaTime * smoothAmount);
        }

    }
}