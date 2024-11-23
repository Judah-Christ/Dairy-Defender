using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        deathScreams = new AudioClip[] {deathScream, deathScream1, deathScream2, deathScream3};
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        enemySliderFill.color = highHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (pc.isPaused) 
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
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
        currenthealth -= damageAmount;
        enemySlider.value = currenthealth;

        if (currenthealth <= 0) 
        { 
            int i = Random.Range(0, deathScreams.Length);
            //audioSource.PlayOneShot(deathScreams[i]);
            StartCoroutine(Death(i));
        }
    }

   

    IEnumerator Death(int i)
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

    internal void ConstantAttack(int v)
    {
        throw new System.NotImplementedException();
    }
}
