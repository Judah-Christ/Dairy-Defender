using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlide : MonoBehaviour
{
    public float inPosition = -47.85f;
    public float outPosition = 0f;
    public float distanceFromBottomEdge = 50f;
    public float invSlideSpeed = 5f;

    private RectTransform transform;
    private bool isMouseNearBottom = false;

    void Start()
    {
        transform = GetComponent<RectTransform>();
        Vector2 rest = transform.anchoredPosition;
        rest.y = inPosition;
        transform.anchoredPosition = rest;
    }

    void Update()
    {
        isMouseNearBottom = Input.mousePosition.y <= distanceFromBottomEdge;
        
        Vector2 move = transform.anchoredPosition;

        if (isMouseNearBottom)
        {
            move.y = Mathf.Lerp(move.y, outPosition, Time.deltaTime * invSlideSpeed);
        }
        else
        {
            move.y = Mathf.Lerp(move.y, inPosition, Time.deltaTime * invSlideSpeed);
        }

        transform.anchoredPosition = move;
    }
}
