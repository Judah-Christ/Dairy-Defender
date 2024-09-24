using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class SlotController : MonoBehaviour, IDropHandler
{


    public bool isFull = false;

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
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
