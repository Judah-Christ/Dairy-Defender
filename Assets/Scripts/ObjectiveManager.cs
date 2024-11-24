using System.Collections;
using System.Drawing;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveManager : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] int maxHealth = 1000;
    private int currentWave;
    private GameManager GM;
    private Slider objectiveHealthSlider;
    private Slider objectiveHealthSlider2;
    private Slider objectiveHealthSlider3;
    private Slider objectiveHealthSlider4;
    private Slider objectiveHealthSlider5;
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
    
    public EnemyManager enemyManager;




    [SerializeField]
    private GameObject CakeFull;

    [SerializeField]
    private GameObject CakeHalf;

    [SerializeField]
    private GameObject CakeCrit;

    // Start is called before the first frame update
    void Start()
    {
        objSliderFill = GameObject.Find("ObjectiveSliderFill").GetComponent<Image>();
        objectmoving = GameObject.Find("GameManager").GetComponent<Objectmoving>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //objectiveHealthSlider = objectmoving.objectiveHealthSlider;
        //objSliderFill = objectmoving.objSliderFill;             
        currentHealth = maxHealth;

        waveSpawner = GM.GetComponent<WaveSpawner>();
        SetUpWaveCanvas();

    }
    private void FixedUpdate()
    {
        if (waveSpawner != null)
        {
            if(currentWave != waveSpawner.nextWave)
            {
                SwitchWave();
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
                    case 4:
                        currentHealthSlider = objectiveHealthSlider5;
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


    private void SetUpWaveCanvas()
    {
        wave1Canvas = GM.WaveCanvas[0];
        wave1Canvas.SetActive(false);
        wave2Canvas = GM.WaveCanvas[1];
        wave2Canvas.SetActive(false);
        wave3Canvas = GM.WaveCanvas[2];
        wave3Canvas.SetActive(false);
        wave4Canvas = GM.WaveCanvas[3];
        wave4Canvas.SetActive(false);
        wave5Canvas = GM.WaveCanvas[4];
        wave5Canvas.SetActive(false);  
        
        if (waveSpawner.nextWave == 0)
        {
            SwitchWave();
        }
    }


    public void SwitchWave()
    {

        switch (waveSpawner.nextWave)
        {
            case 0:
                wave1Canvas.SetActive(true);
                objectiveHealthSlider = GM.WaveCanvas[0].GetComponent<WaveCanvasController>().ObjSliders[0].GetComponent<Slider>();
                objectiveHealthSlider.maxValue = maxHealth;
                objectiveHealthSlider.value = maxHealth;
                objSliderFill.GetComponent<Image>().color = highHealthColor;
                GetCurrentObjective();
                currentWave = 0;
                break;
            case 1:
                wave1Canvas.SetActive(false);
                wave2Canvas.SetActive(true);
                objectiveHealthSlider = GM.WaveCanvas[1].GetComponent<WaveCanvasController>().ObjSliders[0].GetComponent<Slider>();
                objectiveHealthSlider2 = GM.WaveCanvas[1].GetComponent<WaveCanvasController>().ObjSliders[1].GetComponent<Slider>();
                GetCurrentObjective();
                currentWave = 1;
                return;
            case 2:
                wave2Canvas.SetActive(false);
                wave3Canvas.SetActive(true);
                objectiveHealthSlider = GM.WaveCanvas[2].GetComponent<WaveCanvasController>().ObjSliders[0].GetComponent<Slider>();
                objectiveHealthSlider2 = GM.WaveCanvas[2].GetComponent<WaveCanvasController>().ObjSliders[1].GetComponent<Slider>();
                objectiveHealthSlider3 = GM.WaveCanvas[2].GetComponent<WaveCanvasController>().ObjSliders[2].GetComponent<Slider>();
                GetCurrentObjective();
                currentWave = 2;
                return;
            case 3:
                wave3Canvas.SetActive(false);
                wave4Canvas.SetActive(true);
                objectiveHealthSlider = GM.WaveCanvas[3].GetComponent<WaveCanvasController>().ObjSliders[0].GetComponent<Slider>();
                objectiveHealthSlider2 = GM.WaveCanvas[3].GetComponent<WaveCanvasController>().ObjSliders[1].GetComponent<Slider>();
                objectiveHealthSlider3 = GM.WaveCanvas[3].GetComponent<WaveCanvasController>().ObjSliders[2].GetComponent<Slider>();
                objectiveHealthSlider4 = GM.WaveCanvas[3].GetComponent<WaveCanvasController>().ObjSliders[3].GetComponent<Slider>();
                GetCurrentObjective();
                currentWave = 3;
                return;
            case 4:
                wave4Canvas.SetActive(false);
                wave5Canvas.SetActive(true);
                objectiveHealthSlider = GM.WaveCanvas[4].GetComponent<WaveCanvasController>().ObjSliders[0].GetComponent<Slider>();
                objectiveHealthSlider2 = GM.WaveCanvas[4].GetComponent<WaveCanvasController>().ObjSliders[1].GetComponent<Slider>();
                objectiveHealthSlider3 = GM.WaveCanvas[4].GetComponent<WaveCanvasController>().ObjSliders[2].GetComponent<Slider>();
                objectiveHealthSlider4 = GM.WaveCanvas[4].GetComponent<WaveCanvasController>().ObjSliders[3].GetComponent<Slider>();
                objectiveHealthSlider5 = GM.WaveCanvas[4].GetComponent<WaveCanvasController>().ObjSliders[4].GetComponent<Slider>();
                GetCurrentObjective();
                currentWave = 4;
                return;
            default:
                break;

         }

    }

}
