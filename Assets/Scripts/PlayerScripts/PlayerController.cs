using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using FMOD.Studio;
using FMODUnity;

public class PlayerController : MonoBehaviour
{
    public PlayerInput PlayerControls;
    private InputAction upDown;
    private InputAction rightLeft;
    private InputAction interact;
    private InputAction shooting;
    private InputAction map;
    private Coroutine shoot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
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
    private float shadowJumpStartY;
    private float shadowPlayerOffset;
    private float timeStart;
    private float timeUpdate;

    public float mouseSensitvity = 100f;
    [SerializeField] CinemachineVirtualCamera Mapcam;


    private Transform targets;
    private PlayerTurret PT;
    private GameObject currentCrossbow;
    private Transform layerSwitchTransform;
    private SpriteRenderer sr;
    public Vector3 origSize = Vector3.one;
    public Vector3 smallSize = new Vector3(0.75f, 0.75f, 0.75f);
    private float sizeChangeSpeed = 1.5f;
    public bool isOnSurface = true;
    private LadderClimb ladderClimb;
    public float climbSpeed = 3f;
    private Collider2D counterCollision;
    bool soundPlayed = false;
    public bool isPaused = false;
    public GameObject pauseMenu;
    public bool upgradeMenuIsOpen = false;
    public GameObject upgradeMenu;
    public GameObject HUD;
    private ZoomIconChange zoomIcon;
    public GameObject UpgButtonBG;
    public GameObject HammerAndWrench;
    public GameObject EToInteract;
    public GameObject Shadow;
    public GameObject CounterShadow;
    private SpriteRenderer counterShadowSprite;
    public GameObject FloorShadow;
    public SpriteRenderer floorShadowSprite;
    //private bool ismap = true;

    [SerializeField] private Vector3 floorOrigShadowScale;
    [SerializeField] private Vector3 floorMinShadowScale;

    private EventInstance playerFootsteps;
    //private EventInstance fallSound;

    [SerializeField] private GameObject fullyUpgraded;
    [SerializeField] private GameObject notEnoughCoins;

    private bool noFall = false;

    [SerializeField] private GameObject fallBoundaries;
    private GameObject bridgeColliders;

    private Bus pauseBus;
    private Bus allBus;


    // Start is called before the first frame update
    void Start()
    {
        PlayerControls.currentActionMap.Enable();
        upDown = PlayerControls.currentActionMap.FindAction("UpDown");
        rightLeft = PlayerControls.currentActionMap.FindAction("RightLeft");
        interact = PlayerControls.currentActionMap.FindAction("Interact");
        shooting = PlayerControls.currentActionMap.FindAction("Shooting");
        map = PlayerControls.currentActionMap.FindAction("Map");

        upDown.started += UpDown_started;
        upDown.canceled += UpDown_canceled;
        rightLeft.started += RightLeft_started;
        rightLeft.canceled += RightLeft_canceled;
        interact.started += Interact_started;
        interact.canceled += Interact_canceled;
        shooting.canceled += Shooting_canceled;
        shooting.started += Shooting_started;
        map.started += Map_started;
        map.canceled += Map_canceled;

        isPlayerMoving = false;
        isPlayerMovingSide = false;
        isShootOnCD = false;
        isPlayerTouching = false;
        isPlayerInteract = false;
        isTurretMounted = false;

        fireTimerOrig = _fireTimer;

        rb = GetComponent<Rigidbody2D>();

        layerSwitchTransform = transform;
        sr = GameObject.Find("SC_Front").GetComponent<SpriteRenderer>();
        ladderClimb = GetComponentInChildren<LadderClimb>();

        zoomIcon = GameObject.Find("ZoomButton").GetComponent<ZoomIconChange>();

        Shadow = CounterShadow;

        shadowPlayerOffset = Vector3.Distance(transform.position, Shadow.transform.position);
        counterShadowSprite = CounterShadow.GetComponent<SpriteRenderer>();
        floorShadowSprite = FloorShadow.GetComponent<SpriteRenderer>();

        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootsteps);
        //fallSound = AudioManager.instance.CreateEventInstance(FMODEvents.instance.Fall);

        fallBoundaries.SetActive(false);
        pauseBus = RuntimeManager.GetBus("bus:/Pause Bus");
        allBus = RuntimeManager.GetBus("bus:/All Bus");

        bridgeColliders = GameObject.Find("BridgeColliders");
        bridgeColliders.SetActive(false);
    }

    private void Shooting_started(InputAction.CallbackContext context)
    {
        Shooting();
    }

    private void Shooting_canceled(InputAction.CallbackContext context)
    {
        //isShootOnCD = false;
    }

    private void Map_started(InputAction.CallbackContext context)
    {
        zoomIcon.zoomClicked();
       // if (ismap)
       // {
        //    Mapcam.enabled = true;
            
      //  }
      //  ismap = false;
        
    }

    private void Map_canceled(InputAction.CallbackContext context)
    {
        
       // if (!ismap)
      //  {
       //     Mapcam.enabled = false;
       //    
      //  }
       // ismap = true;
       
    }

    private void UpDown_canceled(InputAction.CallbackContext context)
    {
        isPlayerMoving = false;
    }

    private void UpDown_started(InputAction.CallbackContext context)
    {
        isPlayerMoving = true;
        if (isOnSurface && !isPaused)
        {
            //AudioManager.instance.PlayPausableSFX("FootstepsF");
        }
    }
    private void RightLeft_canceled(InputAction.CallbackContext context)
    {
        isPlayerMovingSide = false;
    }

    private void RightLeft_started(InputAction.CallbackContext context)
    {
        isPlayerMovingSide = true;
        if (isOnSurface && !isPaused)
        {
            //AudioManager.instance.PlayPausableSFX("FootstepsF");
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
        if (isTurretMounted)
        {
            TurretNotMounted();
            return;
        }
    }
    public void TurretMounted()

    {
        RotateBasedOnMouse();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector3 newpos = new Vector3(targets.position.x, targets.position.y, 0);
        transform.position = newpos;
        currentCrossbow.GetComponent<PlayerTurret>().PlayerSprite.SetActive(true);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void MountTurret()
    {
        if (isPlayerTouching && isPlayerInteract)
        {
            isTurretMounted = true;
            _physCol.enabled = false;
            PT.changeIsMounted();
            counterShadowSprite.enabled = false;
            floorShadowSprite.enabled = false;
        }
    }

    private void TurretNotMounted()
    {

        isTurretMounted = false;
        _physCol.enabled = true;
        PT.changeIsMounted();
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        currentCrossbow.GetComponent<PlayerTurret>().PlayerSprite.SetActive(false);
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        counterShadowSprite.enabled = true;
        floorShadowSprite.enabled = true;

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
            PT.Shoot();
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
            floorShadowSprite.enabled = false;
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
                //AudioManager.instance.PauseSFX();
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

        if (gameObject.layer == LayerMask.NameToLayer("Counter") && bridgeColliders != null)
        {
            counterShadowSprite.sortingLayerName = "OnCounter";
            floorShadowSprite.enabled = false;

            bridgeColliders.SetActive(true);
        }
        else
        {
            if (bridgeColliders != null)
            {
                bridgeColliders.SetActive(false);
            }
        }

        UpdateSound();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isInAir)
        {
            isInAir = true;
            StartCoroutine(Jump());
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    HUD.SetActive(true);
                    Resume();
                }
                else
                {
                    HUD.SetActive(false);
                    Pause();
                }
            }

           // if (Input.GetKeyDown(KeyCode.R))
           // {
              //  if (upgradeMenuIsOpen)
              //  {
                //    CloseUpgradeMenu();
                //    UpgButtonBG.GetComponent<UpgMenuBGChange>().upgradeMenuCloseButtonHandler();
                 //   HammerAndWrench.GetComponent<UpgradeMenuSlide>().CloseButtonHandler();
              //  }
              //  else
               // {
                //    OpenUpgradeMenu();
                 //   UpgButtonBG.GetComponent<UpgMenuBGChange>().upgradeIconClicked();
               //     HammerAndWrench.GetComponent<UpgradeMenuSlide>().UpgradeButtonClick();
               // }
          //  }

            if (Input.GetKeyDown(KeyCode.F))
            {
                zoomIcon.zoomClicked();
            }

            if (!ladderClimb.isClimbing && Shadow != null)
            {
                counterShadowSprite.enabled = true;
            }
        }
    }

    private IEnumerator Jump()
    {
        jumpStartY = transform.position.y;
        shadowJumpStartY = Shadow.transform.position.y;
        rb.velocity = Vector2.up * jumpForce;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Jump, this.transform.position);

        yield return new WaitForFixedUpdate();

        Vector3 shadowPosition = Shadow.transform.position;
        Vector3 origShadowScale = Shadow.transform.localScale;
        Vector3 minShadowScale = origShadowScale * 0.38f;
        StartCoroutine(ScaleShadow(origShadowScale, minShadowScale));

        while (transform.position.y > jumpStartY)
        {
            shadowPosition.x = transform.position.x;
            shadowPosition.y = shadowJumpStartY;
            Shadow.transform.position = shadowPosition;

            if (rb.velocity.y < 0)
            {
                //AudioManager.instance.PlayPausableSFX("FallFromCounterF");
                soundPlayed = true;
            }
            yield return null;
        }

        shadowPosition = transform.position;
        if (gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            shadowPosition.y = transform.position.y - (shadowPlayerOffset * 0.75f);
        }
        else
        {
            shadowPosition.y = transform.position.y - shadowPlayerOffset;
        }
        Shadow.transform.position = shadowPosition;
        Shadow.transform.localScale = origShadowScale;

        if (!isOnSurface && gameObject.layer == LayerMask.NameToLayer("Counter") && !ladderClimb.isClimbing)
        {
            if (!noFall)
            {
                StartCoroutine(Fall());
            }
            else
            {
                isInAir = false;
                isOnSurface = true;
                soundPlayed = false;
            }
        }
        else
        {
            //AudioManager.instance.PauseSFX();
            isInAir = false;
            isOnSurface = true;
            soundPlayed = false;

            if (isPlayerMovingSide)
            {
                //AudioManager.instance.PlaySFX("FootstepsF");
            }
        }
    }

    public IEnumerator ScaleShadow(Vector3 origSize, Vector3 minSize)
    {
        float elapsedTime = 0f;
        float scaleDuration = 1.04f;

        while (elapsedTime < scaleDuration)
        {
            float scaleProgress = elapsedTime / scaleDuration;

            if (scaleProgress < 0.5f)
            {
                Shadow.transform.localScale = Vector3.Lerp(origSize, minSize, scaleProgress * 2);
            }
            else
            {
                Shadow.transform.localScale = Vector3.Lerp(minSize, origSize, (scaleProgress - 0.5f) * 2);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ScaleFloorShadow(Vector3 normalSize, Vector3 minimumSize, float height)
    {

        while (true)
        {
            float distance = Vector3.Distance(Shadow.transform.position, transform.position);
            float scaleFactor = Mathf.InverseLerp(height, 0, distance);

            Shadow.transform.localScale = Vector3.Lerp(minimumSize, normalSize, scaleFactor);
            if (distance <= shadowPlayerOffset)
            {
                break;
            }

            yield return null;
        }

        Shadow.transform.localScale = normalSize;
    }

    public IEnumerator Fall()
    {
        fallBoundaries.SetActive(true);
        floorShadowSprite.enabled = true;
        Shadow.GetComponent<SpriteRenderer>().sortingLayerName = "Non-visible";
        Shadow = FloorShadow;
        Shadow.transform.localScale = floorMinShadowScale;
        Vector3 floorShadowPosition = Shadow.transform.position;
        floorShadowPosition.y = transform.position.y - (4 + (shadowPlayerOffset * 0.75f));
        Shadow.transform.position = floorShadowPosition;

        float maxDistance = Vector3.Distance(Shadow.transform.position, transform.position);
        StartCoroutine(ScaleFloorShadow(floorOrigShadowScale, floorMinShadowScale, maxDistance));

        if (!soundPlayed)
        {
            //AudioManager.instance.PlayOneShot(FMODEvents.instance.Fall, this.transform.position);
        }
        float startY = transform.position.y;

        LayerSwitch();
        StartCoroutine(IgnoreFloorBoundariesCollision());

        while (transform.position.y > startY - 4 && transform.position.y >= -14.8f)
        {
            rb.gravityScale = 1;
            rb.velocity = new Vector2(horz * speed, rb.velocity.y);

            floorShadowPosition.x = transform.position.x;
            Shadow.transform.position = floorShadowPosition;

            yield return null;
        }

        GameObject.Find("FloorBoundaries").layer = LayerMask.NameToLayer("Floor");
        AudioManager.instance.PlayOneShot(FMODEvents.instance.fallThud, this.transform.position);
        //AudioManager.instance.PauseSFX();
        //AudioManager.instance.PlaySFX("FallLandingThudF");
        isInAir = false;
        isOnSurface = true;
        soundPlayed = false;
        floorShadowPosition = transform.position;
        floorShadowPosition.y = transform.position.y - (shadowPlayerOffset * 0.75f);
        Shadow.transform.position = floorShadowPosition;
        fallBoundaries.SetActive(false);
    }

    public IEnumerator IgnoreFloorBoundariesCollision()
    {
        GameObject.Find("FloorBoundaries").layer = LayerMask.NameToLayer("TempIgnore");

        for (int i = 0; i <= 730; i++)
        {
            if (counterCollision != null && counterCollision.CompareTag("CounterMask") && i > 160)
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
            sr.sortingOrder = 1000;
            StartCoroutine(ChangeSize(origSize));
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Floor");
            sr.sortingLayerName = "OnFloor";
            sr.sortingOrder = 1000;
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
            currentCrossbow = collision.gameObject;
            PT = currentCrossbow.GetComponent<PlayerTurret>();
        }

        if (collision.CompareTag("CounterMask"))
        {
            counterCollision = collision;
        }

        if (collision.CompareTag("Interactable"))
        {
            EToInteract.SetActive(true);
        }

        if (collision.CompareTag("BridgeJump"))
        {
            noFall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == ("Turret"))
        {
            isPlayerTouching = false;
            targets = collision.gameObject.transform;
            currentCrossbow = collision.gameObject;
            PT = currentCrossbow.GetComponent<PlayerTurret>();
        }
        if (collision.CompareTag("CounterMask"))
        {
            counterCollision = null;
        }

        if (collision.CompareTag("Interactable") && EToInteract != null)
        {
            EToInteract.SetActive(false);
        }

        if (collision.CompareTag("BridgeJump"))
        {
            noFall = false;
        }
    }

    public void Pause()
    {
        allBus.setPaused(true);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.pauseMenuOpen, this.transform.position);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        //AudioManager.instance.music.Pause();
        isPaused = true;
    }

    public void Resume()
    {
        allBus.setPaused(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        //AudioManager.instance.music.UnPause();
        isPaused = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
        allBus.setPaused(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        //AudioManager.instance.music.UnPause();
        isPaused = false;
    }

    public void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        fullyUpgraded.SetActive(false);
        notEnoughCoins.SetActive(false);
        upgradeMenuIsOpen = true;
    }

    public void CloseUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        upgradeMenuIsOpen = false;
    }

    private void UpdateSound()
    {
        if (isOnSurface && !isInAir && !isPaused && (rb.velocity.x != 0 || rb.velocity.y != 0))
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        else
        {
            playerFootsteps.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        //if (!fallEnded)
        //{
        //    PLAYBACK_STATE fallPlaybackState;
        //    fallSound.getPlaybackState(out fallPlaybackState);
        //    if (fallPlaybackState.Equals(PLAYBACK_STATE.STOPPED))
        //    {
        //        fallSound.start();
        //    }
        //}
        //else
        //{
        //    fallSound.stop(STOP_MODE.IMMEDIATE);
        //}
    }

}