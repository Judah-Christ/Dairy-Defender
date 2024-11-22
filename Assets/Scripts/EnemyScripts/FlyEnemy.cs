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
    private int knockBackX;

    void Start()
    {
        target = FindAnyObjectByType<ObjectiveManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        speed =0.05f;
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

    public void StopMovement()
    {
        anim.SetTrigger("Death");
        speed = 0f;
        target = gameObject.transform;
        BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
        collider.enabled = false;
    }

    public void CollisionDirection(Vector2 direction)
    {
        anim.SetInteger("KnockBackX", ((int)direction.x));
    }

    public void TriggerKnockback()
    {
        anim.SetTrigger("KnockbackAnim");
    }

}
