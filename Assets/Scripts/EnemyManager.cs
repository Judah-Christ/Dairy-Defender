using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int currenthealth;
    [SerializeField] int maxHealth = 100;

    ObjectiveManager objectmanager;
    

    public GameObject lootDrop;
    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageAmount)
    {
        currenthealth -= damageAmount;

        if (currenthealth <= 0) 
        { 
            CheckDeath();
            Destroy(gameObject);
        }
    }

    private void CheckDeath()
    {
        if(currenthealth <= 0)
        {
            
            Instantiate(lootDrop,transform.position, Quaternion.identity);
        }
    }
   
   

}
