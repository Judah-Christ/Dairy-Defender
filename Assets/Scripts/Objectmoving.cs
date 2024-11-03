using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectmoving : MonoBehaviour
{
    private ObjectiveManager manager;
    private WaveSpawner waveSpawner;
    public class  Objective{
        public string name;
        public Transform objective;

    }

    public Transform[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (waveSpawner.nextWave == 2)
        {
            SpawnObjective(objective.o
        }
    }
    void SpawnObjective(Transform objective)
    {
        Debug.Log("spawning objective:" + objective.name);


        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(objective, sp.position, sp.rotation);

    }

}
