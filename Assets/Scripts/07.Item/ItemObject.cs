using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemObject : MonoBehaviour
{
    public ItemData ItemData;

    public string DisplayItemData()
    {
        string str = $"{ItemData.itemName} \n {ItemData.itemDescription}";
        return str;
    }
}
