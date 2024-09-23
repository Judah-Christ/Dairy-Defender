using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Composites;
using TMPro;

public class ShopButtonController : MonoBehaviour
{
    private ShopController SC;
    [SerializeField]
    private ButtonSlot _buttonSlot;

    private GameItem shopItem;

    private string itemName;
    private GameObject itemObj;
    private int itemCost;

    [SerializeField]
    private TMP_Text _buttonText;

    [SerializeField]
    private TMP_Text _shopCost;


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
        itemObj = shopItem.itemObject;
        itemName = shopItem.itemName;

        _buttonText.text = itemName;
        _shopCost.text = this.itemCost.ToString();
    }
}
