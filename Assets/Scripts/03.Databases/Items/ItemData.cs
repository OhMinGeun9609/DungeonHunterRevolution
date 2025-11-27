using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipments,
    Consumable
}

public enum EquipmentType
{
    Weapon,
    Aromor
}

public enum HandedType
{
    OneHanded,
    TwoHanded
}

public enum ConsumableType
{
    Recovery,
    StatElevation
}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public int value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Item Info")]
    public ItemType ItemType;
    public HandedType handedType;
    public EquipmentType equipmentType;
    public Sprite icon;
    public GameObject prefab;
    public string itemName;
    public string itemDescription;
    public int atk;
    public bool isEquip;
    public bool isExist;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}
