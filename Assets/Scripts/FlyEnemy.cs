using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    private Rigidbody2D rb;
    //private GameManager GM;
    private float speed;
    private Vector2 moveDirection;

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
            moveDirection = new Vector2 (rb.velocity.x, rb.velocity.y);
            AnimationUpdate();
        }


    }

    private void AnimationUpdate()
    {
        anim.SetInteger("MoveX" , ((int)(moveDirection.x)));
        anim.SetInteger("MoveY", ((int)(moveDirection.y)));
    }
}
