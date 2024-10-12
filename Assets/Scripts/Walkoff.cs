using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkoff : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("FloorMask"))
        {
            playerController.isGrounded = true;
        }
        else
        {
            playerController.isGrounded = false;
        }
    }
}
