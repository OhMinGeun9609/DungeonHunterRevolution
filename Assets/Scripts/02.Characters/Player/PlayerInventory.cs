using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private static int DEFAULT_WEAPON_IDX = 0;
    [SerializeField] private ItemData defalutWeapon;
    private InventoryUI ui;


    public ItemData[] data;
    private Transform weaponPos;

    private ItemSlots[] slots;
    private ItemData selectedItemdata;
    private List<ItemData> holdingItems;
    private ItemData currentEquip;
    private PlayerStatus status;

    public int carryGold = 0;


    private void Start()
    {
        holdingItems = new List<ItemData>();
        weaponPos = GetComponentsInChildren<Transform>(true)
            .First(t => t.name == "handslot.r"); ;
        
        status = GetComponent<PlayerStatus>();
        ui = FindObjectOfType<InventoryUI>(true);


        SetDefalutItem();
        SetInventorySlots();
        SetDefalutWeapon(defalutWeapon);
    }

    private void SetDefalutItem()
    {
        holdingItems.Add(defalutWeapon);
        holdingItems[DEFAULT_WEAPON_IDX].isEquip = true;
    }

    private void SetDefalutWeapon(ItemData weapon)
    {
        if (weapon == null) return;

        Instantiate(weapon.prefab, weaponPos);
    }

    public void GetItem(ItemData get)
    {
        if(holdingItems.Count >= slots.Length)
        {
            Debug.Log("Inventory is Full");
            return;
        }

        holdingItems.Add(get);
        slots[holdingItems.Count].SetItem(get);
    }

    public void SelectItem(ItemData selected)
    {
        ClearSelectedItem();

        if(selected == null) return;

        selectedItemdata = selected;

        ui.SetItemDescription(selectedItemdata.prefab.GetComponent<ItemObject>());
    }

    private void ClearSelectedItem()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].SelectedCanceled();
        }

        selectedItemdata = null;
    }

    public void OnClickEquipWeapon()
    {
        if (selectedItemdata.isEquip || selectedItemdata.ItemType != ItemType.Equipments) return;
        if(selectedItemdata.ItemType == ItemType.Equipments)
        {
            if(weaponPos.childCount > 0 && currentEquip != null)
            {
                Destroy(weaponPos.GetChild(0).gameObject);
            }

            currentEquip = selectedItemdata;

            Instantiate(currentEquip.prefab, weaponPos);
            status.Equip(currentEquip.atk, currentEquip.equipmentType);
            int idx = GetEquipItemIndex();
            if(idx >= 0 && slots[idx].itemData != null)
            {
                slots[idx].itemData.isEquip = true;
                slots[idx].EquipMarkActiveJudge();
            }
        }
    }

    private int GetEquipItemIndex()
    {
        if (currentEquip == null) return -1;
        
        for(int i = 0; i < holdingItems.Count; i++)
        {
            if(currentEquip.itemName == holdingItems[i].itemName)
            {
                return i;
            }
        }

        return -1;
    }

    public void GetGold(int rewardGold)
    {
        carryGold += rewardGold;
    }

    private void SetInventorySlots()
    {
        if (holdingItems.Count < 0 || holdingItems == null) return;

        slots = ui.ReturnSlot();

        for(int i = 0; i < holdingItems.Count; i++)
        {
            slots[i].SetItem(holdingItems[i]);
        }
    }
}
