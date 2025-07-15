using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Data/Item Data")]
public class ItemImage : ScriptableObject
{
    [Serializable]
    public class ItemInfo
    {
        public string name;
        public Sprite img;
    }

    public List<ItemInfo> Items;
}
