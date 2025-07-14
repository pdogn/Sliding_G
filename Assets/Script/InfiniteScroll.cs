using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    public RectTransform content;
    public GameObject itemPrefab;
    public int itemCount = 10;
    public float spacing = 10f;

    private List<RectTransform> items = new List<RectTransform>();
    private float itemWidth;

    void Start()
    {
        // Tính chiều rộng item
        itemWidth = ((RectTransform)itemPrefab.transform).rect.width + spacing;

        // Tạo item
        for (int i = 0; i < itemCount; i++)
        {
            GameObject obj = Instantiate(itemPrefab, content);
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(i * itemWidth, 0);
            items.Add(rt);
        }
    }

    void Update()
    {
        float viewportWidth = ((RectTransform)transform).rect.width;
        float contentPosX = content.anchoredPosition.x;

        foreach (RectTransform item in items)
        {
            float itemPosX = item.anchoredPosition.x + contentPosX;

            // Nếu item bị trượt quá bên trái
            if (itemPosX + itemWidth / 2 < -viewportWidth / 2)
            {
                // Dời item sang phải
                float rightMostX = GetRightMostX();
                item.anchoredPosition = new Vector2(rightMostX + itemWidth, item.anchoredPosition.y);
            }
            // Nếu item bị trượt quá bên phải
            else if (itemPosX - itemWidth / 2 > viewportWidth / 2)
            {
                // Dời item sang trái
                float leftMostX = GetLeftMostX();
                item.anchoredPosition = new Vector2(leftMostX - itemWidth, item.anchoredPosition.y);
            }
        }
    }

    float GetRightMostX()
    {
        float maxX = float.MinValue;
        foreach (RectTransform item in items)
        {
            if (item.anchoredPosition.x > maxX)
                maxX = item.anchoredPosition.x;
        }
        return maxX;
    }

    float GetLeftMostX()
    {
        float minX = float.MaxValue;
        foreach (RectTransform item in items)
        {
            if (item.anchoredPosition.x < minX)
                minX = item.anchoredPosition.x;
        }
        return minX;
    }
}
