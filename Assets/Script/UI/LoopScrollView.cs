using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopScrollView : MonoBehaviour
{
    public RectTransform content;
    public float itemHeight = 100f; // Chiều cao của mỗi item
    public int totalItems = 10;

    private List<RectTransform> items = new List<RectTransform>();
    private float viewHeight;
    float contentSize;

    void Start()
    {
        viewHeight = GetComponent<RectTransform>().rect.height;
        contentSize = content.rect.height;
        // Lưu danh sách item con
        foreach (Transform child in content)
        {
            items.Add(child as RectTransform);
        }
    }

    void Update()
    {
        foreach (RectTransform item in items)
        {
            float itemPosY = item.anchoredPosition.y;
            float contentPosY = content.anchoredPosition.y;

            // Nếu item ra khỏi vùng hiển thị bên trên
            if (itemPosY + contentPosY > (contentSize - 52) /2)
            {
                float bottomMost = GetBottomMostY();
                item.anchoredPosition = new Vector2(item.anchoredPosition.x, bottomMost - itemHeight);
                content.SetAsLastSibling();
            }

            // Nếu item ra khỏi vùng hiển thị bên dưới
            else if (itemPosY + contentPosY < -(contentSize-52) / 2 -120f)
            {
                float topMost = GetTopMostY();
                item.anchoredPosition = new Vector2(item.anchoredPosition.x, topMost + itemHeight);
                content.SetAsFirstSibling();
            }
        }
    }

    private float GetBottomMostY()
    {
        float minY = float.MaxValue;
        foreach (var item in items)
        {
            if (item.anchoredPosition.y < minY)
                minY = item.anchoredPosition.y;
        }
        return minY;
    }

    private float GetTopMostY()
    {
        float maxY = float.MinValue;
        foreach (var item in items)
        {
            if (item.anchoredPosition.y > maxY)
                maxY = item.anchoredPosition.y;
        }
        return maxY;
    }
}
