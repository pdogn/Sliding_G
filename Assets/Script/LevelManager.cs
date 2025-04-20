using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Level
{
    public int LevelName;
    public int crr_Star;
    public string path_JsonName;
}

public class LevelManager : MonoBehaviour
{
    private Level[] levels;

    private void Start()
    {
        
    }

    public void LoadLevelInfo()
    {
        int levelsCount = SelectLevel_Canvas.Instance.GetButtonsCount();
        levels = new Level[levelsCount];
        for (int i = 0; i < levelsCount; i++)
        {
            int levelName = i + 1;
            int Crr_Star = 0;
            string _path = $"Level{i + 1}";

            levels[i] = new Level
            {
                LevelName = levelName,
                crr_Star = Crr_Star,
                path_JsonName = _path
            };
        }
    }

    public string GetLevelName(int index)
    {
        return levels[index].LevelName.ToString();
    }

    public int GetCurrentStar(int index)
    {
        return levels[index].crr_Star;
    }
    public string GetPath(int index)
    {
        return levels[index].path_JsonName;
    }
}
