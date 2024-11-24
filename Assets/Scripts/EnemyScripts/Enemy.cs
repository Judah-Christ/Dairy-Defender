using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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
    private bool isAttacking;

    public Vector3 moveDirection;

    private Rigidbody2D rb2d;
    private int knockBackX;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _minSpeed;
    private Animator anim;
    [SerializeField] private int _attackDmg;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!GM.isGamePaused)
        {

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
            CheckDist();                                                    
            agent.SetDestination(target.position);
            AnimationUpdate();


        }
        if (GM.isGamePaused)
        {
            agent.SetDestination(gameObject.transform.position);
        }

        moveDirection = transform.InverseTransformDirection(agent.velocity);


    }
    private void CheckDist()
    {
        if (GM.activeObject.Count == 1)
        {
            target = GM.activeObject[0];
            return;
        }
        int j = 0;
        float maxDistance = 10000000f;
        for (int i = 0; i < GM.activeObject.Count; i++)
        {
            Transform t = GM.activeObject[i];
            if (t != null)
            {
                float dist = Vector2.Distance(transform.position, t.position);
                if (dist < maxDistance)
                {
                    j = i;
                    maxDistance = dist;
                    target = t;
                }
            }

        }
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
        if (collision.CompareTag("Objective"))
        {
            if (!isAttacking)
            {
                StartCoroutine(ConstantAttack());
                isAttacking = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Soda" && !isflyEnemy)
        {
            agent.speed = agentSpeed;
        }
        if (collision.CompareTag("Objective"))
        {
            if (isAttacking)
            {
                StopCoroutine(ConstantAttack());
                isAttacking = false;
            }
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
        agent.SetDestination(transform.position);
        agent.velocity = new Vector3(0, 0, 0);
        agent.speed = 0;
        anim.SetTrigger("DeathAnim");
    }

    //public void Unstuck()
    //{
    //    agent.acceleration = 100;
    //    agent.speed = 100;

    //    //ResetSpeed();
    //}

    //private void ResetSpeed()
    //{
    //    agent.acceleration = 8;
    //    agent.speed = 3.5f;
    //}

    private IEnumerator ConstantAttack()
    {
        while (true)
        {
            target.GetComponent<ObjectiveManager>().TakeDamage(_attackDmg);
            yield return new WaitForSeconds(2f);
        }
    }
}
