using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static ShopButtonController;

public class UpgradeController : MonoBehaviour
{
    private UpgradeSelector US;

    private UnityEngine.UI.Button upgradeButton;
    private UnityEngine.UI.Button dismantleButton;

    private GameObject tower;
    //public Sprite image;
    [SerializeField] private GameItem addItem;
    private GameObject addTowerItem;

    //public GameObject upgradePanel;
    public GameObject[] inventory = new GameObject[8];
    [SerializeField] private UpgradeLevel upgradeLevel;

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
    private int towerType;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        US = GameObject.Find("UpgradeMenu").GetComponent<UpgradeSelector>();

        upgradeButton = gameObject.transform.Find("UpgradeButton").GetComponent<UnityEngine.UI.Button>();
        dismantleButton = gameObject.transform.Find("DismantleButton").GetComponent<UnityEngine.UI.Button>();

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

    //public void AddTowerAgain1()
    //{
    //    for (int i = 0; i < inventory.Length; i++)
    //    {
    //        if (inventory[i].GetComponent<SlotController>().isFull == false)
    //        {
    //            Debug.Log(inventory[i]);
    //            inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Towers[0].itemSprite;
    //            inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = Towers[0].itemObject;
    //            inventory[i].GetComponent<SlotController>().isFull = true;
    //            Destroy(US.currentTower);
    //            break;
    //        }
    //    }
        
        
        

    //}

    //public void AddTowerAgain2()
    //{
    //    for (int i = 0; i < inventory.Length; i++)
    //    {
    //        if (inventory[i].GetComponent<SlotController>().isFull == false)
    //        {
    //            Debug.Log(inventory[i]);
    //            inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = addItem.itemSprite;
    //            inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = addItem.itemObject;
    //            inventory[i].GetComponent<SlotController>().isFull = true;
    //            break;
    //        }
    //    }
    //    Destroy(US.currentTower);
        


    //}

    public void ButtonPress()
    {
        if (towerType == 1)
        {
            switch (playerTurret.TowerLevel)
            {
                case UpgradeLevel.LVL_ONE:
                    PickUpTower(_playerTower, towerType);
                    break;
                case UpgradeLevel.LVL_TWO:
                    PickUpTower(_rustTower, towerType);
                    break;
                case UpgradeLevel.LVL_THREE:
                    PickUpTower(_metalTower, towerType);
                    break;
                case UpgradeLevel.NONE:
                    break;
                default:
                    return;

            }

            if (towerType == 2)
            {
                switch (sodaSlowController.SodaLevel)
                {
                    case UpgradeLevel.LVL_ONE:
                        PickUpTower(_sodaTower, towerType);
                        break;
                    case UpgradeLevel.LVL_TWO:
                        PickUpTower(_lemonade, towerType);
                        break;
                    case UpgradeLevel.LVL_THREE:
                        PickUpTower(_tea, towerType);
                        break;
                    case UpgradeLevel.NONE:
                        break;
                    default:
                        return;

                }
            }
        }
        
    }

    public void UpgradeButtonPress()
    {
        if(towerType == 1)
        {
            switch (playerTurret.TowerLevel)
            {
                case UpgradeLevel.LVL_ONE:
                    if (playerTurret.UpgradeCost < gameManager.Coins)
                    {
                        //textController.UpdateCoins();
                        gameManager.RemoveCoin(playerTurret.UpgradeCost);
                        if(US.CurrentTower != null)
                        {
                            DestroyTower(US.CurrentTower);
                            US.CurrentTower = Instantiate(_rustTower, US.CurrentTower.transform.position, Quaternion.identity);
                            GetUpgradeLevel(US.CurrentTower);
                        }
                        
                    }
                    break;
                case UpgradeLevel.LVL_TWO:
                    if (playerTurret.UpgradeCost < gameManager.Coins)
                    {
                        //textController.UpdateCoins();
                        gameManager.RemoveCoin(playerTurret.UpgradeCost);
                        if(US.CurrentTower != null)
                        {
                            DestroyTower(US.CurrentTower);
                            US.CurrentTower = Instantiate(_metalTower, US.CurrentTower.transform.position, Quaternion.identity);
                            GetUpgradeLevel(US.CurrentTower);
                        }
                        
                    }
                    break;
                case UpgradeLevel.LVL_THREE:
                    Debug.Log("NO UPGRADE YET!");
                    break;
                case UpgradeLevel.NONE:
                    Debug.Log("NO UPGRADE YET!");
                    break;
                default:
                    return;
            }
        }

        if (towerType == 2)
        {
            switch (sodaSlowController.SodaLevel)
            {
                case UpgradeLevel.LVL_ONE:
                    if (sodaSlowController.UpgradeCost < gameManager.Coins)
                    {
                        //textController.UpdateCoins();
                        gameManager.RemoveCoin(sodaSlowController.UpgradeCost);
                        if(US.CurrentTower != null)
                        {
                            DestroyTower(US.CurrentTower);
                            US.CurrentTower = Instantiate(_lemonade, US.CurrentTower.transform.position, Quaternion.identity);
                            GetUpgradeLevel(US.CurrentTower);
                        }
                    }
                    break;
                case UpgradeLevel.LVL_TWO:
                    if (sodaSlowController.UpgradeCost < gameManager.Coins)
                    {
                        //textController.UpdateCoins();
                        gameManager.RemoveCoin(sodaSlowController.UpgradeCost);
                        if(US.CurrentTower != null)
                        {
                            DestroyTower(US.CurrentTower);
                            US.CurrentTower = Instantiate(_tea, US.CurrentTower.transform.position, Quaternion.identity);
                            GetUpgradeLevel(US.CurrentTower);
                        }
                    }
                    break;
                case UpgradeLevel.LVL_THREE:
                    Debug.Log("NO UPGRADE YET!");
                    break;
                case UpgradeLevel.NONE:
                    Debug.Log("NO UPGRADE YET!");
                    break;
                default:
                    return;
            }
        }

    }
    //public enum UpgradeLevel
    //{
    //    ONE,
    //    TWO,
    //    THREE,
    //    FOUR,
    //}

    public void GetUpgradeLevel(GameObject currentTower)
    {
        if (currentTower != null && US.CurrentTower.CompareTag("Turret"))
        {
            playerTurret = US.CurrentTower.GetComponent<PlayerTurret>();
            towerType = 1;
        }
        if (currentTower != null && US.CurrentTower.CompareTag("Soda"))
        {
            sodaSlowController = US.CurrentTower.GetComponentInChildren<SodaSlowController>();
            towerType = 2;
        }
    }

    public void PickUpTower(GameObject tower, int towerType)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetComponent<SlotController>().isFull == false)
            {
                if (towerType == 1) //Player Turret
                {
                    Debug.Log(inventory[i]);
                    inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = playerTurret.toolbarImage;
                    inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = tower;
                    inventory[i].GetComponent<SlotController>().isFull = true;
                    DestroyTower(US.CurrentTower);
                    break;
                }
                if (towerType == 2) //Soda Tower
                {
                    Debug.Log(inventory[i]);
                    inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sodaSlowController.toolbarImage;
                    inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = tower;
                    inventory[i].GetComponent<SlotController>().isFull = true;
                    DestroyTower(US.CurrentTower);
                    break;
                }
                if (towerType == 3) //Trap Tower [NOT IMPLEMENTED YET]
                {
                    Debug.Log(inventory[i]);
                    inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sodaSlowController.toolbarImage;  //CHANGE WHEN TRAP IS ADDED
                    inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = tower;
                    inventory[i].GetComponent<SlotController>().isFull = true;
                    DestroyTower(US.CurrentTower);
                    break;
                }
            }
        }
    }

    public void DestroyTower(GameObject currentTower)
    {
        GameObject towerParent = currentTower.transform.parent.gameObject;
        if (towerParent != null)
        {
           Destroy(towerParent.gameObject);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (US.CurrentTower == null)
        {
            upgradeButton.interactable = false;
            dismantleButton.interactable = false;
        }
        if (US.CurrentTower != null)
        {
            upgradeButton.interactable = true;
            dismantleButton.interactable = true;
        }
    }
}
