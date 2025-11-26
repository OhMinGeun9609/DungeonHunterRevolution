using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDisplayable
{
    public string DisplayItemData();
}

public class ItemObject : MonoBehaviour, IDisplayable
{
    public ItemData ItemData;

    public string DisplayItemData()
    {
        string str = $"{ItemData.itemName} \n {ItemData.itemDescription}";
        return str;
    }
}
