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
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private LayerMask UIMask;
    private PlayerController PC;
    private GameObject currentTower;
    private GameObject selection;
    private UpgradeController UC;
    private UpgradeMenuSlide UM;
    //[SerializeField] private GameObject leftClickToSelect;


    // Start is called before the first frame update
    void Start()
    {
        playerInput.currentActionMap.Enable();
        mouseAction = playerInput.currentActionMap.FindAction("Mouse");
        mouseAction.started += MouseAction_started;
        mousePosition = playerInput.currentActionMap.FindAction("MousePosition");
        PC = GameObject.Find("Player").GetComponent<PlayerController>();
        UC = gameObject.GetComponent<UpgradeController>();
    }

    private void MouseAction_started(InputAction.CallbackContext context)
    {
        //if (PC.upgradeMenuIsOpen == true)
        //{

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>()), Vector2.zero, Mathf.Infinity, collisionMask);
            RaycastHit2D UIhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition.ReadValue<Vector2>()), Vector2.zero, Mathf.Infinity, collisionMask);

            if (hit && currentTower == null)
            {
                currentTower = hit.transform.gameObject;
                UC.SetCurrentTower(currentTower);
                UC.GetUpgradeLevel(currentTower);
                //leftClickToSelect.SetActive(false);

                if (currentTower.CompareTag("Turret"))
                {
                    //DeselectTower(currentTower);              
                    Transform parent = currentTower.transform.parent;
                    selection = parent.Find("Selection").gameObject;
                    selection.SetActive(true);
                    //gameObject.SetActive(true);
                    return;
                }
                if (currentTower.CompareTag("Soda"))
                {
                    //DeselectTower(currentTower);
                    selection = currentTower.transform.Find("Selection").gameObject;
                    selection.SetActive(true);
                    //gameObject.SetActive(false);
                    return;
                }
                return;

            }
            else if (hit && currentTower != null)
            {
                //DeselectTower(true, currentTower);
                //currentTower = null;
                //leftClickToSelect.SetActive(true);
                return;
            }
            else if (hit == false)
            {
                //DeselectTower(true, currentTower);
                //currentTower = null;
                //leftClickToSelect.SetActive(true);
                return;
            }
            //}
            //if (PC.upgradeMenuIsOpen == false)
            //{
            // DeselectTower();
                //CurrentTower = null;
            // return;
            //}
    }

    private void DeselectTower(GameObject tower)
    {
        if(selection != null) 
        {
            selection.SetActive(false);
            //gameObject.SetActive(false);
            selection = null;
            //leftClickToSelect.SetActive(true);
        }

        if (tower != null)
        {
            UC.UnsetCurrentTower();
            //currentTower = null;
        }
    }

    public void DeselectTower(bool removeTower, GameObject tower)
    {
        if (selection != null && removeTower == true)
        {
            selection.SetActive(false);
            //gameObject.SetActive(false);
            selection = null;
            //currentTower = null;
            return;
        }

        if (tower != null)
        {
            UC.UnsetCurrentTower();
            //currentTower = null;
        }
    }

}
