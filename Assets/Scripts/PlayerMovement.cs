using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput PlayerControls;
    private InputAction upDown;
    private InputAction rightLeft;

    public GameObject player;
    private bool isPlayerMoving;
    private bool isPlayerMovingSide;
    public float speed = 10;
    private float moveDirection;
    public float speedX = 10;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControls.currentActionMap.Enable();
        upDown = PlayerControls.currentActionMap.FindAction("UpDown");
        rightLeft = PlayerControls.currentActionMap.FindAction("RightLeft");

        upDown.started += UpDown_started;
        upDown.canceled += UpDown_canceled;
        rightLeft.started += RightLeft_started;
        rightLeft.canceled += RightLeft_canceled;

        isPlayerMoving = false;
    }

    private void UpDown_canceled(InputAction.CallbackContext context)
    {
        isPlayerMoving = false;
    }

    private void UpDown_started(InputAction.CallbackContext context)
    {
        isPlayerMoving = true;
    }
    private void RightLeft_canceled(InputAction.CallbackContext context)
    {
        isPlayerMovingSide= false;
    }

    private void RightLeft_started(InputAction.CallbackContext context)
    {
        isPlayerMovingSide = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isPlayerMoving)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed * moveDirection);
        }
        else if (isPlayerMovingSide)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX * moveDirection, 0);
        }else
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }

    void Update()
    {
        if (isPlayerMoving)
        {
            moveDirection = upDown.ReadValue<float>();
        }
       else if (isPlayerMovingSide)
        {
            moveDirection = rightLeft.ReadValue<float>();
        }
    }
}
