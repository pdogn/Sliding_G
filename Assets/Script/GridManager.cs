using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//[Serializable]
//public class GameObj2DArray
//{
//    public GameObject[] rows = new GameObject[1];
//}

[System.Serializable]
public class LevelData
{
    public int[] data;
    public int rows;
    public int cols;
    public float _cameraSize;
}

public class GridManager : Singleton<GridManager>
{
    [SerializeField] Camera cam;

    [SerializeField]
    Transform spawnedBlockParent;

    //[SerializeField] GameObject blockPrefab;
    public GameObject[] tilePrefabs;
    float tileSize = .5f;

    public GameObject Player;

    //[SerializeField] int gridSizeX;
    //[SerializeField] int gridSizeY;

    //public GameObj2DArray[] allBlockObj;

    public int[,] grid;
    public GameObject[,] allBlockObj;

    public int GridSizeX { get; private set; }
    public int GridSizeY { get; private set; }

    public int CurrenlevelIndex;

    protected override void Awake()
    {
        cam = GameObject.FindObjectOfType<Camera>();
    }
    private void Start()
    {
        //SpawnObj();
        //LoadLevel(levelName);
    }

    //private void SpawnObj()
    //{
    //    if (spawnedBlockParent.childCount > 0)
    //    {
    //        foreach (Transform child in spawnedBlockParent)
    //        {
    //            Destroy(child.gameObject);
    //        }
    //    }

    //    float blockSize = 0.5f;
    //    Vector2 startPos = new Vector2(-((gridSizeX * blockSize) / 2) + (blockSize / 2), ((gridSizeY * blockSize) / 2) - (blockSize / 2));

    //    allBlockObj = new GameObj2DArray[gridSizeX];

    //    for (int i = 0; i < gridSizeX; i++)
    //    {
    //        allBlockObj[i] = new GameObj2DArray();
    //        allBlockObj[i].rows = new GameObject[gridSizeY];

    //        for (int j = 0; j < gridSizeY; j++)
    //        {
    //            Vector3 spawnPos = transform.position + new Vector3(startPos.x + i * blockSize, startPos.y - j * blockSize, 0);

    //            GameObject spawnedObj = Instantiate(blockPrefab, spawnPos, Quaternion.identity, spawnedBlockParent);

    //            allBlockObj[i].rows[j] = spawnedObj;

    //            GetBlockType(spawnedObj, i, j);
    //        }
    //    }
    //}

    public void LoadLevel(int levelId)
    {
        CurrenlevelIndex = levelId;

        if (spawnedBlockParent.childCount > 0)
        {
            foreach (Transform child in spawnedBlockParent)
            {
                Destroy(child.gameObject);
            }
        }

        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        string jsonText = null;

        // Nếu level đã được vượt qua, load từ persistentDataPath
        if (levelId < unlockedLevel)
        {
            string filePath = Path.Combine(Application.persistentDataPath, $"LevelPassed/Level{levelId}.json");

            if (!File.Exists(filePath))
            {
                Debug.LogError("Không tìm thấy file: " + filePath);
                return;
            }

            jsonText = File.ReadAllText(filePath);
        }
        else // Nếu chưa vượt qua, load từ Resources
        {
            string resourcePath = $"LevelJsons/Level{levelId}";
            TextAsset levelJson = Resources.Load<TextAsset>(resourcePath);

            if (levelJson == null)
            {
                Debug.LogError("Không tìm thấy file trong Resources: " + resourcePath);
                return;
            }

            jsonText = levelJson.text;
        }

        LevelData level = JsonUtility.FromJson<LevelData>(jsonText);

        GridSizeX = level.cols;
        GridSizeY = level.rows;
        cam.orthographicSize = level._cameraSize;

        grid = new int[level.rows, level.cols];
        allBlockObj = new GameObject[level.rows, level.cols];

        Vector2 startPos = new Vector2(-((GridSizeX * tileSize) / 2) + (tileSize / 2), ((GridSizeY * tileSize) / 2) - (tileSize / 2));

        for (int i = 0; i < level.rows; i++)
        {
            for (int j = 0; j < level.cols; j++)
            {
                int index = i * level.cols + j;
                grid[i, j] = level.data[index];

                int tileType = grid[i, j];

                if (tileType >= 0 && tileType < tilePrefabs.Length && tilePrefabs[tileType] != null)
                {
                    Vector3 spawnPos = new Vector3(startPos.x + j * tileSize, startPos.y - i * tileSize, 0);
                    GameObject titleObj = Instantiate(tilePrefabs[tileType], spawnPos, Quaternion.identity, spawnedBlockParent);
                    allBlockObj[i, j] = titleObj;

                    if (tileType == (int)BlockTitleMap.empty)
                    {
                        allBlockObj[i, j].SetActive(false);
                    }

                    if (tileType == (int)BlockTitleMap.start)
                    {
                        if (Player == null)
                        {
                            GameObject PlayerPrefab = Resources.Load<GameObject>("Prefabs/Player");
                            Player = Instantiate(PlayerPrefab, spawnPos, Quaternion.identity);
                        }
                        Player.SetActive(false);
                        Player.GetComponent<Player>().SetPlayer(new Vector2(i, j), spawnPos);
                        Player.SetActive(true);
                    }

                    if (tileType == (int)BlockTitleMap.enemyPos)
                    {
                        GameObject enemy = Instantiate(tilePrefabs[tileType], spawnPos, Quaternion.identity);
                        enemy.name = "Enemyyyyy";
                        allBlockObj[i, j].SetActive(false);
                    }
                }
            }
        }

        Debug.Log("Map loaded và tile đã được spawn.");
    }


    public void SaveLevelJson()
    {
        LevelData saveData = new LevelData();
        saveData.rows = grid.GetLength(0);
        saveData.cols = grid.GetLength(1);
        saveData._cameraSize = cam.orthographicSize;

        int row = saveData.rows;
        int col = saveData.cols;
        int[] flatData = new int[row * col];

        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                flatData[y * col + x] = allBlockObj[y, x].activeInHierarchy ? grid[y, x] : 0;
            }
        }

        saveData.data = flatData;

        string saveFolder = Path.Combine(Application.persistentDataPath, "LevelPassed");
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
        string saveFilePath = Path.Combine(saveFolder, $"Level{CurrenlevelIndex}.json");
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Grid saved to: " + saveFilePath);
    }

    //public void SaveLevel(bool allow)
    //{
    //    if (allow == false) return;

    //    GameEvent.DisplayPass_LevelUI();
    //    this.SaveLevelJson();
    //    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    //    PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);
    //}
}
