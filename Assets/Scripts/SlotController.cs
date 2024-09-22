using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IDropHandler
{
    public string itemNameOne;
    public string itemNameTwo;
    public string itemNameThree;
    public int quantity;
    public Sprite itemSpriteOne;
    public Sprite itemSpriteTwo;
    public Sprite itemSpriteThree;
    public bool isFull;

    [SerializeField]
    public Image itemImage;


    public void AddItem1(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemNameOne = itemName;
        this.quantity = quantity;
        this.itemSpriteOne = itemSprite;
        isFull = true;

    }

    public void AddItem2(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemNameTwo = itemName;
        this.quantity = quantity;
        this.itemSpriteTwo = itemSprite;
        isFull = true;

    }


    public void AddItem3(string itemName, int quantity, Sprite itemSprite)
    {
        this.itemNameThree = itemName;
        this.quantity = quantity;
        this.itemSpriteThree = itemSprite;
        isFull = true;

    }

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
