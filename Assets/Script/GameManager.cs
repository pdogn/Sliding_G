using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //khi ch
    public bool isPlayingLevel;

    private void Start()
    {
        totalCoin = PlayerPrefs.GetInt("TotalCoins", 150);
    }

}
