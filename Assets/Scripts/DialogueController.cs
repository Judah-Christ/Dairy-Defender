using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static WaveSpawner;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private string[] dialougue;
    [SerializeField] private string[] wave3Dialougue;
    [SerializeField] private string[] PostWaveDialougue;
    [SerializeField] private TMP_Text dialougueText;
    private int index;
    [SerializeField] private GameObject dialouguePanel;
    public GameObject contButton;
    //public WaveSpawner waveSpawner;
    [SerializeField] private GameObject gameController;
    private bool startFirstTalk = true;
    //private bool startSecondTalk2 = false;
    // Start is called before the first frame update
    void Start()
    {
        dialougueText.text = "";
    }

    public void zeroText()
    {
        dialougueText.text = "";
        index = 0;
        dialouguePanel.SetActive(false);
    }

    IEnumerator TextApperanceSpeed()
    {
        foreach(char letter in dialougue[index].ToCharArray())
        {
            dialougueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);
        if (index < dialougue.Length - 1)
        {
            index++;
            dialougueText.text = "";
            StartCoroutine(TextApperanceSpeed());
        }
        else
        {
            gameController.GetComponent<WaveSpawner>().enabled = true;
            zeroText();
        }
    }


    public void NextWave3Line()
    {
        contButton.SetActive(false);
        if (index < wave3Dialougue.Length - 1)
        {
            index++;
            dialougueText.text = "";
            StartCoroutine(TextApperanceSpeed());
        }
        else
        {
            gameController.GetComponent<WaveSpawner>().enabled = true;
            zeroText();
        }
    }



    public void StartTalking()
    {
        startFirstTalk = false;
        dialouguePanel.SetActive(true);
        StartCoroutine(TextApperanceSpeed());
        //if (waveSpawner.state == SpawnState.waiting)
        //{
        //    startFirstTalk = false;
        //    dialouguePanel.SetActive(true);
        //    StartCoroutine(TextApperanceSpeed());
        //}
        
    }


    // Update is called once per frame
    void Update()
    {
        if(startFirstTalk == true)
        {
            StartTalking();
        }
        
        //if(dialouguePanel.activeInHierarchy)
        //{

        //}
        //else
        //{
        //    dialouguePanel.SetActive(true);
        //    StartCoroutine(TextApperanceSpeed());
        //}

        if (dialougueText.text == dialougue[index])
        {
            contButton.SetActive(true);
        }
    }
}
