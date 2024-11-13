using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy1;
        public Transform enemy2;
        public int count;
        public float rate;
        
    }

    public Wave[] waves;
    public int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 30f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.counting;

    public static Action<int> waveUpdated;

    void Start()
    {

        waveCountdown = timeBetweenWaves;

        
    }

    void Update()
    {
        if (state == SpawnState.waiting)
        {
            if(!enemyisAlive()) 
            {
               WaveCompleted();
                
            }
            else
            {
                return;
            }
        }


        if (waveCountdown <= 0)
        {
            if (state != SpawnState.spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length + 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
            waveUpdated?.Invoke(nextWave);
        }  
    }

    bool enemyisAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;

}

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.spawning;
        for(int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy1);
            SpawnEnemyFly(wave.enemy2);
            yield return new WaitForSeconds(1f/wave.rate);
        }

        state = SpawnState.waiting;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {        
        if (nextWave == 0)
        {
            Transform sp = spawnPoints[Random.Range(0, 5)];
            Instantiate(enemy, sp.position, sp.rotation);
        }if(nextWave == 1)
        {
            Transform sp = spawnPoints[Random.Range(0, 5)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 2)
        {
            Transform sp = spawnPoints[Random.Range(0, 5)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 3)
        {
            Transform sp = spawnPoints[Random.Range(0, 5)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 4)
        {
            Transform sp = spawnPoints[Random.Range(0, 5)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
       
    }
    void SpawnEnemyFly(Transform enemy)
    {
        if (nextWave == 0)
        {
            Transform sp = spawnPoints[Random.Range(6, spawnPoints.Length)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 1)
        {
            Transform sp = spawnPoints[Random.Range(6, spawnPoints.Length)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 2)
        {
            Transform sp = spawnPoints[Random.Range(6, spawnPoints.Length)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 3)
        {
            Transform sp = spawnPoints[Random.Range(6, spawnPoints.Length)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 4)
        {
            Transform sp = spawnPoints[Random.Range(6, spawnPoints.Length)];
            Instantiate(enemy, sp.position, sp.rotation);
        }

    }
}
