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
    public TileManager tileManager;
    public Shotgun shotgun;
    public Scythe scythe;
    public KickbackEffect kickback;
    public GameObject cameraObject;
    private Camera cam;
    public Cart cart;


    public WeaponSway shotgunws;
    public WeaponSway scythews;
    public SurfSway surfboardws;
    private HealthSystem health;
    public ScoreSystem score;

    public float offsetLimit = 15;
    Vector2 leftStick;
    Vector2 rightStick;
    public float lookSensitivity;
    public float horizontalSpeed = 10.0f;
    public float verticalSpeed = 5.0f;

    // ramp shit
    public bool inAir;
    public float speedMag;
    private bool dead;

    private Vector3 velocity;
    private float xAxisClamp = 0.0f;


    private float scoreCounter;


    [FMODUnity.EventRef]
    public string HurtEvent = "";

    [FMODUnity.EventRef]
    public string SurfEvent = "";

    FMOD.Studio.EventInstance hurtInstance;
    FMOD.Studio.EventInstance surfInstance;


    private Vector3 speed;
    private Vector3 lastPosition;

    private void Start()
    {
        health = new HealthSystem();
        health.health = 100;
        score = new ScoreSystem();
        inAir = false;
        cart = GetComponentInParent<Cart>();
        cam = cameraObject.GetComponent<Camera>();
        dead = false;
        hurtInstance = FMODUnity.RuntimeManager.CreateInstance(HurtEvent);
        surfInstance = FMODUnity.RuntimeManager.CreateInstance(SurfEvent);
        surfInstance.start();
    }

    public void Update()
    {
        if (!dead)
        {
            // camera
            CameraLook();
            Movement();
        }

        if (inAir)
        {
            Air();
        }
        
        if(health.health <= 0)
        {
            gameManager.GameOver();
        }

        if (scoreCounter >= 1.0f)
        {
            /// add score
            score.AddScore(10);
            scoreCounter = 0.0f;
        }
        else
        {
            scoreCounter += Time.deltaTime;
        }
    }

    public void Death()
    {
        dead = true;
        surfInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }

    public int GetHealth()
    {
        return health.health;
    }

    public int GetScore()
    {
        return score.score;
    }

    public void Movement()
    {
        var currentPosition = transform.localPosition;

        Vector3 camRight = cam.transform.right;
        camRight = camRight * leftStick.x * Time.deltaTime * horizontalSpeed;
        Vector3 camForward = cam.transform.forward;
        camForward = camForward * leftStick.y * Time.deltaTime * verticalSpeed ;
        currentPosition += new Vector3(camRight.x, 0, camForward.z);

        Tile t = tileManager.GetTile(currentTileIndex);

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

        speed = transform.position - lastPosition;
        speedMag = speed.magnitude;


        lastPosition = transform.position;
    }

    void CameraLook()
    {
        Vector3 cameraAngles = cameraObject.transform.localEulerAngles;

        

        float rotX = rightStick.x * lookSensitivity * Time.deltaTime;
        float rotY = rightStick.y * lookSensitivity * Time.deltaTime;

        xAxisClamp -= rotY;
        
        cameraAngles.y += rotX;
        cameraAngles.x -= rotY;

        if(xAxisClamp > 90)
        {
            xAxisClamp = 90.0f;
            cameraAngles.x = 90.0f;
        }else if(xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            cameraAngles.x = -90.0f;
        }

        cameraObject.transform.localRotation = Quaternion.Euler(cameraAngles);
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
        surfboardws.movementX = leftStick.x;
        surfboardws.movementY = leftStick.y;
    }

    public void OnLook(InputValue value)
    {
        rightStick = value.Get<Vector2>();
        shotgunws.movementX = rightStick.x;
        shotgunws.movementY = rightStick.y;
        scythews.movementX = rightStick.x;
        scythews.movementY = rightStick.y;
    }

    public void OnShoot()
    {
        shotgun.Shoot();
        kickback.DoKickback();
    }

    public void OnMelee()
    {
        scythe.Shoot();
    }

    public void OnRamp()
    {
        velocity = Vector3.zero;
        Debug.Log("Starting Ramp, Y Height : " + transform.localPosition.y);
        velocity += rampVelocity;
        inAir = true;
    }

    public void Hurt(int amount)
    {
        hurtInstance.start();
        health.DoDamage(amount);
    }    
}
