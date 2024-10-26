using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;

    NavMeshAgent agent;
    private GameManager GM;
    [SerializeField] private float agentSpeed = 3.5f;
    [SerializeField] public bool isflyEnemy = false;

    private Vector3 pastMoveDirection;
    private Vector3 moveDirection;

    [SerializeField]
    private float _maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!GM.isGamePaused)
        {
            target = FindAnyObjectByType<ObjectiveManager>().transform;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !GM.isGamePaused)
        {
            agent.SetDestination(target.position);
        }
        if (GM.isGamePaused)
        {
            agent.SetDestination(gameObject.transform.position);
        }


        
    }

    private void GetDistance()
    {
        if (pastMoveDirection != transform.position)
        {
            moveDirection = (pastMoveDirection - transform.position).normalized;
            pastMoveDirection = transform.position;
            //Debug.Log(moveDirection);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Soda" && !isflyEnemy)
        {
            agent.speed /= collision.GetComponent<SodaSlowController>().slowSpeed;
        }
        else
        {
            //Debug.Log("No slow");
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Soda" && !isflyEnemy)
        {
            agent.speed = agentSpeed;
        }
    }
}
