using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public ItemData[] data;
    public Transform weaponPos;

    private ItemSlots[] slots;
    private List<ItemData> holdingItems;
    private ItemData currentEquip;


    private void Start()
    {
        slots = GetComponentsInChildren<ItemSlots>();
        SetDefalutItem();
    }

    private void SetDefalutItem()
    {
        holdingItems.Add(data[0]);
        EquipWeapon(holdingItems[0]);
    }

    private void EquipWeapon(ItemData weapon)
    {
        if (weapon == null) return;

        Instantiate(weapon, weaponPos);
    }

    private void SetInventorySlots()
    {
        for(int i = 0; i < holdingItems.Count; i++)
        {
            for(int j = 0; j < data.Length; i++)
            {
                
            }
        }
    }
}
