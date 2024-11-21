using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] int maxHealth = 1000;
    private GameManager GM;
    public Slider objectiveHealthSlider;
    public Slider objectiveHealthSlider2;
    public Slider objectiveHealthSlider3;
    public Slider objectiveHealthSlider4;
    public Slider objectiveHealthSlider5;
    public Image objSliderFill;
    public Color highHealthColor;
    public Color mediumHealthColor;
    public Color lowHealthColor;
    public Objectmoving objectmoving;
    private WaveSpawner waveSpawner;
    private GameObject wave1Canvas;
    private GameObject wave2Canvas;
    private GameObject wave3Canvas;
    private GameObject wave4Canvas;
    private GameObject wave5Canvas;
    

    [SerializeField]
    private GameObject CakeFull;

    [SerializeField]
    private GameObject CakeHalf;

    [SerializeField]
    private GameObject CakeCrit;

    // Start is called before the first frame update
    void Start()
    {
        wave1Canvas = GameObject.Find("Wave1Canvas");
        wave2Canvas = GameObject.Find("Wave2Canvas");
        wave3Canvas = GameObject.Find("Wave3Canvas");
        wave4Canvas = GameObject.Find("Wave4Canvas");
        wave5Canvas = GameObject.Find("Wave5Canvas");
        objectiveHealthSlider = GameObject.Find("ObjectiveSlider").GetComponent<Slider>();
        objectiveHealthSlider2 = GameObject.Find("ObjectiveSlider2").GetComponent<Slider>();
        objectiveHealthSlider3 = GameObject.Find("ObjectiveSlider3").GetComponent<Slider>();
        objectiveHealthSlider4 = GameObject.Find("ObjectiveSlider4").GetComponent<Slider>();
        objectiveHealthSlider5 = GameObject.Find("ObjectiveSlider5").GetComponent<Slider>();
        objSliderFill = GameObject.Find("ObjectiveSliderFill").GetComponent<Image>();
        objectmoving = GameObject.Find("GameManager").GetComponent<Objectmoving>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //objectiveHealthSlider = objectmoving.objectiveHealthSlider;
        //objSliderFill = objectmoving.objSliderFill;             
        currentHealth = maxHealth;
        objectiveHealthSlider.maxValue = maxHealth;
        objectiveHealthSlider.value = maxHealth;
        objSliderFill.GetComponent<Image>().color = highHealthColor;

        waveSpawner = GameObject.Find("GameManager").GetComponent<WaveSpawner>();
        wave2Canvas.SetActive(false);
        wave3Canvas.SetActive(false);
        wave4Canvas.SetActive(false);
        wave5Canvas.SetActive(false);

    }
    private void FixedUpdate()
    {
        if (waveSpawner.nextWave == 1)
        {
            GameObject.Find("Wave1Canvas").SetActive(false);
            GameObject.Find("Wave2Canvas").SetActive(true);
            objectiveHealthSlider = GameObject.Find("W2ObjectiveSlider").GetComponent<Slider>();
        }

        if (waveSpawner.nextWave == 2)
        {

        }

        if (waveSpawner.nextWave == 3)
        {

        }

        if (waveSpawner.nextWave == 4)
        {

        }

        HealthSliderUpdate();

    }

    public void HealthSliderUpdate()
    {
        objectiveHealthSlider.value = currentHealth;

        if (currentHealth >= 0.66 * maxHealth)
        {
            objSliderFill.color = highHealthColor;
        }
        else if (currentHealth >= 0.33 * maxHealth)
        {
            objSliderFill.color = mediumHealthColor;
        }
        else
        {
            objSliderFill.color = lowHealthColor;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        HealthSliderUpdate();
        
        if(currentHealth <= (maxHealth / 2))
        {
            CakeFull.SetActive(false);
            CakeHalf.SetActive(true);
            CakeCrit.SetActive(false);
        }

        if (currentHealth <= (maxHealth / 4))
        {
            CakeFull.SetActive(false);
            CakeHalf.SetActive(false);
            CakeCrit.SetActive(true);
        }

        if (currentHealth <= 0)
        {
            //StartCoroutine(AudioManager.instance.FadeOut());
            GM.ObjectiveFailed();
            Destroy(gameObject);
        }
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(50);
            //collision.gameObject.GetComponent<EnemyManager>().TakeDamage(100);

        }
    }

}
