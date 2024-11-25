using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private bool isWinScreen;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void WinScreen()
    {
        if (isWinScreen && anim != null)
        {
            anim.SetTrigger("End");
        }
    }

    public void LoseScreen()
    {
        if (!isWinScreen && anim != null)
        {
            anim.SetTrigger("End");
        }
    }
}
