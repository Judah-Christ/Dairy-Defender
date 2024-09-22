using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour
{
    public int buttons;
    public GameObject towerMenu;
    public GameObject upgradeMenu;
    public GameObject shop;

    


    // Start is called before the first frame update
    void Start()
    {


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

    public void PurchaseOne()
    {
        
    }

    public void PurchaseTwo()
    {
        if (buttons >= 7)
        {

            buttons -= 7;
        }
    }

    public void PurchaseThree()
    {
        if (buttons >= 7)
        {

            buttons -= 7;
        }
    }

    public void PurchaseFour()
    {
        if (buttons >= 10)
        {

            buttons -= 10;
        }
    }

    public void AmmoOne()
    {
        if (buttons >= 3)
        {

            buttons -= 3;
        }
    }

    public void AmmoTwo()
    {
        if (buttons >= 2)
        {

            buttons -= 2;
        }
    }

    public void AmmoThree()
    {
        if (buttons >= 1)
        {

            buttons -= 1;
        }
    }
 
       
}
