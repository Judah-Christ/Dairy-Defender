using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    private Rigidbody2D rb;
    //private GameManager GM;
    private float speed;
    private Vector3 pastMoveDirection;
    private Vector3 moveDirection;

    [SerializeField]
    private float _maxSpeed;
    private Animator anim;

    void Start()
    {
        target = FindAnyObjectByType<ObjectiveManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        speed =0.03f;
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
        anim.SetInteger("MoveXInt" , (int)moveDirection.x);
        anim.SetInteger("MoveYInt", (int)moveDirection.y);
    }

    private void GetDistance()
    {
        if (pastMoveDirection != transform.position)
        {
            moveDirection = (pastMoveDirection - transform.position).normalized;
            pastMoveDirection = transform.position;
            AnimationUpdate();
        }
    }
}
