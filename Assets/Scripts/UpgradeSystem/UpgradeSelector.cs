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
        mousePosition = playerInput.currentActionMap.FindAction("MousePosition");
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        UC = GameObject.Find("UpgradeMenu").GetComponent<UpgradeController>();
    }

    private void MouseAction_started(InputAction.CallbackContext context)
    {
        if (PC.upgradeMenuIsOpen == true)
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>()), Vector2.zero, Mathf.Infinity, collisionMask);
            if (hit && CurrentTower == null)
            {
                CurrentTower = hit.transform.gameObject;
                UC.GetUpgradeLevel(CurrentTower);

                if (CurrentTower.CompareTag("Turret"))
                {
                    DeselectTower();
                    Transform parent = CurrentTower.transform.parent;
                    selection = parent.Find("Selection").gameObject;
                    selection.SetActive(true);
                    return;
                }
                if (CurrentTower.CompareTag("Soda"))
                {
                    DeselectTower();
                    selection = CurrentTower.transform.Find("Selection").gameObject;
                    selection.SetActive(true);
                    Debug.Log(CurrentTower);
                    return;
                }

            }
            else if (hit && CurrentTower != null)
            {
                DeselectTower(true);
                return;
            }
            else if (hit == false)
            {
                
            }
        }
        if (PC.upgradeMenuIsOpen == false)
        {
            DeselectTower();
            CurrentTower = null;
            return;
        }
    }

    private void DeselectTower()
    {
        if(selection != null) 
        {
            selection.SetActive(false);
            selection = null;
        }
    }

    public void DeselectTower(bool removeTower)
    {
        if (selection != null && removeTower == true)
        {
            selection.SetActive(false);
            selection = null;
            CurrentTower = null;
            return;
        }
    }

}
