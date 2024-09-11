using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shop;
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
        shop.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
