using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlide : MonoBehaviour
{
    public float inPosition = -47.85f;
    public float outPosition = 0f;
    public float distanceFromBottomEdge = 50f;
    public float invSlideSpeed = 5f;

    private RectTransform rectTransform;
    private bool isMouseNearBottom = false;
    private bool isMouseAboveInv = false;

    private PlayerController playerController;
    private ShopController shopController;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Vector2 rest = rectTransform.anchoredPosition;
        rest.y = inPosition;
        rectTransform.anchoredPosition = rest;
        
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        shopController = GameObject.Find("Shop").GetComponent<ShopController>();
    }

    void Update()
    {
        isMouseNearBottom = Input.mousePosition.y <= distanceFromBottomEdge;
        isMouseAboveInv = (Input.mousePosition.x >= 428f && Input.mousePosition.x <= 1491f);
        
        Vector2 move = rectTransform.anchoredPosition;

        if (playerController.upgradeMenuIsOpen || shopController.shopMenuIsOpen)
        {
            move.y = Mathf.Lerp(move.y, outPosition, Time.deltaTime * invSlideSpeed);
        }
        else if (isMouseNearBottom && isMouseAboveInv)
        {
            move.y = Mathf.Lerp(move.y, outPosition, Time.deltaTime * invSlideSpeed);
        }
        else
        {
            move.y = Mathf.Lerp(move.y, inPosition, Time.deltaTime * invSlideSpeed);
        }

        rectTransform.anchoredPosition = move;
    }
}
