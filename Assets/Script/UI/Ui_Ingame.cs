using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Ingame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] TextMeshProUGUI coinsTxt;
    [SerializeField] TextMeshProUGUI StarsTxt;

    [SerializeField] Button PauseBtn;

    private void OnEnable()
    {
        GameEvent.OnCoinChanged += UpdateCoinUI;
        GameEvent.OnStarChanged += UpdateStarUI;
    }

    private void Start()
    {
        PauseBtn.onClick.AddListener(PauseGame);
    }

    private void OnDisable()
    {
        GameEvent.OnCoinChanged -= UpdateCoinUI;
        GameEvent.OnStarChanged -= UpdateStarUI;
    }

    private void UpdateCoinUI(int newCoins)
    { 
        coinsTxt.text = "Coin: " + newCoins.ToString();
    }
    private void UpdateStarUI(int newStars)
    {
        StarsTxt.text = "Stars: " + newStars.ToString();
    }

    private void PauseGame()
    {

    }
}
