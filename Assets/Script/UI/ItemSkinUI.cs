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

    public bool isUsing;

    //[SerializeField] UI_Skin_canvas uiSkinCanvas;

    public void SetUpItem(ItemSkinInfo item)
    {
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

    public ItemSkinInfo RetunItemInfo()
    {
        ItemSkinInfo itemInf = new ItemSkinInfo();
        itemInf.name = lbl_name.text;
        itemInf.img = skinImage.sprite;
        itemInf.index = this.index;
        return itemInf;
    }
}
