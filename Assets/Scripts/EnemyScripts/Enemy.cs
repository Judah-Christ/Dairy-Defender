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
    [SerializeField] private bool isflyEnemy = false;

    public Vector3 moveDirection;

    private Rigidbody2D rb2d;
    private int knockBackX;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _minSpeed;
    private Animator anim;

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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && !GM.isGamePaused)
        {
            
            agent.SetDestination(target.position);
            AnimationUpdate();
        }
        if (GM.isGamePaused)
        {
            agent.SetDestination(gameObject.transform.position);
        }

        moveDirection = transform.InverseTransformDirection(agent.velocity);
        
        
    }


    private void AnimationUpdate()
    {
        anim.SetFloat("MoveX", moveDirection.x);
        anim.SetFloat("MoveY", moveDirection.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Soda" && !isflyEnemy)
        {
            agent.speed /= collision.GetComponent<SodaSlowController>().slowSpeed;
        }
        if (collision.CompareTag("OOB") && gameObject.layer != LayerMask.NameToLayer("Floor"))
        {
            Debug.Log("Bumped!");
            Unstuck();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Soda" && !isflyEnemy)
        {
            agent.speed = agentSpeed;
        }
    }

    public void CollisionDirection(Vector2 direction)
    {
        anim.SetInteger("KnockBackX", ((int)direction.x));
    }

    public void TriggerKnockback()
    {
        anim.SetTrigger("KnockbackAnim");
    }

    public void StopEnemy()
    {
        agent.SetDestination(target.position);
        agent.velocity = new Vector3(0, 0, 0);
        agent.speed = 0;
        anim.SetTrigger("DeathAnim");
    }

    public void Unstuck()
    {
        agent.acceleration = 10000;
        agent.speed = 1000;

        agent.acceleration = 8;
        agent.speed = 3.5f;
    }

}
