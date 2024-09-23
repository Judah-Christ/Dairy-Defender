using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
   public enum PickupObject{COIN,TOWER };
    public PickupObject currentObject;
    public int pickupQuanity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            if(currentObject == PickupObject.COIN)
            {
                GameManager.gameManager.coins += pickupQuanity;
                Debug.Log(GameManager.gameManager.coins);
            }
            else if (currentObject == PickupObject.TOWER)
            {
                GameManager.gameManager.tower += pickupQuanity;
                Debug.Log(GameManager.gameManager.tower); 
            }
            Destroy(gameObject);
        }
    }
}
