using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkoff : MonoBehaviour
{
    private PlayerController playerController;
    Rigidbody2D rb;
    private float moveInput;
    private LadderClimb ladderClimb;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        rb = GetComponentInParent<Rigidbody2D>();
        ladderClimb = GetComponent<LadderClimb>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FloorMask") && GameObject.Find("Player").layer == LayerMask.NameToLayer("Counter") && !ladderClimb.isClimbing)
        {
            playerController.isOnSurface = false;
            
            if(!playerController.isInAir)
            {
                playerController.isInAir = true;
                moveInput = Input.GetAxis("Horizontal");
                rb.velocity = new Vector2(moveInput * playerController.speed, rb.velocity.y);
                StartCoroutine(playerController.Fall());
            }
        }

        if (collision.CompareTag("FloorBoundaries") && GameObject.Find("Player").layer == LayerMask.NameToLayer("Floor"))
        {
            playerController.isOnSurface = true;
        }

        if (collision.CompareTag("CounterMask") && !playerController.isOnSurface)
        {
            playerController.isOnSurface = true;
        }
    }
}
