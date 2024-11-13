using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    private Rigidbody2D rb;
    //private GameManager GM;
    private float speed;
    private Vector3 moveDirection;

    [SerializeField]
    private float _maxSpeed;
    private Animator anim;

    void Start()
    {
        target = FindAnyObjectByType<ObjectiveManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        speed =0.01f;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (target != null)
        {
            rb.position = Vector3.MoveTowards(rb.position, target.position, speed);
            GetDistance();

        }

        if (rb.velocity.magnitude > _maxSpeed)
        {
            rb.velocity = Vector2.zero;
        }
        
        
    }

    private void AnimationUpdate()
    {
        anim.SetFloat("MoveX" , moveDirection.x);
        anim.SetFloat("MoveY", moveDirection.y);
    }

    private void GetDistance()
    {
        if (target.transform.position != transform.position)
        {
            moveDirection = (target.transform.position - transform.position).normalized;
            AnimationUpdate();
        }
    }

    
}
