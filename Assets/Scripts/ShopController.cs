using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shop;
    public ButtonController buttonController;
    // Start is called before the first frame update
    void Start()
    {
        
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

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
