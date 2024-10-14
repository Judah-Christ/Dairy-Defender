using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    public bool isClimbing = false;
    private SpriteRenderer sr;
    private Collider2D ladderCollider;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (isClimbing)
        {
            float ladderHeight = ladderCollider.bounds.size.y;
            float currentHeight = transform.position.y - ladderCollider.bounds.min.y;
            float scale = Mathf.Lerp(0.75f, 1f, currentHeight / ladderHeight);
            transform.localScale = new Vector3(scale, scale, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
            gameObject.layer = LayerMask.NameToLayer("OnLadder");
            ladderCollider = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            ladderCollider = collision;

            if (transform.position.y > ladderCollider.bounds.max.y)
            {
                gameObject.layer = LayerMask.NameToLayer("Counter");
                sr.sortingLayerName = "OnCounter";
                sr.sortingOrder = 5;
            }
            else if (transform.position.y < ladderCollider.bounds.min.y)
            {
                gameObject.layer = LayerMask.NameToLayer("Floor");
                sr.sortingLayerName = "OnFloor";
                sr.sortingOrder = 5;
            }

            isClimbing = false;
        }
    }
}
