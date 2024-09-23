using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Coroutine shoot;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float firingSpeed = 0.5f;
    private float fireTimer;
    private float sighted = 4f;
    public LayerMask raycastMask;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
    private void Shoot()
    {

        Debug.DrawRay(firingPoint.position, firingPoint.right * sighted, Color.green);
        Ray ray = new Ray(firingPoint.position, firingPoint.right);
        RaycastHit2D hit = Physics2D.Raycast(firingPoint.position, firingPoint.right, sighted, raycastMask);
        //print(hit.transform.name);
        if (hit.collider != null && hit.collider.gameObject.tag == "Enemy")
        {
            Debug.Log("hitttt");
            Instantiate(bullet, firingPoint.position, firingPoint.rotation);
        }
    }
}
