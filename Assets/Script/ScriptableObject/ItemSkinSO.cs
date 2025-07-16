using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/Item Data")]
public class ItemSkinSO : ScriptableObject
{

    public List<ItemSkinInfo> Items;
}

[Serializable]
public class ItemSkinInfo
{
    public string name;
    public Sprite img;
    public int index;
}
