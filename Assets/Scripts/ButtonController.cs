using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public int buttons;
    public GameObject towerMenu;
    public GameObject upgradeMenu;
    public GameObject shop;
    // Start is called before the first frame update
    void Start()
    {
        buttons = 100;
    }

    private void TowerMenu()
    {
        shop.SetActive(false);
        towerMenu.SetActive(true);
    }

    private void UpgradeMenu()
    {
        shop.SetActive(false);
        upgradeMenu.SetActive(true);
    }

    private void PurchaseOne()
    {
        if (buttons >= 4)
        {
            
            buttons -= 4;
        }
    }

    private void PurchaseTwo()
    {
        if (buttons >= 7)
        {

            buttons -= 7;
        }
    }

    private void PurchaseThree()
    {
        if (buttons >= 7)
        {

            buttons -= 7;
        }
    }

    private void PurchaseFour()
    {
        if (buttons >= 10)
        {

            buttons -= 10;
        }
    }

    private void AmmoOne()
    {
        if (buttons >= 3)
        {

            buttons -= 3;
        }
    }

    private void AmmoTwo()
    {
        if (buttons >= 2)
        {

            buttons -= 2;
        }
    }

    private void AmmoThree()
    {
        if (buttons >= 1)
        {

            buttons -= 1;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
