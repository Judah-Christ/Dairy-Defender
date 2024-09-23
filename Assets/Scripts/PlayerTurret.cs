using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerTurret : MonoBehaviour
{

    //public Transform targets;
    //public GameObject playerTurret;
    //private GameObject enemy;
    private GameObject player;
    public float speed;
    //private Coroutine shoot;
    [SerializeField] private GameObject bullet;
    private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float firingSpeed = 0.5f;
    [SerializeField] private float _fireTimer;
    private float fireTimerOrig;
    private float sighted = 8f;
    public LayerMask raycastMask;
    Scan scansScript;
    private PlayerController PC;
    private bool isShootOnCD;
    private bool isPlayerNear;
    private float _maxRange;

    
    


    // Start is called before the first frame update
    void Start()
    {
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        scansScript = gameObject.GetComponent<Scan>();
        player = GameObject.Find("Player");
        PC = player.GetComponent<PlayerController>();
        fireTimerOrig = _fireTimer;
        firingPoint = gameObject.GetComponentInChildren<Transform>();

    }
   
    private void RotateBasedOnMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle-  90);
    }

    private void Shoot()
    {
        Debug.DrawRay(firingPoint.position, firingPoint.up * sighted, Color.green);
        Ray ray = new Ray(firingPoint.position, firingPoint.up);
        RaycastHit2D hit = Physics2D.Raycast(firingPoint.position, firingPoint.up, sighted,raycastMask);
        if (hit.collider != null && hit.transform.tag == "Enemy")
        {
            if (isShootOnCD == false)
            {
                Instantiate(bullet, firingPoint.position, firingPoint.rotation);
                isShootOnCD = true;
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!PC.isTurretMounted || !isPlayerNear)
        {
            this.scansScript.SpeedStart();
            Shoot();
        }
        if (PC.isTurretMounted && isPlayerNear)
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
            _fireTimer = fireTimerOrig;
            isShootOnCD = false;
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

}
