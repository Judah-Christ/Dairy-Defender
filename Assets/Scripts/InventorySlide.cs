using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlide : MonoBehaviour
{
    public float inPosition = -47.85f;
    public float outPosition = 0f;
    public float invSlideSpeed = 5f;

    private RectTransform rectTransform;
    private bool isInventoryOpen = false;

    private PlayerController playerController;

    private EventSystem eventSystem;
    private PointerEventData pointerEventData;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Vector2 rest = rectTransform.anchoredPosition;
        rest.y = inPosition;
        rectTransform.anchoredPosition = rest;

        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        eventSystem = EventSystem.current;
    }

    void Update()
    {
        Vector2 move = rectTransform.anchoredPosition;

        if (Input.GetMouseButtonDown(0))
        {
            if (IsPointerOverInventory())
            {
                isInventoryOpen = !isInventoryOpen;
            }
            else if (isInventoryOpen)
            {
                isInventoryOpen = false;
            }
        }

        if (playerController.upgradeMenuIsOpen)
        {
            move.y = Mathf.Lerp(move.y, outPosition, Time.deltaTime * invSlideSpeed);
        }
        else if (isInventoryOpen)
        {
            move.y = Mathf.Lerp(move.y, outPosition, Time.deltaTime * invSlideSpeed);
        }
        else
        {
            move.y = Mathf.Lerp(move.y, inPosition, Time.deltaTime * invSlideSpeed);
        }

        rectTransform.anchoredPosition = move;
    }

    private bool IsPointerOverInventory()
    {
        pointerEventData = new PointerEventData(eventSystem)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == this.gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
