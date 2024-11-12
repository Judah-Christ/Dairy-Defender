using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ShopController : MonoBehaviour
{
    private GameManager gameManager;
    private bool EOpensShop = false;
    public GameObject shopPanel;

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
            OpenShop();
        }
    }

    private void BuyTurret()
    {

    }

    private void BuySoda()
    {

    }

    private void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            EOpensShop = true;
        }
    }
}
