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
    private int pointState = 0;

    public GameObject objectiveSet;

    public Slider objectiveHealthSlider;
    public Image objSliderFill;


    // Start is called before the first frame update
    void Start()
    {
        SpawnObjective(objectives[pointState].objective);
        WaveSpawner.waveUpdated += HandleWaveUpdated;
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    void SpawnObjective(Transform objective)
    {
     
        Transform sp = spawnPoints[pointState];
        pointState++;
        objectiveSet = Instantiate(objective, sp.position, sp.rotation).gameObject;
       

    }
    
    private void HandleWaveUpdated(int waveU)
    {
        if (waveU == 1)
        {
            Destroy(objectiveSet);
            SpawnObjective(objectives[pointState].objective);
        }
        if(waveU == 2)
        {
            Destroy(objectiveSet);
            SpawnObjective(objectives[pointState].objective);
        }
        if(waveU == 3)
        {
            Destroy(objectiveSet);
            SpawnObjective(objectives[pointState].objective);
        }
        if (waveU == 4)
        {
            Destroy(objectiveSet);
            SpawnObjective(objectives[pointState].objective);
        }
    }

    private void OnDestroy()
    {
        WaveSpawner.waveUpdated -= HandleWaveUpdated;
    }
}
