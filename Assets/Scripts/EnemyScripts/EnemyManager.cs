using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        deathScreams = new AudioClip[] {deathScream, deathScream1, deathScream2, deathScream3};
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
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
    }

    public void TakeDamage(int damageAmount)
    {
        currenthealth -= damageAmount;

        if (currenthealth <= 0) 
        { 
            int i = Random.Range(0, deathScreams.Length);
            audioSource.PlayOneShot(deathScreams[i]);
            StartCoroutine(Death(i));
        }
    }

    IEnumerator Death(int i)
    {
            audioSource.PlayOneShot(coinDrop);
            GameObject coin = Instantiate(lootDrop, transform.position, Quaternion.identity);
            //coin.layer = gameObject.layer;
            SpriteRenderer sr = coin.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
            yield return new WaitForSeconds(deathScreams[i].length);
            Destroy(gameObject);
    }
   
   

}
