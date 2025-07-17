using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopScrollView : MonoBehaviour
{
    public RectTransform content;
    public float itemHeight = 100f; // Chiều cao của mỗi item
    public float contentSizeHeight = 526f; //chiều dài tổng các item

    private List<RectTransform> items = new List<RectTransform>();
    private float viewHeight;
    
    float topView, bottomView;

    public ItemSkinSO itemSkinSO;
    private LinkedList<ItemSkinInfo> itemSkins;

    private void Awake()
    {
        // lưu danh sách skin
        itemSkins = new LinkedList<ItemSkinInfo>();
        foreach (ItemSkinInfo item in itemSkinSO.Items)
        {
            itemSkins.AddLast(item);
        }
    }

    void Start()
    {
        viewHeight = GetComponent<RectTransform>().rect.height;
        // cạnh trên của vùng hiển thị
        topView = GetComponent<RectTransform>().rect.y + viewHeight / 2;
        // cachj dưới của vùng hiển thị
        bottomView = GetComponent<RectTransform>().rect.y - viewHeight / 2;

        // Lưu danh sách item con
        foreach (Transform child in content)
        {
            items.Add(child as RectTransform);
        }

        //init item
        Init();
    }

    void Update()
    {
        foreach (RectTransform item in items)
        {
            float itemPosY = item.anchoredPosition.y;
            float contentPosY = content.anchoredPosition.y;

            // Nếu item ra khỏi vùng hiển thị bên trên
            if (itemPosY + contentPosY > bottomView + contentSizeHeight - itemHeight/2)
            {
                float bottomMost = GetBottomMostY();
                item.anchoredPosition = new Vector2(item.anchoredPosition.x, bottomMost - itemHeight-5);
                content.SetAsLastSibling();

                //reload item
                ItemSkinInfo itemInf = item.GetComponent<ItemSkinUI>().itemSkinInfo;
                if (!itemSkins.Contains(itemInf))
                {
                    itemSkins.AddLast(itemInf);
                }
                ItemSkinInfo itemInfo = itemSkins.First.Value;
                item.GetComponent<ItemSkinUI>().SetUpItem(itemInfo);
                itemSkins.RemoveFirst();

            }

            // Nếu item ra khỏi vùng hiển thị bên dưới
            else if (itemPosY + contentPosY < topView - contentSizeHeight + itemHeight/2)
            {
                float topMost = GetTopMostY();
                item.anchoredPosition = new Vector2(item.anchoredPosition.x, topMost + itemHeight+5);
                content.SetAsFirstSibling();

                //reload item
                ItemSkinInfo itemInf = item.GetComponent<ItemSkinUI>().itemSkinInfo;
                if (!itemSkins.Contains(itemInf))
                {
                    itemSkins.AddFirst(itemInf);
                }
                ItemSkinInfo itemInfo = itemSkins.Last.Value;
                item.GetComponent<ItemSkinUI>().SetUpItem(itemInfo);
                itemSkins.RemoveLast();

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

    void Init()
    {
        foreach (Transform child in content)
        {
            ItemSkinInfo itemInfo = itemSkins.First.Value;
            child.GetComponent<ItemSkinUI>().SetUpItem(itemInfo);
            itemSkins.RemoveFirst();
        }

    }

}
