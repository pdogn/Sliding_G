using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static event Action<int> OnCoinChanged;
    public static event Action<int> OnTotalCoinChanged;

    public static event Action<int> OnStarChanged;
    public static event Action<int> OnTotalStarChanged;

    public static event Action OnDisplayMainUI;
    public static event Action OnDisplaySelectLvUI;

    public static event Action OnPassLevel;
    public static event Action OnFailLevel;

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

    public static void Pass_Level()
    {
        OnPassLevel?.Invoke();
    }
    public static void Fail_Level()
    {
        OnFailLevel?.Invoke();
    }
}

public class GameManager : Singleton<GameManager>
{
    public int Level;
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

    public int totalStars;

    //public event Action<int> OnCoinChanged;
    //public event Action<int> OnStarChanged;
}
