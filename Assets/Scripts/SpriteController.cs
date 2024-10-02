using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    private Vector2 spriteDirection;
    private Rigidbody2D rb2d;
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        spriteDirection = rb2d.velocity;
        anim.SetInteger("H_Movement", ((int)spriteDirection.x));
        anim.SetInteger("V_Movement", ((int)spriteDirection.y));
    }
}
