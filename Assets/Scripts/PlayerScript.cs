using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public int currentTileIndex;
    public GameManager gameManager;

    public GameObject cameraObject;
    private Camera cam;
    public Cart cart;
    public WeaponSway ws;

    Vector2 leftStick;
    Vector2 rightStick;
    public float lookSensitivity;
    public float horizontalSpeed = 10.0f;

    private void Start()
    {
        cart = GetComponentInParent<Cart>();
    }

    public void Update()
    {
        // camera
        CameraLook();

    
    }

    void Movement()
    {
        
        
    }

    void CameraLook()
    {
        Vector3 cameraAngles = cameraObject.transform.eulerAngles;
        cameraAngles.x -= rightStick.y * lookSensitivity * Time.deltaTime;
        cameraAngles.y += rightStick.x * lookSensitivity * Time.deltaTime;
        cameraObject.transform.eulerAngles = cameraAngles;
    }

    public void OnMove(InputValue value)
    {
        leftStick = value.Get<Vector2>();
        transform.localPosition += new Vector3(leftStick.x, 0, 0);
        
        Debug.Log("Left Stick Values : " + leftStick);
    }

    public void OnLook(InputValue value)
    {
        rightStick = value.Get<Vector2>();
        ws.movementX = rightStick.x;
        ws.movementY = rightStick.y;
        Debug.Log("Right Stick Values : " + rightStick);
    }
}
