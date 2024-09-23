using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerTurret : MonoBehaviour
{

    public Transform targets;
    public GameObject playerTurret;
    private GameObject enemy;
    public float speed;
    private Coroutine shoot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float firingSpeed = 0.5f;
    [SerializeField] private float _fireTimer;
    private float fireTimerOrig;
    private float sighted = 8f;
    public LayerMask raycastMask;
    Scan scansScript;
    private PlayerController PC;
    private bool isShootOnCD;
    
    


    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        scansScript = GameObject.FindGameObjectWithTag("Turret").GetComponent<Scan>();
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        fireTimerOrig = _fireTimer;
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
        if (!PC.isTurretMounted)
        {
            scansScript.SpeedStart();
            Shoot();
        }
        if (PC.isTurretMounted)
        {
           RotateBasedOnMouse();
           scansScript.SpeedStop();

        }

        if(isShootOnCD)
        {
            ShootCD();
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

}
