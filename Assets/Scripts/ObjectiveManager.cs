using System.Drawing;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveManager : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] int maxHealth = 1000;
    private GameManager GM;
    private Slider objectiveHealthSlider;
    private Slider objectiveHealthSlider2;
    private Slider objectiveHealthSlider3;
    private Slider objectiveHealthSlider4;
    private Slider currentHealthSlider;
    private Image objSliderFill;
    public UnityEngine.Color highHealthColor;
    public UnityEngine.Color mediumHealthColor;
    public UnityEngine.Color lowHealthColor;
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
        objectiveHealthSlider = GM.WaveCanvas[0].GetComponentInChildren<Slider>();
        objectiveHealthSlider2 = GM.WaveCanvas[1].GetComponentInChildren<Slider>();
        objectiveHealthSlider3 = GM.WaveCanvas[2].GetComponentInChildren<Slider>();
        objectiveHealthSlider4 = GM.WaveCanvas[3].GetComponentInChildren<Slider>();
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

        GetCurrentObjective();

    }
    private void FixedUpdate()
    {
        if (waveSpawner != null)
        {
            switch(waveSpawner.nextWave)
            {
                case 1:
                    wave1Canvas.SetActive(false);
                    wave2Canvas.SetActive(true);
                    objectiveHealthSlider = GameObject.Find("W2ObjectiveSlider").GetComponent<Slider>();
                    return;
                case 2:
                    wave2Canvas.SetActive(false);
                    wave3Canvas.SetActive(true);
                    objectiveHealthSlider = GameObject.Find("W3ObjectiveSlider").GetComponent<Slider>();
                    objectiveHealthSlider2 = GameObject.Find("W3ObjectiveSlider2").GetComponent<Slider>();
                    return;
                case 3:
                    wave3Canvas.SetActive(false);
                    wave4Canvas.SetActive(true);
                    objectiveHealthSlider = GameObject.Find("W4ObjectiveSlider").GetComponent<Slider>();
                    objectiveHealthSlider2 = GameObject.Find("W4ObjectiveSlider2").GetComponent<Slider>();
                    objectiveHealthSlider3 = GameObject.Find("W4ObjectiveSlider3").GetComponent<Slider>();
                    return;
                case 4:
                    wave4Canvas.SetActive(false);
                    wave5Canvas.SetActive(true);
                    objectiveHealthSlider = GameObject.Find("W5ObjectiveSlider").GetComponent<Slider>();
                    objectiveHealthSlider2 = GameObject.Find("W5ObjectiveSlider2").GetComponent<Slider>();
                    objectiveHealthSlider3 = GameObject.Find("W5ObjectiveSlider3").GetComponent<Slider>();
                    objectiveHealthSlider4 = GameObject.Find("W5ObjectiveSlider4").GetComponent<Slider>();
                    return;
                default:
                    break;

            }
        }

        HealthSliderUpdate();

    }

    public void HealthSliderUpdate()
    {
        if(currentHealthSlider != null)
        {
            currentHealthSlider.value = currentHealth;

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

    }

    private void GetCurrentObjective()
    {
        for(int i = 0;  i < GM.activeObject.Count; i++)
        {
            if (gameObject.transform == GM.activeObject[i])
            {
                switch(i)
                {
                    case 0:
                        currentHealthSlider = objectiveHealthSlider;
                        return;
                    case 1:
                        currentHealthSlider = objectiveHealthSlider2;
                        return;
                    case 2:
                        currentHealthSlider = objectiveHealthSlider3;
                        return;
                    case 3:
                        currentHealthSlider = objectiveHealthSlider4;
                        return;
                    default:
                        break;
                }
            }
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
            FindAnyObjectByType<GameManager>().lose();
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
