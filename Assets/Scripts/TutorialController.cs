using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private string[] tutorialMove;
    [SerializeField] private string[] tutorialJump;
    [SerializeField] private string[] tutorialPlacement;
    private int moveIndex;
    private int jumpIndex;
    private int placementIndex;
    [SerializeField] private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveZeroText()
    {
        tutorialText.text = "";
        moveIndex = 0;
    }

    public void JumpZeroText()
    {
        tutorialText.text = "";
        jumpIndex = 0;
    }

    public void PlacementZeroText()
    {
        tutorialText.text = "";
        placementIndex = 0;
    }




    public void MoveNextLine()
    {
        if(moveIndex < tutorialMove.Length - 1)
        {
            moveIndex++;
            tutorialText.text = "";
            StartCoroutine(MoveTextApperanceSpeed());
        }
    }

    IEnumerator MoveTextApperanceSpeed()
    {
        foreach (char letter in tutorialMove[moveIndex].ToCharArray())
        {
            tutorialText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
