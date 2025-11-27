using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlots : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Image equipMark;

    private Outline outline;

    public ItemData itemData;

    private void Start()
    {
        outline = GetComponent<Outline>();
    }

    public void SetItem(ItemData data)
    {
        itemData = data;

        if (itemData.isEquip)
        {
            equipMark.gameObject.SetActive(true);
        }
        else
        {
            equipMark.gameObject.SetActive(false);
        }

        icon.sprite = itemData.icon;
        icon.gameObject.SetActive(true);
        EquipMarkActiveJudge();
    }

    public void EquipMarkActiveJudge()
    {
        if (itemData.isEquip)
        {
            equipMark.gameObject.SetActive(true);
        }
        else
        {
            equipMark.gameObject.SetActive(false);
        }
    }

    public void OnClickSlot()
    {
        GameManager.Instance.PlayerInfo.inventory.SelectItem(this.itemData);
        outline.enabled = true;
    }

    public void SelectedCanceled()
    {
        outline.enabled = false;
    }
}
