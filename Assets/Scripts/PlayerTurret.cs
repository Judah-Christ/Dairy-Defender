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

    
    // Start is called before the first frame update
    void Start()
    {
        isTurretMounted = false;
    }
    private void RotateBasedOnMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    private void TargetedSighted()
    {
        if (!isTurretMounted)
        {
            
        }
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
            isTurretMounted = false;
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (isTurretMounted)
        {
            RotateBasedOnMouse();
        }
    }

}
