using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static ShopButtonController;

public class UpgradeController : MonoBehaviour
{
    public GameObject tower;
    public Sprite image;
    [SerializeField] private GameItem addItem;

    public GameObject upgradePanel;
    public GameObject[] inventory = new GameObject[8];
    [SerializeField] private ButtonSlot _buttonSlot;
    public List<GameItem> Towers = new List<GameItem>();
    private PlayerTurret playerTurret;
    private SodaBullet sodaBullet;
    public bool isUpgraded = false;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inventory[0] = GameObject.Find("InventorySlot1");
        inventory[1] = GameObject.Find("InventorySlot2");
        inventory[2] = GameObject.Find("InventorySlot3");
        inventory[3] = GameObject.Find("InventorySlot4");
        inventory[4] = GameObject.Find("InventorySlot5");
        inventory[5] = GameObject.Find("InventorySlot6");
        inventory[6] = GameObject.Find("InventorySlot7");
        inventory[7] = GameObject.Find("InventorySlot8");
    }

    public void AddTowerAgain()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetComponent<SlotController>().isFull == false)
            {
                Debug.Log(inventory[i]);
                inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = addItem.itemSprite;
                inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = addItem.itemObject;
                inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject.transform.position =
                    inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().imageLocation.transform.position;
                inventory[i].GetComponent<SlotController>().isFull = true;
                break;
            }
        }
        Destroy(tower);
        Destroy(upgradePanel);
        

    }

    public void ButtonPress()
    {
        switch (_buttonSlot)
        {
            case ButtonSlot.ONE:
                addItem.itemSprite = Towers[0].itemSprite;
                addItem.itemObject = Towers[0].itemObject;
                AddTowerAgain();
                if(addItem.itemObject == tower)
                {
                    Destroy(tower);
                    Destroy(upgradePanel);
                }
                
                break;
            case ButtonSlot.TWO:
                addItem.itemObject = Towers[1].itemObject;
                addItem.itemSprite = Towers[1].itemSprite;
                if (addItem.itemObject == tower)
                {
                    Destroy(tower);
                    Destroy(upgradePanel);
                }
                AddTowerAgain();
                break;
            case ButtonSlot.THREE:
                addItem.itemObject = Towers[2].itemObject;
                addItem.itemSprite = Towers[2].itemSprite;
                AddTowerAgain();
                break;
            case ButtonSlot.FOUR:
                addItem.itemObject = Towers[3].itemObject;
                addItem.itemSprite = Towers[3].itemSprite;
                AddTowerAgain();
                break;

        }
    }

    public void UpgradeButtonPress()
    {
        switch (_buttonSlot)
        {
            case ButtonSlot.ONE:
                addItem.itemUpgradeCost = Towers[0].itemUpgradeCost;
                if (addItem.itemUpgradeCost < gameManager.Coins)
                {
                    playerTurret.firingSpeed += 0.1f;
                    gameManager.RemoveCoin(addItem.itemUpgradeCost);
                }
                
                break;
            case ButtonSlot.TWO:
                addItem.itemUpgradeCost = Towers[1].itemUpgradeCost;
                if (addItem.itemUpgradeCost < gameManager.Coins)
                {
                    isUpgraded = true;
                    gameManager.RemoveCoin(addItem.itemUpgradeCost);
                }
                break;
            case ButtonSlot.THREE:
                Debug.Log("NO UPGRADE YET!");
                break;
            case ButtonSlot.FOUR:
                Debug.Log("NO UPGRADE YET!");
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
