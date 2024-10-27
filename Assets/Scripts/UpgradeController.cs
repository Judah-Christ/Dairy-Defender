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
    private GameObject addTowerItem;

    public GameObject upgradePanel;
    public GameObject[] inventory = new GameObject[8];
    [SerializeField] private UpgradeSlot upgradeSlot;
    public List<GameItem> Towers = new List<GameItem>();
    [SerializeField] private PlayerTurret playerTurret;
    //private PlayerTurret playerTurret;
    private SodaBullet sodaBullet;
    public bool isUpgraded = false;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject towerParent;
    [SerializeField] private TextController textController;
    [SerializeField] private SodaSlowController sodaSlowController;
    [SerializeField] private GameObject lemonade;
    [SerializeField] private GameObject tea;
    [SerializeField] private GameObject rustTower;
    [SerializeField] private GameObject metalTower;
    private int noUpgrading = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //textController = GameObject.Find("GameText").GetComponent<TextController>();
        inventory[0] = GameObject.Find("InventorySlot1");
        inventory[1] = GameObject.Find("InventorySlot2");
        inventory[2] = GameObject.Find("InventorySlot3");
        inventory[3] = GameObject.Find("InventorySlot4");
        inventory[4] = GameObject.Find("InventorySlot5");
        inventory[5] = GameObject.Find("InventorySlot6");
        inventory[6] = GameObject.Find("InventorySlot7");
        inventory[7] = GameObject.Find("InventorySlot8");
    }

    public void AddTowerAgain1()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetComponent<SlotController>().isFull == false)
            {
                Debug.Log(inventory[i]);
                inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Towers[0].itemSprite;
                inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = Towers[0].itemObject;
                inventory[i].GetComponent<SlotController>().isFull = true;
                break;
            }
        }
        Destroy(tower);
        Destroy(upgradePanel);
        

    }

    public void AddTowerAgain2()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetComponent<SlotController>().isFull == false)
            {
                Debug.Log(inventory[i]);
                inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = addItem.itemSprite;
                inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = addItem.itemObject;
                inventory[i].GetComponent<SlotController>().isFull = true;
                break;
            }
        }
        Destroy(tower);
        Destroy(upgradePanel);


    }

    public void ButtonPress()
    {
        switch (upgradeSlot)
        {
            case UpgradeSlot.ONE:
                Debug.Log("Only one");
                AddTowerAgain1();
                Destroy(tower);
                Destroy(upgradePanel);
                Destroy(towerParent);
                break;
            case UpgradeSlot.TWO:
                AddTowerAgain2();
                Destroy(tower);
                break;
            case UpgradeSlot.THREE:
                addItem.itemObject = Towers[2].itemObject;
                addItem.itemSprite = Towers[2].itemSprite;
                //AddTowerAgain();
                break;
            case UpgradeSlot.FOUR:
                addItem.itemObject = Towers[3].itemObject;
                addItem.itemSprite = Towers[3].itemSprite;
                //AddTowerAgain();
                break;

        }
    }

    public void UpgradeButtonPress()
    {
        switch (upgradeSlot)
        {
            case UpgradeSlot.ONE:
                addItem.itemUpgradeCost = Towers[0].itemUpgradeCost;
                if (addItem.itemUpgradeCost < gameManager.Coins && noUpgrading < 3)
                {
                    noUpgrading++;
                    textController.UpdateCoins();
                    playerTurret.firingSpeed += 0.1f;
                    gameManager.RemoveCoin(addItem.itemUpgradeCost);
                }
                if(noUpgrading == 1)
                {
                    Destroy(tower);
                    Instantiate(rustTower, tower.transform.position, Quaternion.identity);
                }
                
                if(noUpgrading == 2)
                {
                    Destroy(tower);
                    Instantiate(metalTower, tower.transform.position, Quaternion.identity);
                }

                break;
            case UpgradeSlot.TWO:
                addItem.itemUpgradeCost = Towers[1].itemUpgradeCost;
                if (addItem.itemUpgradeCost < gameManager.Coins && noUpgrading < 3)
                {
                    noUpgrading++;
                    sodaSlowController.slowSpeed += 1;
                    textController.UpdateCoins();
                    isUpgraded = true;
                    gameManager.RemoveCoin(addItem.itemUpgradeCost);
                }

                if (noUpgrading == 1)
                {
                    Destroy(tower);
                    Instantiate(lemonade, tower.transform.position, Quaternion.identity);
                }

                if(noUpgrading == 2)
                {
                    Destroy(tower);
                    Instantiate(tea, tower.transform.position, Quaternion.identity);
                }

                break;
            case UpgradeSlot.THREE:
                Debug.Log("NO UPGRADE YET!");
                break;
            case UpgradeSlot.FOUR:
                Debug.Log("NO UPGRADE YET!");
                break;

        }
    }
    public enum UpgradeSlot
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
