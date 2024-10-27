using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] int maxHealth = 1000;
    private GameManager GM;
    public Slider objectiveHealthSlider;
    public Image objSliderFill;
    public Color highHealthColor;
    public Color mediumHealthColor;
    public Color lowHealthColor;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
        objectiveHealthSlider.maxValue = maxHealth;
        objectiveHealthSlider.value = maxHealth;
        objSliderFill.color = highHealthColor;
    }
    private void Update()
    {
        //Debug.Log(currentHealth);
    }

    public void HealthSliderUpdate()
    {
        objectiveHealthSlider.value = currentHealth;

        if (currentHealth > 0.66 * maxHealth)
        {
            objSliderFill.color = highHealthColor;
        }
        else if (currentHealth > 0.33 * maxHealth)
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
        

        if(currentHealth <= 0)
        {
            StartCoroutine(AudioManager.instance.FadeOut());
            GM.ObjectiveFailed();
            Destroy(gameObject);
            Debug.Log("TEST");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Counter")
        {
            TakeDamage(50);
            collision.gameObject.GetComponent<EnemyManager>().TakeDamage(100);

        }
    }

}
