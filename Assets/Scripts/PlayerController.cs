using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public PlayerInput PlayerControls;
    private InputAction upDown;
    private InputAction rightLeft;
    private InputAction interact;

    public GameObject player;
    private bool isPlayerMoving;
    private bool isPlayerMovingSide;
    private bool isPlayerTouching;
    private bool isPlayerInteract;
    private bool isTurretMounted;
    public float speed = 10;
    private float moveDirection;
    public float speedX = 10;

    private float timeStart;
    private float timeUpdate;

    public Transform targets;

    // Start is called before the first frame update
    void Start()
    {
        PlayerControls.currentActionMap.Enable();
        upDown = PlayerControls.currentActionMap.FindAction("UpDown");
        rightLeft = PlayerControls.currentActionMap.FindAction("RightLeft");
        interact = PlayerControls.currentActionMap.FindAction("Interact");

        upDown.started += UpDown_started;
        upDown.canceled += UpDown_canceled;
        rightLeft.started += RightLeft_started;
        rightLeft.canceled += RightLeft_canceled;
        interact.started += Interact_started;
        interact.canceled += Interact_canceled;

        isPlayerMoving = false;
        isPlayerMovingSide = false;
        isPlayerTouching = false;
        isPlayerInteract = false;   
        isTurretMounted = false;    
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
    private void Interact_canceled(InputAction.CallbackContext context)
    {
       // if (timeStart <= 1000.0f)
      //  {
            isPlayerInteract = false;

       // }
        
    }

    private void Interact_started(InputAction.CallbackContext context)
    {

      // TimerStart();
       // if (timeStart >= 3.0f)
      // {
            isPlayerInteract = true;
            
       // }
    }
    private void TurretMounted()
    {
        if (isPlayerTouching)
        {
            if (isPlayerInteract)
            {
              
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                Vector3 newpos = new Vector3(targets.position.x, targets.position.y, +10);
                transform.position = newpos;
            }
        }
    }
        private void TurretNotMounted()
    {
       
        isTurretMounted = true;

    }
    private void TimerUpdate()
    {
        timeUpdate += Time.deltaTime;
    }
    private void TimerStart()
    {
        timeUpdate += timeStart;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayerTouching = true;
       
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
        if (!isTurretMounted)
        {
           
            TurretMounted();
        }
        else{
            TurretNotMounted();
        }
        TimerUpdate();
    }
}
