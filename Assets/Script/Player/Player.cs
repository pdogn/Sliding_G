using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector2 target;     //(rows, cols)

    // Start is called before the first frame update
    void Start()
    {
        InputHandle.Instance.OnSwipe += OnSwipeDetected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSwipeDetected(Vector2 direction)
    {
        if (direction == Vector2.right)
        {
            FindTarget(direction, target);
            MoveToTarget(.1f);
        }
        else if (direction == Vector2.left)
        {
            FindTarget(direction, target);
            MoveToTarget(.1f);
        }
        else if (direction == Vector2.up)
        {
            FindTarget(direction, target);
            MoveToTarget(.1f);
        }
        else if (direction == Vector2.down)
        {
            FindTarget(direction, target);
            MoveToTarget(.1f);
        }
    }

    void FindTarget(Vector2 direction, Vector2 gridIndex)
    {
        //if (direction == Vector2.right)
        //{
        //    if(target.y + 1 < GridManager.Instance.GridSizeX && GridManager.Instance.allBlockObj[(int)target.x, (int)target.y + 1]) 
        //    { 

        //    }
        //}

        int x = (int)gridIndex.x;
        int y = (int)gridIndex.y;
        if(direction == Vector2.right)
        {
            if (y + 1 < GridManager.Instance.GridSizeX && GridManager.Instance.allBlockObj[x, y+1] != null)
            {
                int value = GridManager.Instance.grid[x, y + 1];
                if (value == 0 || value == 2 || value == 3 || value == 6 || value == 7)
                {
                    target = new Vector2(x, y + 1);
                    FindTarget(direction, new Vector2(x, y + 1));
                }
            }
        }
        if (direction == Vector2.left)
        {
            if (y - 1 < GridManager.Instance.GridSizeX && GridManager.Instance.allBlockObj[x, y - 1] != null)
            {
                int value = GridManager.Instance.grid[x, y - 1];
                if (value == 0 || value == 2 || value == 3 || value == 6 || value == 7)
                {
                    target = new Vector2(x, y - 1);
                    FindTarget(direction, new Vector2(x, y - 1));
                }
            }
        }
        if (direction == Vector2.down)
        {
            if (x + 1 < GridManager.Instance.GridSizeY && GridManager.Instance.allBlockObj[x+1, y] != null)
            {
                int value = GridManager.Instance.grid[x + 1, y];
                if (value == 0 || value == 2 || value == 3 || value == 6 || value == 7)
                {
                    target = new Vector2(x + 1, y);
                    FindTarget(direction, new Vector2(x + 1, y));
                }
            }
        }
        if (direction == Vector2.up)
        {
            if (x - 1 < GridManager.Instance.GridSizeY && GridManager.Instance.allBlockObj[x - 1, y] != null)
            {
                int value = GridManager.Instance.grid[x - 1, y];
                if (value == 0 || value == 2 || value == 3 || value == 6 || value == 7)
                {
                    target = new Vector2(x - 1, y);
                    FindTarget(direction, new Vector2(x - 1, y));
                }
            }
        }
    }

    void MoveToTarget(float arriveTime)
    {
        DOTween.Kill(transform);
        transform.DOKill();
        Transform targetObj = GridManager.Instance.allBlockObj[(int)target.x, (int)target.y].transform;
        transform.DOMove(targetObj.position, arriveTime).SetEase(Ease.Linear).OnComplete(() =>
        {

        });
    }

    public void SetPlayer(Vector2 _target)
    {
        target = _target;
    }
}
