using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class CurrencyPickup : MonoBehaviour
{
    public enum PickupObject{COIN,TOWER };
    public PickupObject currentObject;
    public int pickupQuanity;
    private GameManager GM;
    private bool isMagnetized;
    private Transform target;

    [SerializeField] private float _pickupSpeed;

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
                AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonPickUp, this.transform.position);
                Destroy(gameObject);
                return;
            }
            else if (currentObject == PickupObject.TOWER)
            {
                GM.Tower += pickupQuanity;
                Destroy(gameObject);
                return;
            }
            
        }

        if (other.CompareTag("CoinAOE") && target == null)
        {
            target = other.transform.parent;
            isMagnetized = true;
            gameObject.layer = LayerMask.NameToLayer("TempIgnore");
            
        }
    }

    private void FixedUpdate()
    {
        if(isMagnetized && target != null)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.transform.position, _pickupSpeed);
            _pickupSpeed = _pickupSpeed + 0.01f;
        }
    }
}
