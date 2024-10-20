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
    [SerializeField] private float firingSpeed = 0.5f;
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
    float horz;
    float vert;
    [SerializeField] float jumpForce;
    Rigidbody2D rb;
    public bool isInAir = false;
    private float jumpStartY;

    private float timeStart;
    private float timeUpdate;

    public float mouseSensitvity = 100f;


    private Transform targets;
    private Transform layerSwitchTransform;
    private SpriteRenderer sr;
    public Vector3 origSize = Vector3.one;
    public Vector3 smallSize = new Vector3(0.75f, 0.75f, 0.75f);
    private float sizeChangeSpeed = 1.5f;
    public bool isOnSurface = true;
    private LadderClimb ladderClimb;
    private float i = 0;
    public float climbSpeed = 3f;
    private Collider2D counterCollision;
    bool soundPlayed = false;

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

        rb = GetComponent<Rigidbody2D>();

        layerSwitchTransform = transform;
        sr = GetComponentInChildren<SpriteRenderer>();
        ladderClimb = GetComponentInChildren<LadderClimb>();
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
        if (isOnSurface)
        {
        AudioManager.instance.PlayPausableSFX("FootstepsF");
        }
    }
    private void RightLeft_canceled(InputAction.CallbackContext context)
    {
        isPlayerMovingSide= false;
    }

    private void RightLeft_started(InputAction.CallbackContext context)
    {
        isPlayerMovingSide = true;
        if (isOnSurface)
        {
        AudioManager.instance.PlayPausableSFX("FootstepsF");
        }
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
        horz = Input.GetAxis("Horizontal");
        vert = Input.GetAxis("Vertical");
        if (isInAir)
        {
            rb.velocity = new Vector2(horz * speed, rb.velocity.y);
            rb.gravityScale = 1f;
        }
        else if (ladderClimb.isClimbing && gameObject.layer == LayerMask.NameToLayer("OnLadder"))
        {
            isInAir = false;
            rb.velocity = new Vector2(rb.velocity.x, vert * climbSpeed);
            rb.gravityScale = 0f;
        }
        else
        {
            rb.velocity = new Vector2(horz * speed, rb.velocity.y);
            rb.gravityScale = 0f;
            
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
                AudioManager.instance.PauseSFX();
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isInAir)
        {
            isInAir = true;
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {   
        jumpStartY = transform.position.y;
        rb.velocity = Vector2.up * jumpForce;
        AudioManager.instance.PauseSFX();
        Debug.Log("Jump Works");

        yield return new WaitForFixedUpdate();

        while (transform.position.y > jumpStartY)
        {   
            if (!soundPlayed && rb.velocity.y < 0)
            {
                AudioManager.instance.PlayPausableSFX("FallFromCounterF");
                soundPlayed = true;
            }
            yield return null;
        }

        if (!isOnSurface && gameObject.layer == LayerMask.NameToLayer("Counter") && !ladderClimb.isClimbing)
        {
            StartCoroutine(Fall());
        }
        else
        {
            AudioManager.instance.PauseSFX();
            isInAir = false;
            isOnSurface = true;
            soundPlayed = false;

            if (isPlayerMovingSide)
            {
                AudioManager.instance.PlaySFX("FootstepsF");
            }
        }
    }

    public IEnumerator Fall()
    {
        if (!soundPlayed)
        {
            AudioManager.instance.PlayPausableSFX("FallFromCounterF");
        }
        float startY = transform.position.y;

        LayerSwitch();
        StartCoroutine(IgnoreFloorBoundariesCollision());

        while (transform.position.y > startY - 3.4 && transform.position.y >= -14.8f)
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(horz * speed, rb.velocity.y);

            yield return null;
        }

        AudioManager.instance.PauseSFX();
        AudioManager.instance.PlaySFX("FallLandingThudF");
        isInAir = false;
        isOnSurface = true;
        soundPlayed = false;
    }

    public IEnumerator IgnoreFloorBoundariesCollision()
    {
        GameObject.Find("FloorBoundaries").layer = LayerMask.NameToLayer("TempIgnore");

        for (int i = 0; i <= 730; i++)
        {
            if (counterCollision != null && counterCollision.CompareTag("CounterMask") && i > 200)
            {
                GameObject.Find("FloorBoundaries").layer = LayerMask.NameToLayer("Floor");
            }

            yield return null;
        }

        GameObject.Find("FloorBoundaries").layer = LayerMask.NameToLayer("Floor");
        isOnSurface = true;
    }

    public void LayerSwitch()
    {
        bool movingToCounter = gameObject.layer == LayerMask.NameToLayer("Floor");

        if (movingToCounter)
        {
            gameObject.layer = LayerMask.NameToLayer("Counter");
            sr.sortingLayerName = "OnCounter";
            sr.sortingOrder = 5;
            StartCoroutine(ChangeSize(origSize));
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Floor");
            sr.sortingLayerName = "OnFloor";
            sr.sortingOrder = 5;
            StartCoroutine(ChangeSize(smallSize));
        }
    }

    public IEnumerator ChangeSize(Vector3 targetSize)
    {
        Vector3 startSize = layerSwitchTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < 1f / sizeChangeSpeed)
        {
            elapsedTime += Time.deltaTime * sizeChangeSpeed;
            layerSwitchTransform.localScale = Vector3.Lerp(startSize, targetSize, elapsedTime);
            yield return null;
        }

        layerSwitchTransform.localScale = targetSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == ("Turret"))
        {
            isPlayerTouching = true;
            targets = collision.gameObject.transform;
        }

        if (collision.CompareTag("CounterMask"))
        {
            counterCollision = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CounterMask"))
        {
            counterCollision = null;
        }
    }
}