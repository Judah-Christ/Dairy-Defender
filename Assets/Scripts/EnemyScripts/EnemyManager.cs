using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using static WaveSpawner;

public class EnemyManager : MonoBehaviour
{
    private int currenthealth;
    [SerializeField] int maxHealth = 100;

    ObjectiveManager objectmanager;
    

    public GameObject lootDrop;
    public AudioClip deathScream;
    public AudioClip deathScream1;
    public AudioClip deathScream2;
    public AudioClip deathScream3;
    public AudioClip coinDrop;
    private AudioClip[] deathScreams;
    private SpriteRenderer sr;
    private GameObject coin;
    private PlayerController pc;

    [SerializeField] private Color highHealth;
    [SerializeField] private Color mediumHealth;
    [SerializeField] private Color lowHealth;
    [SerializeField] private Slider enemySlider;
    [SerializeField] private Image enemySliderFill;
    [SerializeField] private bool isflyEnemy;

    [SerializeField] private Canvas canvas;
    [SerializeField] private float _knockbackAmount;
    private Rigidbody2D rb2d;
    private FlyEnemy flyEnemy;
    private Enemy enemy;

    private StudioEventEmitter emitter;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        deathScreams = new AudioClip[] {deathScream, deathScream1, deathScream2, deathScream3};
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        enemySliderFill.color = highHealth;
        rb2d = gameObject.GetComponent<Rigidbody2D>();

        if(isflyEnemy == true)
        {
            flyEnemy = gameObject.GetComponent<FlyEnemy>();
        }
        if(isflyEnemy == false)
        {
            enemy = gameObject.GetComponent<Enemy>();
        }
    

        if (gameObject.name == "RatEnemy(Clone)")
        {
            emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.ratSqueaks, this.gameObject);
        }
        else if (gameObject.name == "FlyEnemy(Clone)")
        {
            emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.flyBuzzing, this.gameObject);
        }
        emitter.Play();
    }

    // Update is called once per frame
    void Update()
    {


        if (currenthealth >= 0.66 * maxHealth)
        {
            enemySliderFill.color = highHealth;
        }
        else if (currenthealth >= 0.33 * maxHealth)
        {
            enemySliderFill.color = mediumHealth;
        }
        else
        {
            enemySliderFill.color = lowHealth;
        }
    }

    public void TakeDamage(int damageAmount, Vector2 direction)
    {
        
        if(currenthealth > 0)
        {
            currenthealth -= damageAmount;
            rb2d.AddForce(direction * _knockbackAmount * Time.deltaTime, ForceMode2D.Impulse);
            enemySlider.value = currenthealth;


            if (isflyEnemy == true)
            {
                flyEnemy.CollisionDirection(direction);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.flyDie, this.transform.position);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonDrop, this.transform.position);

            }
            if (isflyEnemy == false)
            {
                enemy.CollisionDirection(direction);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.ratDeathScreams, this.transform.position);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonDrop, this.transform.position);
            }
        }

        if (currenthealth <= 0) 
        {
            if (isflyEnemy == true)
            {
                flyEnemy.CollisionDirection(direction);
            }
            if (isflyEnemy == false)
            {
                enemy.CollisionDirection(direction);
            }
            enemySlider.value = 0;
            Death();
        }

    }



    private void Death()
    {

         GameObject coin = Instantiate(lootDrop, transform.position, Quaternion.identity);
         //coin.layer = gameObject.layer;
         SpriteRenderer sr = coin.GetComponent<SpriteRenderer>();
         sr.sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;

        if(isflyEnemy == true)
        {
            flyEnemy.StopMovement();
        }
        if (isflyEnemy == false)
        {
            enemy.StopEnemy();
        }
    }

}
