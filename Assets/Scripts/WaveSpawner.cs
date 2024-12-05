using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Cinemachine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting };
    [SerializeField] CinemachineVirtualCamera Mapcam;
    [SerializeField] private TextMeshProUGUI waveInfo;
    [SerializeField] private TextMeshProUGUI numRatsLeft;
    [SerializeField] private TextMeshProUGUI numFliesLeft;
    [SerializeField] private GameObject waveStarting;
    [SerializeField] private GameObject waveComplete;
    [SerializeField] private GameObject ratsLeft;
    [SerializeField] private GameObject fliesLeft;
    private PlayerController playerController;
    public int countdownTime;
    public int ratsRemaining;
    public int fliesRemaining;

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
    public int WaveCount = 1;

    public Transform[] spawnPoints;

    public float timeBetweenWaves;
    private float waveCountdown;

    private float searchCountdown = 1f;

    public SpawnState state = SpawnState.counting;

    public static Action<int> waveUpdated;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("no spawn points");
        }

        waveCountdown = timeBetweenWaves;
       


        waveStarting.SetActive(false);
        waveComplete.SetActive(false);
        ratsLeft.SetActive(false);
        fliesLeft.SetActive(false);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (state == SpawnState.waiting)
        {
            if (!enemyisAlive())
            {
                WaveCompleted();
                WaveCount++;
               

            }
            else
            {
                return;
            }
        }

        if(WaveCount == 1 && state == SpawnState.counting)
        {
            timeBetweenWaves = 30;
            countdownTime = 30;
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
                waveInfo.text = "Wave: " + (WaveCount) + " of 5";
                StartCoroutine(MapcamCall());
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

        if (playerController.upgradeMenuIsOpen)
        {
            ratsLeft.SetActive(false);
            fliesLeft.SetActive(false);
            waveInfo.enabled = false;
        }
        else if (!playerController.upgradeMenuIsOpen)
        {
            ratsLeft.SetActive(true);
            fliesLeft.SetActive(true);
            waveInfo.enabled = true;
        }

        numRatsLeft.text = "" + ratsRemaining;
        numFliesLeft.text = "" + fliesRemaining;
    }

   public void WaveCompleted()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.roundWin, this.transform.position);
        ratsLeft.SetActive(false);
        fliesLeft.SetActive(false);
        StartCoroutine(ShowPostWaveText());
        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;
        StartCoroutine(CountdownFrom30());
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
        AudioManager.instance.PlayOneShot(FMODEvents.instance.waveStart, this.transform.position);
        ratsRemaining = waves[nextWave].count;
        fliesRemaining = waves[nextWave].count;
        ratsLeft.SetActive(true);
        fliesLeft.SetActive(true);
        numRatsLeft.text = "" + ratsRemaining;
        numFliesLeft.text = "" + fliesRemaining;
        StartCoroutine(ShowPreWaveText());
       


        state = SpawnState.spawning;
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy1);
            SpawnEnemyFly(wave.enemy2);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        state = SpawnState.waiting;
        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {

        if (nextWave == 0)
        {
            Transform sp = spawnPoints[Random.Range(3, 5)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 1)
        {
            Transform sp = spawnPoints[Random.Range(9, 12)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 2)
        {
            Transform sp = spawnPoints[Random.Range(17, 21)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 3)
        {
            Transform sp = spawnPoints[Random.Range(27, 32)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 4)
        {
            Transform sp = spawnPoints[Random.Range(39, 45)];
            Instantiate(enemy, sp.position, sp.rotation);
        }

    }
    void SpawnEnemyFly(Transform enemy)
    {
       

        if (nextWave == 0)
        {
            Transform sp = spawnPoints[Random.Range(0, 2)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 1)
        {
            Transform sp = spawnPoints[Random.Range(6, 8)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 2)
        {
            Transform sp = spawnPoints[Random.Range(13, 16)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 3)
        {
            Transform sp = spawnPoints[Random.Range(33, 38)];
            Instantiate(enemy, sp.position, sp.rotation);
        }
        if (nextWave == 4)
        {
            Transform sp = spawnPoints[Random.Range(6, spawnPoints.Length)];
            Instantiate(enemy, sp.position, sp.rotation);
        }

    }

    IEnumerator MapcamCall()
    {
        Mapcam.enabled = true;
        yield return new WaitForSeconds(5f);
        Mapcam.enabled = false;
    }

    public IEnumerator CountdownFrom30()
    {
      
        for (int i = countdownTime; i > 0; i--)
        {
            waveInfo.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        if (nextWave + 1 > waves.Length + 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
            waveUpdated?.Invoke(nextWave);
        }
    }

    IEnumerator ShowPreWaveText()
    {
        waveStarting.SetActive(true);
        yield return new WaitForSeconds(5f);
        waveStarting.SetActive(false);
    }

    IEnumerator ShowPostWaveText()
    {
        waveComplete.SetActive(true);
        yield return new WaitForSeconds(5f);
        waveComplete.SetActive(false);
    }
}

