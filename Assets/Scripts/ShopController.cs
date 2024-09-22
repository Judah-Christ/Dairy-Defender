using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ShopController : MonoBehaviour
{
    public GameObject shop;
    private GameManager gameManager;
    public ButtonController buttonController;

    private bool isShopFull;

    

    private List<ScriptableObject> shopItems;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shopItems = new List<ScriptableObject>();
        RollShop();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shop.SetActive(true);
            
        }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (shop == null)
        {
            buttonController.towerMenu.SetActive(false);
            buttonController.upgradeMenu.SetActive(false);
        }
        else
        {
            shop.SetActive(false);
            buttonController.towerMenu.SetActive(false);
            buttonController.upgradeMenu.SetActive(false);
        }
    }

    public void RollShop()
    {
        if (shopItems.Count >= 4)
        {
            isShopFull = true;
            return;
        }
        if (isShopFull == false)
        {
            int index = Random.Range(0, gameManager.Towers.Count);
            CheckForCopy(gameManager.Towers[index]);
        }
    }

    public void CheckForCopy(ScriptableObject currentItem)
    {
        bool isCopy = false;
        foreach (var item in shopItems)
        {
            if (currentItem.Equals(item))
            {
                isCopy = true;
                return;
            }
        }
        if (isCopy == true)
        {
            RollShop();
        }
        else
        {
            shopItems.Add(currentItem);
            RollShop();
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
