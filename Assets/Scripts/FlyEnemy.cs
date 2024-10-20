using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    private Rigidbody2D rb;
    private GameManager GM;
    private float speed;

    void Start()
    {
        target = FindAnyObjectByType<ObjectiveManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        speed = 2f;
    }

    void Update()
    {
        if (target != null)
        {
            rb.position = Vector3.MoveTowards(rb.position, target.position, .004f);
        }
    }
}
