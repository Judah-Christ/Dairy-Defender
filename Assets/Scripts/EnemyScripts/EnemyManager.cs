using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

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
    private AudioSource audioSource;
    private AudioClip[] deathScreams;
    private SpriteRenderer sr;
    private GameObject coin;
    private PlayerController pc;

    [SerializeField] private Color highHealth;
    [SerializeField] private Color mediumHealth;
    [SerializeField] private Color lowHealth;
    [SerializeField] private Slider enemySlider;
    [SerializeField] private Image enemySliderFill;

    [SerializeField] private Canvas canvas;

    private StudioEventEmitter emitter;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        deathScreams = new AudioClip[] {deathScream, deathScream1, deathScream2, deathScream3};
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        enemySliderFill.color = highHealth;

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
        if (pc.isPaused) 
        {
            //audioSource.Pause();
        }
        else
        {
            //audioSource.UnPause();
        }

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

    public void TakeDamage(int damageAmount)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.enemyHit, this.transform.position);
        currenthealth -= damageAmount;
        enemySlider.value = currenthealth;

        if (currenthealth <= 0) 
        {
            if (gameObject.name == "RatEnemy(Clone)")
            {
                AudioManager.instance.PlayOneShot(FMODEvents.instance.ratDeathScreams, this.transform.position);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonDrop, this.transform.position);
            }
            else if (gameObject.name == "FlyEnemy(Clone)")
            {
                //emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.flyBuzzing, this.gameObject);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonDrop, this.transform.position);
            }
            emitter.Stop();
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
         //audioSource.PlayOneShot(coinDrop);
         GameObject coin = Instantiate(lootDrop, transform.position, Quaternion.identity);
         //coin.layer = gameObject.layer;
         SpriteRenderer sr = coin.GetComponent<SpriteRenderer>();
         sr.sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
         //yield return new WaitForSeconds(deathScreams[i].length);
         yield return new WaitForSeconds(3);
         Destroy(gameObject);
    }
   
   

}
