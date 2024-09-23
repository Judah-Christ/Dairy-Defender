using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    public enum PickupObject{COIN,TOWER };
    public PickupObject currentObject;
    public int pickupQuanity;
    private GameManager GM;


    private void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            if(currentObject == PickupObject.COIN)
            {
                GM.AddCoin(pickupQuanity);
                Destroy(gameObject);
                return;
            }
            else if (currentObject == PickupObject.TOWER)
            {
                GM.Tower += pickupQuanity;
                Destroy(gameObject);
                Debug.Log(GM.Tower);
                return;
            }
            
        }
    }
}
