using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI GoldText;
    private ItemSlots[] itemSlots;

    private Player player;

    private void Start()
    {
        player = GameManager.Instance.playerInfo;
    }

    private void Update()
    {
        SetText();
    }

    private void SetText()
    {
        int gold = player.inventory.carryGold;

        GoldText.text = gold.ToString();
    }

    public void SetItemDescription(ItemObject item)
    {
        string description = item.DisplayItemData();
        descriptionTxt.text = description;
    }

    public ItemSlots[] ReturnSlot()
    {
        itemSlots = GetComponentsInChildren<ItemSlots>(true);
        return itemSlots;
    }
}
