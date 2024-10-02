using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaBullet : MonoBehaviour
{

    //public Transform targets;
    //[Range(1, 10)]
    //[SerializeField] private float lifetime = 10f;
    public GameObject upgradePanel;

    // Start is called before the first frame update

    //private Rigidbody2D rb;
    //public Vector2 move;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {


    }

    //private void FixedUpdate()
    //{
       
    //    rb.position = transform.up ;

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            upgradePanel.SetActive(true);
        }
        
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        //Destroy(gameObject);

    //    }
    //}
}
