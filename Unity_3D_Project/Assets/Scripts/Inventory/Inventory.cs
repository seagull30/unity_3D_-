using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Slot[] slots;
    private int _maxSlotNum;
    private int _selectSlotNum;
    public int ItemCount = 0;
    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        _maxSlotNum = slots.Length;
        slots[0].SelectSlot();
    }

    public bool AcquireItem(Item _item)
    {
        if (ItemCount <= _maxSlotNum)
        {
            for (int i = 0; i < _maxSlotNum; i++)
            {
                if (slots[i].item == null)
                {
                    slots[i].AddItem(_item);
                    ++ItemCount;
                    return true;
                }
            }
        }
        return false;
    }
    public void SelectSlot(int slotNum)
    {
        _selectSlotNum = slotNum;
        slots[_selectSlotNum].SelectSlot();
    }
    public void Useitem()
    {
        if (slots[_selectSlotNum] != null)
        {
            slots[_selectSlotNum].UseItem();
            --ItemCount;
        }
    }


}

