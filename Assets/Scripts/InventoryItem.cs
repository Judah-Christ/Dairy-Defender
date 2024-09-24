using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEngine.UI.Image image;
    public SlotController slotController;
    private bool isTowerPlaced = false;
    public Transform imageLocation;
    public GameObject towerObject;

    [HideInInspector] public Transform parentAfterDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        if (slotController.isFull == true)
        {
            slotController.isFull = false;
            isTowerPlaced = true;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
       transform.position = Input.mousePosition;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
        if(isTowerPlaced == true)
        {
            isTowerPlaced = false;
            GetComponent<UnityEngine.UI.Image>().sprite = null;
            Instantiate(towerObject, imageLocation);
        }

    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
