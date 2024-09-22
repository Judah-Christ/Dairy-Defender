using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerTurret : MonoBehaviour
{

    private bool isTurretMounted;
    public Transform targets;
    public GameObject playerTurret;
    private GameObject enemy;
    public float speed;
    private Coroutine shoot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float firingSpeed = 0.5f;
    private float fireTimer;
    private float sighted = 8f;
    public LayerMask raycastMask;
    Scan scansScript;
    
    


    // Start is called before the first frame update
    void Start()
    {
       
        isTurretMounted = false;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        scansScript = GameObject.FindGameObjectWithTag("Turret").GetComponent<Scan>();
    }
   
    private void RotateBasedOnMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle-  90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targets.position.x <= playerTurret.GetComponent<Rigidbody2D>().position.x) {
           isTurretMounted = true;
            

        }

    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (targets.position.x >= playerTurret.GetComponent<Rigidbody2D>().position.x)
        {
            
            scansScript.SpeedStart();
            
        }
    }
    private void Shoot()
    {
      
        Debug.DrawRay(firingPoint.position, firingPoint.up * sighted, Color.green);
        Ray ray = new Ray(firingPoint.position, firingPoint.up);
        RaycastHit2D hit = Physics2D.Raycast(firingPoint.position, firingPoint.up, sighted,raycastMask);
        //print(hit.transform.name);
        if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
        {
            Debug.Log("hitttt");
            Instantiate(bullet, firingPoint.position, firingPoint.rotation);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
       
        if (!isTurretMounted)
        {
            scansScript.SpeedStart();
            Shoot();
            
           
        }
        else if (isTurretMounted)
        {
           RotateBasedOnMouse();
            scansScript.SpeedStop();

        }
    }

}
