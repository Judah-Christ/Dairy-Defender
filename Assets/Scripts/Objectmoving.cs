using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static WaveSpawner;

public class Objectmoving : MonoBehaviour
{
   

    public Objective[] objectives;
    public Transform[] spawnPoints;
    public Transform[] activeObjectives;
    public int point = 0;
    public GameObject objectiveSet;
    private GameManager gameManager;

    public Slider objectiveHealthSlider;
    public Image objSliderFill;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SpawnObjective(objectives[0].objective);
        
        WaveSpawner.waveUpdated += HandleWaveUpdated;
     
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    void SpawnObjective(Transform objective)
    {
     
        Transform sp = spawnPoints[point];
        point++;
        objectiveSet = Instantiate(objective, sp.position, sp.rotation).gameObject;
        gameManager.AddToObjectiveList(objectiveSet.transform);
       

    }
    
    private void HandleWaveUpdated(int waveU)
    {
        if (waveU == 1)
        {
            //Destroy(objectiveSet);
            SpawnObjective(objectives[point].objective);

        }

            if (waveU == 2)
            {
                // Destroy(objectiveSet);
                SpawnObjective(objectives[point].objective);

            }
            if (waveU == 3)
            {
                // Destroy(objectiveSet);
                SpawnObjective(objectives[point].objective);

            }
            if (waveU == 4)
            {
                // Destroy(objectiveSet);
                SpawnObjective(objectives[point].objective);

            }

        
    }

    private void OnDestroy()
    {
        WaveSpawner.waveUpdated -= HandleWaveUpdated;
    }
}
