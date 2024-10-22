using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public bool isClimbing = false;
    private SpriteRenderer sr;
    private Collider2D ladderCollider;
    private Transform playerTransform;

    void Start()
    {
        sr = GameObject.Find("SC_Front").GetComponent<SpriteRenderer>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (isClimbing)
        {
            float ladderHeight = ladderCollider.bounds.size.y;
            float currentHeight = playerTransform.position.y - ladderCollider.bounds.min.y;
            float scale = Mathf.Lerp(0.75f, 1f, currentHeight / ladderHeight);
            playerTransform.localScale = new Vector3(scale, scale, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
            GameObject.Find("Player").layer = LayerMask.NameToLayer("OnLadder");
            ladderCollider = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            ladderCollider = collision;

            if (playerTransform.position.y > ladderCollider.bounds.max.y)
            {
                GameObject.Find("Player").layer = LayerMask.NameToLayer("Counter");
                sr.sortingLayerName = "OnCounter";
                sr.sortingOrder = 5;
            }
            else if (transform.position.y < ladderCollider.bounds.min.y)
            {
                GameObject.Find("Player").layer = LayerMask.NameToLayer("Floor");
                sr.sortingLayerName = "OnFloor";
                sr.sortingOrder = 5;
            }

            isClimbing = false;
        }
    }
}
