using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public int buttons;
    // Start is called before the first frame update
    void Start()
    {
        buttons = 100;
    }

    public void PurchaseOne()
    {
        if (buttons >= 4)
        {
            
            buttons -= 4;
        }
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
