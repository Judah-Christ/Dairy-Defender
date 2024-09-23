using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (!GM.isGamePaused)
        {
            yield return wait;
            int rand = Random.Range(0,enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            Instantiate(enemyToSpawn,transform.position,Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
