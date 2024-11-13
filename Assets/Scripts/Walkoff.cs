using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkoff : MonoBehaviour
{
    private PlayerController playerController;
    Rigidbody2D rb;
    private float moveInput;
    private LadderClimb ladderClimb;
    private bool backEdgeSwitch = false;

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
                AudioManager.instance.PauseSFX();
                GameObject.Find("FloorBoundaries").layer = LayerMask.NameToLayer("TempIgnore");
                StartCoroutine(playerController.Fall());

            }
        }

        if (collision.CompareTag("FloorBoundaries") && GameObject.Find("Player").layer == LayerMask.NameToLayer("Floor") && !playerController.isInAir)
        {
            playerController.isOnSurface = true;
        }

        if (collision.CompareTag("CounterMask") && !playerController.isOnSurface && !backEdgeSwitch)
        {
            playerController.isOnSurface = true;
        }

        if (collision.CompareTag("BackEdge") && GameObject.Find("Player").layer == LayerMask.NameToLayer("Counter"))
        {
            backEdgeSwitch = true;

            if (!playerController.isInAir)
            {
                playerController.isOnSurface = false;
                playerController.isInAir = true;
                moveInput = Input.GetAxis("Horizontal");
                rb.velocity = Vector2.zero;
                rb.velocity = new Vector2(moveInput * playerController.speed, rb.velocity.y);
                GameObject.Find("FloorBoundaries").layer = LayerMask.NameToLayer("TempIgnore");
                StartCoroutine(playerController.Fall());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BackEdge"))
        {
            backEdgeSwitch = false;
        }
    }
}