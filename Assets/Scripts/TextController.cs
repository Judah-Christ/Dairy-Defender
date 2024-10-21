using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public TMP_Text coinText;
    private int currentCoinTotal;

    // Start is called before the first frame update
    void Start()
    {
        currentCoinTotal = gameManager.Coins;
        coinText.text = gameManager.Coins.ToString();
    }

    public void UpdateCoins()
    {
        currentCoinTotal = gameManager.Coins;
        coinText.text = currentCoinTotal.ToString();
        //if (currentCoinTotal < gameManager.Coins)
        //{
        //    currentCoinTotal = gameManager.Coins;
        //    coinText.text = currentCoinTotal.ToString();
            
        //}
    }


    // Update is called once per frame
    void Update()
    {
        UpdateCoins();
    }
}
