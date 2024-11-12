using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static WaveSpawner;

public class Objectmoving : MonoBehaviour
{
   

    public Objective[] objectives;
    public Transform[] spawnPoints;

    public GameObject objectiveSet;


    // Start is called before the first frame update
    void Start()
    {
        SpawnObjective(objectives[Random.Range(0, 0)].objective);
        WaveSpawner.waveUpdated += HandleWaveUpdated;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    void SpawnObjective(Transform objective)
    {
        Debug.Log("spawning objective:" + objective.name);
        objectiveSet = objective.gameObject;
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(objective, sp.position, sp.rotation);
       

    }
    
    private void HandleWaveUpdated(int waveU)
    {
        if (waveU == 1)
        {
            Destroy(objectiveSet);
            SpawnObjective(objectives[Random.Range(1, 1)].objective);
           
        }
    }

    private void OnDestroy()
    {
        WaveSpawner.waveUpdated -= HandleWaveUpdated;
    }
}
