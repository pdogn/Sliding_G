using System;
using System.Collections;
using System.Collections.Generic;
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

    public GameObject PlayerPrefab;

    //[SerializeField] int gridSizeX;
    //[SerializeField] int gridSizeY;

    //public GameObj2DArray[] allBlockObj;

    public int[,] grid;
    public GameObject[,] allBlockObj;

    public int GridSizeX { get; private set; }
    public int GridSizeY { get; private set; }

    public string levelName = "Level5";

    private void Awake()
    {
        cam = GameObject.FindObjectOfType<Camera>();
    }
    private void Start()
    {
        //SpawnObj();
        LoadLevel(levelName);
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

    private void LoadLevel(string name)
    {
        if (spawnedBlockParent.childCount > 0)
        {
            foreach (Transform child in spawnedBlockParent)
            {
                Destroy(child.gameObject);
            }
        }

        string path = $"LevelJsons/{name}";
        TextAsset levelJson = Resources.Load<TextAsset>(path);

        if (levelJson == null)
        {
            Debug.LogError($"Không tìm thấy file: {path}");
            return;
        }
       
        LevelData level = JsonUtility.FromJson<LevelData>(levelJson.text);
        GridSizeX = level.cols;
        GridSizeY = level.rows;
        Debug.LogError(level._cameraSize + "lll");
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
                Debug.Log($"Tile at ({i},{j}) = {grid[i, j]}");

                int tileType = grid[i, j];

                if (tileType >= 0 && tileType < tilePrefabs.Length && tilePrefabs[tileType] != null)
                {
                    Vector3 spawnPos = new Vector3(startPos.x + j * tileSize, startPos.y - i * tileSize, 0);
                    GameObject titleObj = Instantiate(tilePrefabs[tileType], spawnPos, Quaternion.identity, spawnedBlockParent);
                    allBlockObj[i, j] = titleObj;

                    if(tileType == (int)BlockTitleMap.empty)
                    {
                        allBlockObj[i, j].SetActive(false);
                    }

                    if(tileType == (int)BlockTitleMap.start)
                    {
                        GameObject player = Instantiate(PlayerPrefab, spawnPos, Quaternion.identity);
                        player.GetComponent<Player>().SetPlayer(new Vector2(i, j));
                    }

                    if(tileType == (int)BlockTitleMap.enemyPos)
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

}
