using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Vector2 currentIndex;
    [SerializeField] Vector2 target;     //(rows, cols)
    [SerializeField] float timeMoveToNextblock = 0.1f;
    bool isMoving;
    public int receivedCoins;

    // Start is called before the first frame update
    void Start()
    {
        InputHandle.Instance.OnSwipe += OnSwipeDetected;
        receivedCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    GridManager.Instance.SaveLevelJson();
        //    int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        //    PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);
        //}
    }

    void OnSwipeDetected(Vector2 direction)
    {
        if (isMoving) return;

        isMoving = true;
        if (direction == Vector2.right)
        {
            FindTarget(direction, target);
            MoveToTarget(target, CaculateTimeToMove());
        }
        else if (direction == Vector2.left)
        {
            FindTarget(direction, target);
            MoveToTarget(target, CaculateTimeToMove());
        }
        else if (direction == Vector2.up)
        {
            FindTarget(direction, target);

            MoveToTarget(target, CaculateTimeToMove());
        }
        else if (direction == Vector2.down)
        { 
            FindTarget(direction, target);
            MoveToTarget(target, CaculateTimeToMove());
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
            if (y - 1 >= 0 && GridManager.Instance.allBlockObj[x, y - 1] != null)
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
            if (x - 1 >= 0 && GridManager.Instance.allBlockObj[x - 1, y] != null)
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

    float CaculateTimeToMove()
    {
        if(currentIndex == target) return 0;

        float _timeToMove = timeMoveToNextblock;
        int distanceX = (int)Mathf.Abs(target.x - currentIndex.x);
        int distanceY = (int)Mathf.Abs(target.y - currentIndex.y);
        if(distanceX > 0)
        {
            _timeToMove = timeMoveToNextblock * distanceX;
        }
        if(distanceY > 0)
        {
            _timeToMove = timeMoveToNextblock * distanceY;
        }
        return _timeToMove;
    }

    void MoveToTarget(Vector2 _targetIndex, float arriveTime)
    {
        arriveTime = CaculateTimeToMove();
        Debug.Log("Dong: arriveTime: " + arriveTime);

        DOTween.Kill(transform);
        transform.DOKill();
        Transform targetObj = GridManager.Instance.allBlockObj[(int)_targetIndex.x, (int)_targetIndex.y].transform;
        transform.DOMove(targetObj.position, arriveTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            isMoving = false;
            currentIndex = _targetIndex;
        });
    }

    public void SetPlayer(Vector2 _target, Vector3 _position)
    {
        target = _target;
        currentIndex = target;
        this.transform.position = _position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("coin"))
        {
            collision.gameObject.SetActive(false);
            receivedCoins += 1;
            GameManager.Instance.Coins = receivedCoins;
        }

        if (collision.CompareTag("Finish"))
        {
            Debug.Log("fnnnn");

            GameEvent.DisplayPass_LevelUI();
            GridManager.Instance.SaveLevelJson();
            int crrLevel = GameManager.Instance.CurrenLevel;
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            if(crrLevel == unlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel + 1);
            }

            //this.gameObject.SetActive(false);
        }
    }
}
