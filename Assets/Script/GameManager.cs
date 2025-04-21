using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                OnCoinChanged?.Invoke(coins);
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
                OnStarChanged?.Invoke(stars);
            }
        }
    }

    public int totalStars;

    public event Action<int> OnCoinChanged;
    public event Action<int> OnStarChanged;
}
