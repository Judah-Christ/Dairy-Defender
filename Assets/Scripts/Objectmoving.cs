using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveSpawner;

public class Objectmoving : MonoBehaviour
{
   

    public Objective[] objectives;
    public Transform[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        WaveSpawner.waveUpdated += HandleWaveUpdated;
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    void SpawnObjective(Transform objective)
    {
        Debug.Log("spawning objective:" + objective.name);


        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(objective, sp.position, sp.rotation);

    }
    
    private void HandleWaveUpdated(int waveU)
    {
        if (waveU == 1)
        {
            SpawnObjective(objectives[Random.Range(0, spawnPoints.Length)].objective);
            Destroy(gameObject);
            
        }
    }

    private void OnDestroy()
    {
        WaveSpawner.waveUpdated -= HandleWaveUpdated;
    }
}
