using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkoff : MonoBehaviour
{
    private PlayerController playerController;
    Rigidbody2D rb;
    private float moveInput;
    private bool isOnFloor = false;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FloorMask") && GameObject.Find("Player").layer == LayerMask.NameToLayer("Counter"))
        {
            playerController.isOnSurface = false;
            
            if(!playerController.isInAir)
            {
                playerController.isInAir = true;
                moveInput = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(moveInput * playerController.speed, rb.velocity.y);
                StartCoroutine(playerController.Fall());
                isOnFloor = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("FloorBoundaries"))
        {
            playerController.isOnSurface = true;
            isOnFloor = false;
        }
    }
}
