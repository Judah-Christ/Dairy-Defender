using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 30f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.counting;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("no spawn points");
        }

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
        Debug.Log("waveCompleted");
        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length + 1)
        {
            nextWave = 0;
            Debug.Log("all waves done looping");
        }
        else
        {
            nextWave++;
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
        Debug.Log("spawning Wave" + wave.name);
        state = SpawnState.spawning;
        for(int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate);
        }

        state = SpawnState.waiting;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        Debug.Log("spawning enemy:" + enemy.name);

       
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
      Instantiate(enemy, sp.position, sp.rotation);
         
    }
}
