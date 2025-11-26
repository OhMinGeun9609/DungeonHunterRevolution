using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour
{
    [SerializeField] private Image icon;

    private ItemData itemData;
    
    public void SetItem(ItemData data)
    {
        itemData = data;
        icon.sprite = itemData.icon;
    }
}
