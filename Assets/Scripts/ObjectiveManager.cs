using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveManager : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] int maxHealth = 1000;
    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //Debug.Log(currentHealth);
    }
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        

        if(currentHealth <= 0)
        {
            GM.ObjectiveFailed();
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(100);
            collision.gameObject.GetComponent<EnemyManager>().TakeDamage(100);

        }
    }

}
