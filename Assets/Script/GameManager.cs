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
                Stars = GetLevelStars(crrlevel);
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

    [SerializeField] int stars;

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

    /// <summary>
    /// lưu số sao nhận dược mỗi level
    /// </summary>
    #region
    
    [System.Serializable]
    public class LevelStarsData
    {
        public string levelName;
        public int stars;
    }

    [System.Serializable]
    public class LevelStarsDataList
    {
        public List<LevelStarsData> levels = new List<LevelStarsData>();
    }

    private Dictionary<string, int> levelStars = new Dictionary<string, int>();

    private string saveFilePath => Application.persistentDataPath + "/levelStarsData.json";

    public void SaveStarLevels()
    {
        LevelStarsDataList dataList = new LevelStarsDataList();
        foreach (var kvp in levelStars)
        {
            dataList.levels.Add(new LevelStarsData { levelName = kvp.Key, stars = kvp.Value });
        }

        string json = JsonUtility.ToJson(dataList, true);
        System.IO.File.WriteAllText(saveFilePath, json);

        Debug.Log("Saved to " + saveFilePath);
    }

    public void LoadStarLevels()
    {
        if (System.IO.File.Exists(saveFilePath))
        {
            string json = System.IO.File.ReadAllText(saveFilePath);
            LevelStarsDataList dataList = JsonUtility.FromJson<LevelStarsDataList>(json);

            levelStars.Clear();
            foreach (var level in dataList.levels)
            {
                levelStars[level.levelName] = level.stars;
            }

            Debug.Log("Loaded from " + saveFilePath);
        }
        else
        {
            Debug.LogWarning("No save file found at " + saveFilePath);
        }
    }
    public void SetLevelStars(int levelName, int stars)
    {
        levelStars[$"StarLevel{levelName}"] = stars;
    }

    public int GetLevelStars(int levelName)
    {
        return levelStars.ContainsKey($"StarLevel{levelName}") ? levelStars[$"StarLevel{levelName}"] : 0;
    }
    #endregion

    //khi chơi gameplay
    public bool isPlayingTGamePlay;

    private void Start()
    {
        totalCoin = PlayerPrefs.GetInt("TotalCoins", 150);
        LoadStarLevels();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.L))
    //    {
    //        foreach (var kvp in levelStars)
    //        {
    //            Debug.Log($"Level: {kvp.Key}, Stars: {kvp.Value}");
    //        }
    //    }
    //}
}

