using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;
using UnityEngine.UIElements;
using FMOD.Studio;
//using static ShopButtonController;

public class UpgradeController : MonoBehaviour
{
    private UpgradeSelector US;

    private UnityEngine.UI.Button upgradeButton;
    private UnityEngine.UI.Button dismantleButton;
    private UnityEngine.UI.Button pickUpButton;
    private UnityEngine.UI.Button rotateLeftButton;
    private UnityEngine.UI.Button rotateRightButton;

    public GameObject[] inventory = new GameObject[8];

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
        pickUpButton = gameObject.transform.Find("PickUpButton").GetComponent<UnityEngine.UI.Button>();
        dismantleButton = gameObject.transform.Find("DismantleButton").GetComponent<UnityEngine.UI.Button>();
        rotateLeftButton = gameObject.transform.Find("RotateLeftButton").GetComponent<UnityEngine.UI.Button>();
        rotateRightButton = gameObject.transform.Find("RotateRightButton").GetComponent<UnityEngine.UI.Button>();

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

    public void ButtonPress()
    {
        GetUpgradeLevel(US.CurrentTower);
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
                            Instantiate(_rustTower, US.CurrentTower.transform.position, Quaternion.identity);

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
                            Instantiate(_metalTower, US.CurrentTower.transform.position, Quaternion.identity);
                        }
                        
                    }
                    break;
                case UpgradeLevel.LVL_THREE:
                    break;
                case UpgradeLevel.NONE:
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
                            Destroy(US.CurrentTower);
                            Instantiate(_lemonade, US.CurrentTower.transform.position, Quaternion.identity);
                            AudioManager.instance.PlayOneShot(FMODEvents.instance.upgrade, this.transform.position);
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
                            Destroy(US.CurrentTower);
                            Instantiate(_tea, US.CurrentTower.transform.position, Quaternion.identity);
                            AudioManager.instance.PlayOneShot(FMODEvents.instance.upgrade, this.transform.position);
                        }
                    }
                    break;
                case UpgradeLevel.LVL_THREE:
                    break;
                case UpgradeLevel.NONE:
                    break;
                default:
                    return;
            }
        }

    }

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
                    inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = playerTurret.toolbarImage;
                    inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = tower;
                    inventory[i].GetComponent<SlotController>().isFull = true;
                    DestroyTower(US.CurrentTower);
                    break;
                }
                if (towerType == 2) //Soda Tower
                {
                    inventory[i].transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = sodaSlowController.toolbarImage;
                    inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = tower;
                    inventory[i].GetComponent<SlotController>().isFull = true;
                    DestroyTower(US.CurrentTower);
                    break;
                }
                if (towerType == 3) //Trap Tower [NOT IMPLEMENTED YET]
                {
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
        if (currentTower.CompareTag("Soda"))
        {
            Destroy(currentTower);
        }
        else
        {
            GameObject towerParent = currentTower.transform.parent.gameObject;
            if (towerParent != null)
            {
                Destroy(towerParent.gameObject);
            }
        }        
    }

    public void RotateTowerLeft()
    {
        if(US.CurrentTower != null && US.CurrentTower.CompareTag("Turret"))
        {
            playerTurret.CurrentRotation += 90;
            US.CurrentTower.transform.parent.Rotate(0, 0, playerTurret.CurrentRotation);
        }
    }

    public void RotateTowerRight()
    {
        if (US.CurrentTower != null && US.CurrentTower.CompareTag("Turret"))
        {
            playerTurret.CurrentRotation -= 90;
            US.CurrentTower.transform.parent.Rotate(0, 0, playerTurret.CurrentRotation);
        }
    }

    public void DismantleTower()
    {
        if(US.CurrentTower != null)
        {
            GetUpgradeLevel(US.CurrentTower);
            if(towerType == 1)
            {
                gameManager.AddCoin((playerTurret.UpgradeCost -1));
                AudioManager.instance.PlayOneShot(FMODEvents.instance.dismantle, this.transform.position);
                DestroyTower(US.CurrentTower);
                return;
            }
            if(towerType == 2)
            {
                gameManager.AddCoin((sodaSlowController.UpgradeCost - 1));
                AudioManager.instance.PlayOneShot(FMODEvents.instance.dismantle, this.transform.position);
                DestroyTower(US.CurrentTower);
                return;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (US.CurrentTower == null)
        {
            upgradeButton.interactable = false;
            dismantleButton.interactable = false;
            pickUpButton.interactable = false;
            rotateLeftButton.interactable = false;
            rotateRightButton.interactable = false;
        }
        if (US.CurrentTower != null)
        {
            upgradeButton.interactable = true;
            dismantleButton.interactable = true;
            pickUpButton.interactable = true;
            if(towerType == 1)
            {
                rotateLeftButton.interactable = true;
                rotateRightButton.interactable = true;
            }
        }
    }
}
