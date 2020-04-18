using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    public Vector3 gravity;
    public Vector3 rampVelocity;

    public int currentTileIndex;
    public GameManager gameManager;
    public Shotgun shotgun;
    public KickbackEffect kickback;
    public GameObject cameraObject;
    private Camera cam;
    public Cart cart;
    public WeaponSway ws;
    public float offsetLimit = 15;
    Vector2 leftStick;
    Vector2 rightStick;
    public float lookSensitivity;
    public float horizontalSpeed = 10.0f;

    // ramp shit
    private bool inAir;
    private Vector3 velocity;

    private void Start()
    {
        inAir = false;
        cart = GetComponentInParent<Cart>();
        cam = cameraObject.GetComponent<Camera>();
    }

    public void Update()
    {
        // camera
        CameraLook();
        Movement();

        if(inAir)
        {
            Air();
        }

    }

    public void Movement()
    {
        var currentPosition = transform.localPosition;

        Vector3 camRight = cam.transform.right;
        Debug.Log("Cam Right = " + camRight);
        camRight = camRight * leftStick.x * Time.deltaTime * horizontalSpeed;
        currentPosition += new Vector3(camRight.x, 0, 0);

        if (Mathf.Abs(currentPosition.x) > offsetLimit)
        {
            if (currentPosition.x < 0)
            {
                currentPosition.x = -offsetLimit;
            }
            else
            {
                currentPosition.x = offsetLimit;
            }

        }
        transform.localPosition = currentPosition;
    }

    void CameraLook()
    {
        Vector3 cameraAngles = cameraObject.transform.eulerAngles;
        cameraAngles.x -= rightStick.y * lookSensitivity * Time.deltaTime;
        cameraAngles.y += rightStick.x * lookSensitivity * Time.deltaTime;
        cameraObject.transform.eulerAngles = cameraAngles;
    }

    void Air()
    {
        transform.localPosition += (velocity * Time.deltaTime);
        velocity += gravity * Time.deltaTime;

        if(transform.localPosition.y <= 0.0f)
        {
            inAir = false;
        }
    }

    public void OnMove(InputValue value)
    {
        leftStick = value.Get<Vector2>();    
        
        Debug.Log("Left Stick Values : " + leftStick);
    }

    public void OnLook(InputValue value)
    {
        rightStick = value.Get<Vector2>();
        ws.movementX = rightStick.x;
        ws.movementY = rightStick.y;
        Debug.Log("Right Stick Values : " + rightStick);
    }

    public void OnShoot()
    {
        shotgun.Shoot();
        kickback.DoKickback();
    }

    public void OnRamp()
    {
        velocity = Vector3.zero;
        Debug.Log("Starting Ramp, Y Height : " + transform.localPosition.y);
        velocity += rampVelocity;
        inAir = true;
    }
}
