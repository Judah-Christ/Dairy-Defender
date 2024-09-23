using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameItem> Towers;
    public List<GameItem> Ammo;

    public static GameManager gameManager;
    public int Coins;
    public int Tower;
    public bool isGamePaused;

    [SerializeField] private float objectiveTimer;
    private float origTimer;

    // Start is called before the first frame update
    void Start()
    {
        origTimer = objectiveTimer;
        StartWaves();
    }

    public void AddCoin(int amount)
    {
        Coins += amount;
        return;
    }

    public void RemoveCoin(int amount)
    {
        Coins -= amount;
        return;
    }

    public void StartWaves()
    {
        isGamePaused = false;

    }

    public void EndWaves()
    {
        isGamePaused = true;
    }

    private void FixedUpdate()
    {
        if (!isGamePaused)
        {
            if(objectiveTimer > 0)
            {
                objectiveTimer -= Time.deltaTime;
            }
            if(objectiveTimer < 0)
            {
                ObjectiveComplete();
            }
        }
    }

    public void ObjectiveComplete()
    {
        EndWaves();
        Debug.Log("Objective Complete!");
    }

    public void ObjectiveFailed()
    {
        EndWaves();
        Debug.Log("Objective Failed!");
    }

}
