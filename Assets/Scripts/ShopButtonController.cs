using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using TMPro;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    private ShopController SC;
    [SerializeField]
    private ButtonSlot _buttonSlot;
    
    private GameItem shopItem;

    private string itemName;
    
    
    private int itemCost;

    [SerializeField]
    private TMP_Text _buttonText;

    [SerializeField]
    private TMP_Text _shopCost;

    public GameObject[] inventory = new GameObject[8];
    bool itemAdded = false;

    public void AddTower()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetComponent<SlotController>().isFull == false)
            {
                Debug.Log(inventory[i]);
                inventory[i].transform.GetChild(0).GetComponent<Image>().sprite = shopItem.itemSprite;
                inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = shopItem.itemObject;
                inventory[i].GetComponent<SlotController>().isFull = true;
                itemAdded = true;
                break;
            }

        }

        if (!itemAdded)
        {
            Debug.Log("Inventory full - item not added");
        }
    }

    void Start()
    {
        SC = GetComponentInParent<ShopController>();
        ItemSetup();

    }

    private void ItemSetup()
    {
        switch (_buttonSlot)
        {
            case ButtonSlot.ONE:
                shopItem = SC.ShopItems[0];
                UISetup();
                break;
            case ButtonSlot.TWO:
                shopItem = SC.ShopItems[1];
                UISetup();
                break;
            case ButtonSlot.THREE:
                shopItem = SC.ShopItems[2];
                UISetup();
                break;
            case ButtonSlot.FOUR:
                shopItem = SC.ShopItems[3];
                UISetup();
                break;
        }
    }

    public void ButtonPress()
    {
        switch( _buttonSlot)
        {
            case ButtonSlot.ONE:
                AddTower();
                break;
            case ButtonSlot.TWO:
                AddTower();
                break;
            case ButtonSlot.THREE:
                AddTower();
                break;
            case ButtonSlot.FOUR:
                AddTower();
                break;

        }

    }

    public void PlaceTower()
    {
        
    }


    public enum ButtonSlot
    {
        ONE,
        TWO, 
        THREE,
        FOUR,
    }

    private void UISetup()
    {
        this.itemCost = shopItem.itemCost;
        //itemIm = shopItem.itemImage;
        //itemSpri = shopItem.itemSprite;
        itemName = shopItem.itemName;


        _buttonText.text = itemName;
        _shopCost.text = this.itemCost.ToString();
    }
}
