using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeSelector : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputAction mouseAction;
    private InputAction mousePosition;
    private bool isMouseActive = false;
    [SerializeField] private LayerMask collisionMask;
    private Ray ray;
    public GameObject currentTower { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

        playerInput.currentActionMap.Enable();
        mouseAction = playerInput.currentActionMap.FindAction("Mouse");
        mouseAction.started += MouseAction_started;
        mouseAction.canceled += MouseAction_canceled;
        mousePosition = playerInput.currentActionMap.FindAction("MousePosition");
    }

    private void MouseAction_started(InputAction.CallbackContext context)
    {
        isMouseActive = true;
    }

    private void MouseAction_canceled(InputAction.CallbackContext context)
    {
        isMouseActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouseActive == true)
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>()), Vector2.zero, Mathf.Infinity, collisionMask);
            if (hit)
            {
                currentTower = hit.transform.gameObject;;
                print(hit.transform.gameObject.name);
            }
            else
            {
                currentTower = null;
            }
        }
    }
}