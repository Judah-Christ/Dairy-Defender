using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using static WaveSpawner;

public class ObjectiveManager : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] int maxHealth = 1000;
    private GameManager GM;
    public Slider objectiveHealthSlider;
    public Image objSliderFill;
    public UnityEngine.Color highHealthColor;
    public UnityEngine.Color mediumHealthColor;
    public UnityEngine.Color lowHealthColor;
    public Objectmoving objectmoving;

    private int objectivesLeft = 1;


    [SerializeField]
    private GameObject CakeFull;

    [SerializeField]
    private GameObject CakeHalf;

    [SerializeField]
    private GameObject CakeCrit;

    // Start is called before the first frame update
    void Start()
    {
        objectiveHealthSlider = GameObject.Find("ObjectiveSlider").GetComponent<Slider>();
        objSliderFill = GameObject.Find("ObjectiveSliderFill").GetComponent<Image>();
        objectmoving = GameObject.Find("GameManager").GetComponent<Objectmoving>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //objectiveHealthSlider = objectmoving.objectiveHealthSlider;
        //objSliderFill = objectmoving.objSliderFill;             
        currentHealth = maxHealth;
        objectiveHealthSlider.maxValue = maxHealth;
        objectiveHealthSlider.value = maxHealth;
        objSliderFill.GetComponent<Image>().color = highHealthColor;
        WaveSpawner.waveUpdated += HandleWaveUpdated;
    }
    private void FixedUpdate()
    {
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
            StartCoroutine(AudioManager.instance.FadeOut());
           
                objectivesLeft = objectivesLeft - 1;
            if (objectivesLeft == 0)
            {
               GM.ObjectiveFailed();
            }
            Destroy(gameObject);
        }
    }
    private void HandleWaveUpdated(int waveU)
    {
        if (waveU == 1)
        {

            objectivesLeft++;
            Debug.Log(objectivesLeft);

        }
            if (waveU == 2)
            {
                objectivesLeft++;

            }
            if (waveU == 3)
            {
                objectivesLeft++;
            }
            if (waveU == 4)
            {
                objectivesLeft++;

            }

        

    }
    private void OnDestroy()
    {
        WaveSpawner.waveUpdated -= HandleWaveUpdated;
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
