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
    public Image objSliderFill;
    public Color highHealthColor;
    public Color mediumHealthColor;
    public Color lowHealthColor;
    public Objectmoving objectmoving;
    

    [SerializeField]
    private GameObject CakeFull;

    [SerializeField]
    private GameObject CakeHalf;

    [SerializeField]
    private GameObject CakeCrit;

    // Start is called before the first frame update
    void Start()
    {
        objectmoving = GameObject.Find("GameManager").GetComponent<Objectmoving>();
        objectiveHealthSlider = objectmoving.objectiveHealthSlider;
        objSliderFill = objectmoving.objSliderFill;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
        objectiveHealthSlider.maxValue = maxHealth;
        objectiveHealthSlider.value = maxHealth;
        objSliderFill.GetComponent<Image>().color = highHealthColor;
    }
    private void FixedUpdate()
    {
        //Debug.Log(currentHealth);
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
       // HealthSliderUpdate();
        
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
            GM.ObjectiveFailed();
            Destroy(gameObject);
            Debug.Log("TEST");
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
