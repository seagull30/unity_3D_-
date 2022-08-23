using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image itemImage;
    bool isSelet = false;
    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemData.itemImage;
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;

        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
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
    public void UseItem()
    {
        if (item != null)
        {
            item.itemData.itemPrefab.GetComponent<Use>().UseEffect();
            SetSlotCount(-1);
        }
    }
}