using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static WaveSpawner;

public class GameManager : MonoBehaviour
{
    public List<GameItem> Towers;
    public List<GameItem> Ammo;

    public static GameManager gameManager;
    private ObjectiveManager objectiveM;
    private Objectmoving objectmoving;
    private WaveSpawner waveSpawner;
    public int Coins;
    public int Tower;
    public bool isGamePaused;
    public int objectivesLeft = 1;
    public List<Transform> activeObject;

    [SerializeField] private GameObject WinMenu;
    private GameObject WinMenu;
    private GameObject LoseMenu;

    [SerializeField] private float objectiveTimer;
    private float origTimer;
    private object wavesDone;

    // Start is called before the first frame update
    void Start()
    {
        origTimer = objectiveTimer;
        StartWaves();
        AudioManager.instance.PlayMusic("DDBattleLoop");
      
        WinMenu = GameObject.Find("WinMenuCanvas");
        WaveSpawner.waveUpdated += HandleWaveUpdated;
      
        WinMenu.SetActive(false);
        LoseMenu = GameObject.Find("LoseMenuCanvas");
        LoseMenu.SetActive(false);
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
    public void AddToObjectiveList(Transform added)
    {
        activeObject.Add(added);
    }

    public void lose()
    {
        objectivesLeft--;
        
        if (objectivesLeft == 0)
        {
            ObjectiveFailed();
        }
       
    }
    
      
    public void ObjectiveComplete()
    {
        EndWaves();
        //StartCoroutine(AudioManager.instance.FadeOut());
        //SceneManager.LoadScene(3);
        WinMenu.SetActive(true);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.roundWin, this.transform.position);
    }

    public void ObjectiveFailed()
    {
        EndWaves();
        //SceneManager.LoadScene(4);
        LoseMenu.SetActive(true);
    }
    private void HandleWaveUpdated(int waveU)
    {
        if (waveU == 1)
        {

            objectivesLeft++;
            Debug.Log(objectivesLeft);

        }
        if (waveU == 2)
        {
            objectivesLeft++;

        }
        if (waveU == 3)
        {
            objectivesLeft++;
        }
        if (waveU == 4)
        {
           
            objectivesLeft++;
            if (objectivesLeft >= 1  )
            {
                waveSpawner.WaveCompleted();
               
            }
           

        }
        if (waveU == 5)
        {
            ObjectiveComplete();
        }



    }
    private void OnDestroy()
    {
        WaveSpawner.waveUpdated -= HandleWaveUpdated;
    }

}
