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
    private PlayerController PC;
    public GameObject CurrentTower;
    private GameObject selection;
    private UpgradeController UC;


    // Start is called before the first frame update
    void Start()
    {
        playerInput.currentActionMap.Enable();
        mouseAction = playerInput.currentActionMap.FindAction("Mouse");
        mouseAction.started += MouseAction_started;
        mouseAction.canceled += MouseAction_canceled;
        mousePosition = playerInput.currentActionMap.FindAction("MousePosition");
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        UC = GameObject.Find("UpgradeMenu").GetComponent<UpgradeController>();
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
        if (isMouseActive == true && PC.upgradeMenuIsOpen == true)
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>()), Vector2.zero, Mathf.Infinity, collisionMask);
            if (hit && CurrentTower == null)
            {
                CurrentTower = hit.transform.gameObject;
                UC.GetUpgradeLevel(CurrentTower);
                
                if (CurrentTower.CompareTag("Turret"))
                {
                    if (selection != null)
                    {
                        selection.SetActive(false);
                        selection = null;
                    }
                    Transform parent = CurrentTower.transform.parent;
                    selection = parent.Find("Selection").gameObject;
                    selection.SetActive(true);
                    Debug.Log(CurrentTower);
                }
                if (CurrentTower.CompareTag("Soda"))
                {
                    if (selection != null)
                    {
                    selection.SetActive(false);
                    selection = null;
                    }
                    selection = CurrentTower.transform.Find("Selection").gameObject;
                    selection.SetActive(true);
                    Debug.Log(CurrentTower);
                }
                
            }
            else if (hit == false && CurrentTower == null)
            {
                if (selection != null)
                {
                    selection.SetActive(false);
                    selection = null;
                }
                CurrentTower = null;
            }
        }
        if (PC.upgradeMenuIsOpen == false)
        {
            if (selection != null)
            {
                selection.SetActive(false);
                selection = null;
            }
            CurrentTower = null;
        }
    }
}
