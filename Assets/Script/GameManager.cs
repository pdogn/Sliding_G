using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvent
{
    public static event Action<int> OnCoinChanged;
    public static event Action<int> OnStarChanged;

    public static void CoinChanged(int coins)
    {
        OnCoinChanged?.Invoke(coins);
    }
    public static void StarChanged(int stars)
    {
        OnStarChanged?.Invoke(stars);
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
