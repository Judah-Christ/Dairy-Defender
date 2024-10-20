using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [Range(0, 25)]
    [SerializeField] private float speed = 25f;

    [Range(1, 10)]
    [SerializeField] private float lifetime = 3f;

    // Start is called before the first frame update
   
         private Rigidbody2D rb;
    public Vector2 move;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject , lifetime);
    }

    // Update is called once per frame
    void Update()
    {
     
       
    }

    private void FixedUpdate()
    {
        
            rb.velocity = transform.up * speed;
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            EnemyManager enemyComponent = collision.gameObject.GetComponent<EnemyManager>();
            enemyComponent.TakeDamage(100);
            Destroy(gameObject);
        }
    }
}

   

