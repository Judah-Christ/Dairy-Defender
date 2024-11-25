using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TurretController : MonoBehaviour
{
    private Coroutine shoot;
    [SerializeField] private GameObject bullet;
     private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float firingSpeed = .5f;
    private float fireTimer;
    private float sighted = 4f;
    public LayerMask raycastMask;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        firingPoint = gameObject.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Tracked();
        if (fireTimer <= 0f)
        {
            Shoot();
            fireTimer = firingSpeed;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

    }
   public void Tracked()
    {
        Debug.DrawRay(firingPoint.position, firingPoint.up * sighted, Color.green);
        Ray ray = new Ray(firingPoint.position, firingPoint.up);
       
    }
    private void Shoot()
    {

        RaycastHit2D hit = Physics2D.Raycast(firingPoint.position, firingPoint.up, sighted, raycastMask);
        //print(hit.transform.name);
        if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
        {
            Instantiate(bullet, firingPoint.position, firingPoint.rotation);
            //AudioManager.instance.PlayOneShot(FMODEvents.instance.towerShoot, this.transform.position);
        }
    }
}
