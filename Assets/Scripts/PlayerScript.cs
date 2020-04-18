using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public int currentTileIndex;
    public GameManager gameManager;


    Vector2 leftStick;
    Vector2 rightStick;
    

    private void OnMove(InputValue value)
    {
        leftStick = value.Get<Vector2>();
        Debug.Log("Left Stick Values : " + leftStick);
    }

    private void OnLook(InputValue value)
    {
        rightStick = value.Get<Vector2>();
        Debug.Log("Right Stick Values : " + rightStick);
    }
}
