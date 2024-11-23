using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameItem> Towers;
    public List<GameItem> Ammo;

    public static GameManager gameManager;
    public int Coins;
    public int Tower;
    public bool isGamePaused;
    private GameObject WinMenu;
    private GameObject LoseMenu;

    [SerializeField] private float objectiveTimer;
    private float origTimer;

    // Start is called before the first frame update
    void Start()
    {
        origTimer = objectiveTimer;
        StartWaves();
        //AudioManager.instance.PlayMusic("DDBattleLoop");
        WinMenu = GameObject.Find("WinMenuCanvas");
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

    private void FixedUpdate()
    {
        if (!isGamePaused)
        {
            //if(objectiveTimer > 0)
            //{
            //    objectiveTimer -= Time.deltaTime;
            //}
            //if(objectiveTimer < 0)
            //{
            //    ObjectiveComplete();
            //}
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

}
