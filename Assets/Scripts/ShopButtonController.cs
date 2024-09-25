using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

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
    public bool isPickUp = false;
    [SerializeField] private GameManager gameManager;



    public void AddTower()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetComponent<SlotController>().isFull == false && isPickUp == false)
            {
                Debug.Log(inventory[i]);
                inventory[i].transform.GetChild(0).GetComponent<Image>().sprite = shopItem.itemSprite;
                inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = shopItem.itemObject;
                inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject.transform.position = 
                    inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().imageLocation.transform.position;
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
        inventory[0] = GameObject.Find("InventorySlot1");
        inventory[1] = GameObject.Find("InventorySlot2");
        inventory[2] = GameObject.Find("InventorySlot3");
        inventory[3] = GameObject.Find("InventorySlot4");
        inventory[4] = GameObject.Find("InventorySlot5");
        inventory[5] = GameObject.Find("InventorySlot6");
        inventory[6] = GameObject.Find("InventorySlot7");
        inventory[7] = GameObject.Find("InventorySlot8");
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
                if(shopItem.itemCost <= gameManager.Coins)
                {
                    gameManager.Coins -= shopItem.itemCost;
                    AddTower();
                    Debug.Log("purchased");
                }
                break;
            case ButtonSlot.TWO:
                if(shopItem.itemCost <= gameManager.Coins)
                {
                    gameManager.Coins -= shopItem.itemCost;
                    AddTower();
                    Debug.Log("purchased");
                }
                break;
            case ButtonSlot.THREE:
                if(shopItem.itemCost <= gameManager.Coins)
                {
                    gameManager.Coins -= shopItem.itemCost;
                    AddTower();
                    Debug.Log("purchased");
                }
                break;
            case ButtonSlot.FOUR:
                if(shopItem.itemCost <= gameManager.Coins)
                {
                    gameManager.Coins -= shopItem.itemCost;
                    AddTower();
                    Debug.Log("purchased");
                }
                break;

        }

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
