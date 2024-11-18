
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private GameManager gameManager;
    private bool EOpensShop = false;
    public GameObject shopPanel;
    public GameObject[] inventory = new GameObject[8];
    //bool itemAdded = false;
    public bool isPickUp = false;
    [SerializeField]
    private GameItem towerItem;
    [SerializeField]
    private GameItem sodaItem;
    private GameItem shopItem;
    public bool shopMenuIsOpen;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && EOpensShop)
        {
            Debug.Log($"E key pressed. shopMenuIsOpen before = {shopMenuIsOpen}");
            OpenShop();
            Debug.Log($"E key pressed. shopMenuIsOpen after = {shopMenuIsOpen}");
        }
    }

    public void BuyTurret()
    {
        shopItem = towerItem;
        if (shopItem.itemCost <= gameManager.Coins)
        {
            gameManager.Coins -= shopItem.itemCost;
            AddTower();
        }
    }

    public void BuySoda()
    {
        shopItem = sodaItem;
        if (shopItem.itemCost <= gameManager.Coins)
        {
            gameManager.Coins -= shopItem.itemCost;
            AddTower();
        }
    }

    private void OpenShop()
    {
        shopPanel.SetActive(true);
        shopMenuIsOpen = true;
        Debug.Log($"OpenShop called. shopMenuIsOpen = {shopMenuIsOpen}");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            EOpensShop = true;
        }
    }

    private void AddTower()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].GetComponent<SlotController>().isFull == false && isPickUp == false)
            {
                inventory[i].transform.GetChild(0).GetComponent<Image>().sprite = shopItem.itemSprite;
                inventory[i].transform.GetChild(0).GetComponent<InventoryItem>().towerObject = shopItem.itemObject;
                inventory[i].GetComponent<SlotController>().isFull = true;
                break;
            }

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        EOpensShop = false;
    }

    public void CloseHandle()
    {
        shopMenuIsOpen = false;
        Debug.Log($"CloseHandler called. shopMenuIsOpen = {shopMenuIsOpen}");
    }
}
