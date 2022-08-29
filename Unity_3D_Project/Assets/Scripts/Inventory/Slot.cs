using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemImage;
    bool isSelet = false;
    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    public void AddItem(Item _item)
    {
        item = _item;
        itemImage.sprite = item.itemData.itemImage;
    }

    private void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
    }

    public void SelectSlot()
    {
        isSelet = !isSelet;

        if (isSelet)
            itemImage.color = Color.red;
        else
            itemImage.color = Color.white;
    }
    public bool UseItem()
    {
        if (item != null)
        {
            item.itemData.itemPrefab.GetComponent<Use>().UseEffect();
            ClearSlot();
            return true;
        }
        return false;
    }
}