using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [Range(0, 25)]
    [SerializeField] private float speed = 25f;

    [Range(1, 10)]
    [SerializeField] private float lifetime = 3f;

    [Range(25, 300)]
    [SerializeField] private int damageAmount;

    private Rigidbody2D rb;
    public Vector2 move;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject , lifetime);
    }


    private void FixedUpdate()
    {
        
         rb.velocity = transform.up * speed;
         direction = rb.velocity;
      
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            EnemyManager enemyComponent = collision.gameObject.GetComponent<EnemyManager>();
            enemyComponent.TakeDamage(damageAmount, direction);
            Destroy(gameObject);
        }
    }
}

   

