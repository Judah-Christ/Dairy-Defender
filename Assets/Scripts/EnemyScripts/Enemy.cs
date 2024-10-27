using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;

    NavMeshAgent agent;
    private GameManager GM;
    [SerializeField] private float agentSpeed = 3.5f;
    [SerializeField] public bool isflyEnemy = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Soda" && !isflyEnemy)
        {
            agent.speed /= collision.GetComponent<SodaSlowController>().slowSpeed;
        }
        else
        {
            Debug.Log("No slow");
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
