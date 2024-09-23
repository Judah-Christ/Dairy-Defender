using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int currenthealth;
    [SerializeField] int maxHealth = 100;
    [SerializeField] bool isflyEnemy = false;
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
