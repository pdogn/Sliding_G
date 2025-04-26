using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputHandle : Singleton<InputHandle>
{
    // Gửi hướng vuốt
    public event Action<Vector2> OnSwipe;
    // Ngưỡng để tính là vuốt
    public float swipeThreshold = 50f;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    public bool IsSwiping => isSwiping;

    public Vector2 direction;

    void Update()
    {
        if (!GameManager.Instance.isPlayingLevel)
        {
            isSwiping = false;
            return;
        }
#if UNITY_EDITOR
        HandleMouseSwipe();
#else
        HandleTouchSwipe();
#endif
    }

    void HandleMouseSwipe()
    {
        //if (!GameManager.Instance.isPlayingLevel)
        //{
        //    isSwiping = false;
        //    return;
        //}

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            endTouchPosition = Input.mousePosition;
            DetectSwipe();
            isSwiping = false;
        }
    }

    void HandleTouchSwipe()
    {
        //if (!GameManager.Instance.isPlayingLevel) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;
                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        endTouchPosition = touch.position;
                        DetectSwipe();
                        isSwiping = false;
                    }
                    break;
            }
        }
    }

    void DetectSwipe()
    {
        Vector2 delta = endTouchPosition - startTouchPosition;

        if (delta.magnitude > swipeThreshold)
        {
            direction = Vector2.zero;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                direction = delta.x > 0 ? Vector2.right : Vector2.left;
            }
            else
            {
                direction = delta.y > 0 ? Vector2.up : Vector2.down;
            }

            OnSwipe?.Invoke(direction);

            // Debug log hướng vuốt
            if (direction == Vector2.up) Debug.Log("Swipe Up");
            else if (direction == Vector2.down) Debug.Log("Swipe Down");
            else if (direction == Vector2.left) Debug.Log("Swipe Left");
            else if (direction == Vector2.right) Debug.Log("Swipe Right");
        }
    }
}
