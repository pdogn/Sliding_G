using System;

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
