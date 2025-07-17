using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkinUI : MonoBehaviour
{
    public int index;
    [SerializeField] public Image skinImage;
    [SerializeField] public TextMeshProUGUI lbl_name;
    [SerializeField] public Button use_Btn;
    [SerializeField] public RectTransform lbl_using;

    //public bool isUsing;

    public ItemSkinInfo itemSkinInfo;

    private void Start()
    {
        use_Btn.onClick.AddListener(Click);
    }

    public void SetUpItem(ItemSkinInfo item)
    {
        itemSkinInfo = item;

        skinImage.sprite = item.img;
        lbl_name.text = item.name;
        index = item.index;
        //uiSkinCanvas.AddDic(item.name, this);
        if(GameManager.Instance.CurrentSkin == item.index)
        {
            use_Btn.gameObject.SetActive(false);
            lbl_using.gameObject.SetActive(true);
        }
        else
        {
            use_Btn.gameObject.SetActive(true);
            lbl_using.gameObject.SetActive(false);
        }
    }

    void Click()
    {
        GameManager.Instance.CurrentSkin = itemSkinInfo.index;

        Transform content = this.gameObject.transform.parent;
        foreach(Transform child in content)
        {
            ItemSkinUI item = child.GetComponent<ItemSkinUI>();
            if (GameManager.Instance.CurrentSkin == item.index)
            {
                item.use_Btn.gameObject.SetActive(false);
                item.lbl_using.gameObject.SetActive(true);
            }
            else
            {
                item.use_Btn.gameObject.SetActive(true);
                item.lbl_using.gameObject.SetActive(false);
            }
        }
    }
}
