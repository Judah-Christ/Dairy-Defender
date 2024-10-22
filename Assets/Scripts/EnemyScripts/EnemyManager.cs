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
    public AudioClip deathScream4;
    public AudioClip deathScream5;
    public AudioClip deathScream6;
    public AudioClip deathScream7;
    public AudioClip coinDrop;
    private AudioSource audioSource;
    private AudioClip[] deathScreams;
    private SpriteRenderer sr;
    private GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        deathScreams = new AudioClip[] {deathScream, deathScream1, deathScream2, deathScream3, deathScream4, deathScream5, deathScream6, deathScream7};
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GameObject coin = Instantiate(lootDrop,transform.position, Quaternion.identity);
            coin.layer = gameObject.layer;
            SpriteRenderer sr = coin.GetComponent<SpriteRenderer>();
            sr.sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
            yield return new WaitForSeconds(deathScreams[i].length);
            Destroy(gameObject);
    }
   
   

}
