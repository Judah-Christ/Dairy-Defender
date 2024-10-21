using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private InputAction shooting;
    private Coroutine shoot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] public float firingSpeed = 0.5f;
    [SerializeField] private Collider2D _physCol;
    [SerializeField] private float _fireTimer;
    private float fireTimerOrig;

    public GameObject player;
    private bool isPlayerMoving;
    private bool isPlayerMovingSide;
    private bool isPlayerTouching;
    private bool isPlayerInteract;
    public bool isTurretMounted;
    private bool isShootOnCD;
    public float speed = 10;
    private float moveDirection;
    public float speedX = 10;

    private float timeStart;
    private float timeUpdate;

    public float mouseSensitvity = 100f;


    private Transform targets;


    // Start is called before the first frame update
    void Start()
    {
        PlayerControls.currentActionMap.Enable();
        upDown = PlayerControls.currentActionMap.FindAction("UpDown");
        rightLeft = PlayerControls.currentActionMap.FindAction("RightLeft");
        interact = PlayerControls.currentActionMap.FindAction("Interact");
        shooting = PlayerControls.currentActionMap.FindAction("SHooting");

        upDown.started += UpDown_started;
        upDown.canceled += UpDown_canceled;
        rightLeft.started += RightLeft_started;
        rightLeft.canceled += RightLeft_canceled;
        interact.started += Interact_started;
        interact.canceled += Interact_canceled;
        shooting.canceled += Shooting_canceled;
        shooting.started += Shooting_started;

        isPlayerMoving = false;
        isPlayerMovingSide = false;
        isShootOnCD = false;
        isPlayerTouching = false;
        isPlayerInteract = false;   
        isTurretMounted = false;

        fireTimerOrig = _fireTimer;
    }

    private void Shooting_started(InputAction.CallbackContext context)
    {
        Shooting();
    }

    private void Shooting_canceled(InputAction.CallbackContext context)
    {
        //isShootOnCD = false;
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
       if (isPlayerTouching && !isTurretMounted)
       {
           MountTurret();
           return;
       }
       if(isTurretMounted)
       {
           TurretNotMounted();
           return;
       }
    }
    public void TurretMounted()

    {
        RotateBasedOnMouse();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector3 newpos = new Vector3(targets.position.x, targets.position.y, +10);
        transform.position = newpos;
    }

    public void MountTurret()
    {
        if (isPlayerTouching && isPlayerInteract)
        {
            isTurretMounted = true;
            _physCol.enabled = false;
        }
    }

    private void TurretNotMounted()
    {
       
        isTurretMounted = false;
        _physCol.enabled = true;
        gameObject.transform.eulerAngles = new Vector3(0,0,0);

    }

    private void RotateBasedOnMouse()
    {
       Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       Vector2 direction = mousePosition - transform.position;
       float angle = Vector2.SignedAngle(Vector2.right, direction);
       transform.eulerAngles = new Vector3(0, 0, angle - 90); 
    }

    private void Shooting()
    {
        if (!isShootOnCD && isTurretMounted)
        {
            Instantiate(bullet, firingPoint.position, firingPoint.rotation);
            isShootOnCD = true;
        }
    }

    private void ShootCD()
    {
        if (isShootOnCD == true && _fireTimer <= 0f)
        {
            _fireTimer = fireTimerOrig;
            isShootOnCD = false;
        }
        else
        {
            _fireTimer -= Time.deltaTime;
        }
    }
       

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isPlayerMoving)
        {
            moveDirection = upDown.ReadValue<float>();
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed * moveDirection);

        }
        else if (isPlayerMovingSide)
        {
            moveDirection = rightLeft.ReadValue<float>();
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-speedX * moveDirection, 0);
           
        }
        else
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (isTurretMounted)
        {
            TurretMounted();
        }

        if (isShootOnCD)
        {
            ShootCD();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == ("Turret"))
        {
            isPlayerTouching = true;
            targets = collision.gameObject.transform;
        }
    }
}
