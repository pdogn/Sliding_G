using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    //public static event Action<int> OnLevelChanged;

    public static event Action<int> OnCoinChanged;
    public static event Action<int> OnTotalCoinChanged;

    public static event Action<int> OnStarChanged;
    public static event Action<int> OnTotalStarChanged;

    public static event Action OnDisplayMainUI;
    public static event Action OnDisplaySelectLvUI;

    public static event Action OnPassLevel;
    public static event Action OnFailLevel;

    public static event Action<int> OnReplayLevel;
    public static event Action OnPlayNextLevel;

    //public static void LevelChanged(int level)
    //{
    //    OnLevelChanged?.Invoke(level);
    //}

    public static void CoinChanged(int coins)
    {
        OnCoinChanged?.Invoke(coins);
    }
    public static void TotalCoinChanged(int coins)
    {
        OnTotalCoinChanged?.Invoke(coins);
    }
    public static void StarChanged(int stars)
    {
        OnStarChanged?.Invoke(stars);
    }
    public static void TotalStarChanged(int stars)
    {
        OnTotalStarChanged?.Invoke(stars);
    }

    public static void DisplayMainUI()
    {
        OnDisplayMainUI?.Invoke();
    }
    public static void DisplaySelectLvUI()
    {
        OnDisplaySelectLvUI?.Invoke();
    }

    public static void DisplayPass_LevelUI()
    {
        OnPassLevel?.Invoke();
    }
    public static void DisPlayFail_LevelUI()
    {
        OnFailLevel?.Invoke();
    }
    public static void ReplayLevel(int lvId)
    {
        OnReplayLevel?.Invoke(lvId);
    }
    public static void PlayNextLevel()
    {
        OnPlayNextLevel?.Invoke();
    }
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int crrlevel;
    public int CurrenLevel
    {
        get { return crrlevel; }
        set 
        {
            if(crrlevel != value)
            {
                crrlevel = value;
                //GameEvent.LevelChanged(level);
            }
        }
    }

    private int coins;

    public int Coins
    {
        get { return coins; }
        set 
        { 
            if(coins != value)
            {
                coins = value;
                //OnCoinChanged?.Invoke(coins);
                GameEvent.CoinChanged(coins);
            }
        }
    }

    public int totalCoin;

    private int stars;

    public int Stars
    {
        get { return stars; }
        set 
        {
            if(stars != value)
            {
                stars = value;
                //OnStarChanged?.Invoke(stars);
                GameEvent.StarChanged(stars);
            }
        }
    }

    //public int totalStars;

    public bool isPlayingLevel;

    private void Start()
    {
        totalCoin = PlayerPrefs.GetInt("TotalCoins", 150);
    }

}
