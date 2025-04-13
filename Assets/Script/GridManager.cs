using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObj2DArray
{
    public GameObject[] rows = new GameObject[1];
}

public class GridManager : MonoBehaviour
{
    [SerializeField]
    Transform spawnedBlockParent;

    [SerializeField] GameObject blockPrefab;

    [SerializeField] int gridSizeX;
    [SerializeField] int gridSizeY;

    public GameObj2DArray[] allBlockObj;

    private void Start()
    {
        SpawnObj();
    }

    private void SpawnObj()
    {
        if(spawnedBlockParent.childCount > 0)
        {
            foreach(Transform child in spawnedBlockParent)
            {
                Destroy(child.gameObject);
            }
        }

        float blockSize = 0.5f;
        Vector2 startPos = new Vector2(-((gridSizeX * blockSize) / 2) + (blockSize/2), ((gridSizeY * blockSize) / 2) - (blockSize / 2));

        allBlockObj = new GameObj2DArray[gridSizeX];

        for(int i=0; i< gridSizeX; i++)
        {
            allBlockObj[i] = new GameObj2DArray();
            allBlockObj[i].rows = new GameObject[gridSizeY];

            for (int j=0; j< gridSizeY; j++)
            {
                Vector3 spawnPos = transform.position + new Vector3(startPos.x + i*blockSize, startPos.y - j*blockSize, 0);

                GameObject spawnedObj = Instantiate(blockPrefab, spawnPos, Quaternion.identity, spawnedBlockParent);

                allBlockObj[i].rows[j] = spawnedObj;

                GetBlockType(spawnedObj, i, j);
            }
        }
    }

    void GetBlockType(GameObject blockObj, int xIndex, int yIndex)
    {
        Block bl = blockObj.AddComponent<Block>();

        bl.gridIndex = new Vector2(xIndex, yIndex);
    }
}
