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
    private UpgradeSelector US;

    private GameObject tower;
    //public Sprite image;
    [SerializeField] private GameItem addItem;
    private GameObject addTowerItem;

    //public GameObject upgradePanel;
    public GameObject[] inventory = new GameObject[8];
    [SerializeField] private UpgradeLevel upgradeLevel;
    public List<GameItem> Towers = new List<GameItem>();

    //private PlayerTurret playerTurret;
    private SodaBullet sodaBullet;
    public bool isUpgraded = false;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextController textController;

    private PlayerTurret playerTurret;
    private SodaSlowController sodaSlowController;
    [SerializeField] private GameObject _sodaTower;
    [SerializeField] private GameObject _lemonade;
    [SerializeField] private GameObject _tea;
    [SerializeField] private GameObject _playerTower;
    [SerializeField] private GameObject _rustTower;
    [SerializeField] private GameObject _metalTower;
    private int noUpgrading = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        US = GameObject.Find("UpgradeManager").GetComponent<UpgradeSelector>();

        playerTurret = _playerTower.GetComponent<PlayerTurret>();
        sodaSlowController = _sodaTower.GetComponent<SodaSlowController>();

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
        Destroy(US.currentTower);
        
        

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
        Destroy(US.currentTower);
        


    }

    public void ButtonPress()
    {
        switch (upgradeLevel)
        {
            case UpgradeLevel.ONE:
                Debug.Log("Only one");
                AddTowerAgain1();
                Destroy(US.currentTower);
                break;
            case UpgradeLevel.TWO:
                AddTowerAgain2();
                Destroy(US.currentTower);
                break;
            case UpgradeLevel.THREE:
                addItem.itemObject = Towers[2].itemObject;
                addItem.itemSprite = Towers[2].itemSprite;
                //AddTowerAgain();
                break;
            case UpgradeLevel.FOUR:
                addItem.itemObject = Towers[3].itemObject;
                addItem.itemSprite = Towers[3].itemSprite;
                //AddTowerAgain();
                break;

        }
    }

    public void UpgradeButtonPress()
    {
        switch (upgradeLevel)
        {
            case UpgradeLevel.ONE:
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
                    Destroy(US.currentTower);
                    Instantiate(_rustTower, US.currentTower.transform.position, Quaternion.identity);
                }
                
                if(noUpgrading == 2)
                {
                    Destroy(US.currentTower);
                    Instantiate(_metalTower, US.currentTower.transform.position, Quaternion.identity);
                }

                break;
            case UpgradeLevel.TWO:
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
                    Destroy(US.currentTower);
                    Instantiate(_lemonade, US.currentTower.transform.position, Quaternion.identity);
                }

                if(noUpgrading == 2)
                {
                    Destroy(US.currentTower);
                    Instantiate(_tea, US.currentTower.transform.position, Quaternion.identity);
                }

                break;
            case UpgradeLevel.THREE:
                Debug.Log("NO UPGRADE YET!");
                break;
            case UpgradeLevel.FOUR:
                Debug.Log("NO UPGRADE YET!");
                break;

        }
    }
    public enum UpgradeLevel
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
