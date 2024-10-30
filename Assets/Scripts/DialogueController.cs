using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static WaveSpawner;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private string[] dialougue;
    [SerializeField] private TMP_Text dialougueText;
    private int index;
    [SerializeField] private GameObject dialouguePanel;
    public GameObject contButton;
    public WaveSpawner waveSpawner;
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
            zeroText();
        }
    }


    public void StartTalking(Wave wave)
    {
        if (wave.name == "Wave1")
        {
            WaveSpawner.Wave wave1;
            dialouguePanel.SetActive(true);
            StartCoroutine(TextApperanceSpeed());
        }
        
    }


    // Update is called once per frame
    void Update()
    {
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
