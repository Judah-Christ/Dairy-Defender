using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using static UnityEngine.GraphicsBuffer;

public class PlayerTurret : MonoBehaviour
{

    private GameObject player;
    public GameObject PlayerSprite;
    public float speed;
    [SerializeField] private GameObject bullet;
    private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] public float firingSpeed = 0.5f;
    [SerializeField] private float _fireTimer;
    private float fireTimerOrig;
    private float sighted = 8f;
    public LayerMask raycastMask;
    Scan scansScript;
    private PlayerController PC;
    private bool isShootOnCD;
    private bool isPlayerNear;
    [SerializeField] private float _maxRange;
    private bool IsTurretActive;
    public Animator playerTurretAnim;

    public UpgradeLevel TowerLevel;
    public Sprite toolbarImage;
    public int UpgradeCost;
    public int CurrentRotation;
    


    // Start is called before the first frame update
    void Start()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        scansScript = gameObject.GetComponent<Scan>();
        player = GameObject.Find("Player");
        PC = player.GetComponent<PlayerController>();
        fireTimerOrig = _fireTimer;
        firingPoint = gameObject.GetComponentInChildren<Transform>();
        playerTurretAnim = gameObject.GetComponentInParent<Animator>();

    }

    private void RotateBasedOnMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle-  90);
    }

    public void Shoot()
    {
        Debug.DrawRay(firingPoint.position, firingPoint.up * sighted, Color.green);
        Ray ray = new Ray(firingPoint.position, firingPoint.up);
        RaycastHit2D hit = Physics2D.Raycast(firingPoint.position, firingPoint.up, sighted, raycastMask);
        if (hit.collider != null && hit.transform.CompareTag("Enemy") && !IsTurretActive)
        {
            if (isShootOnCD == false)
            {
                playerTurretAnim.SetTrigger("ShootBolt");
                AudioManager.instance.PlayOneShot(FMODEvents.instance.towerShoot, this.transform.position);
                Instantiate(bullet, firingPoint.position, firingPoint.rotation);
                isShootOnCD = true;
                
            }
        }

        if (IsTurretActive == true)
        {
            if (isShootOnCD == false)
            {
                playerTurretAnim.SetTrigger("ShootBolt");
                AudioManager.instance.PlayOneShot(FMODEvents.instance.towerShoot, this.transform.position);
                Instantiate(bullet, firingPoint.position, firingPoint.rotation);
                isShootOnCD = true;

            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!IsTurretActive || !isPlayerNear)
        {
            this.scansScript.SpeedStart();
            Shoot();
        }
        if (IsTurretActive && isPlayerNear)
        {
           RotateBasedOnMouse();
           this.scansScript.SpeedStop();

        }

        if(isShootOnCD)
        {
            ShootCD();
        }

        CheckPlayerDistance();
    }

    private void ShootCD()
    {
        
        if (isShootOnCD == true && _fireTimer <= 0f)
        {
            playerTurretAnim.SetTrigger("ChargeBolt");
            isShootOnCD = false;
            _fireTimer = fireTimerOrig;
        }
        else
        {
            _fireTimer -= Time.deltaTime;
        }
    }

    private void CheckPlayerDistance()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 turretPos = gameObject.transform.position;
        Vector2 distance = playerPos - turretPos;
        if (distance.x <= _maxRange && distance.y <= _maxRange)
        {
            isPlayerNear = true;
        }
        else
        {
            isPlayerNear = false;
        }
    }

    public void changeIsMounted()
    {
        if (IsTurretActive)
        {
            IsTurretActive = false;
            return;
        }
        else
        {
            IsTurretActive = true;
            return;
        }
    }



}
