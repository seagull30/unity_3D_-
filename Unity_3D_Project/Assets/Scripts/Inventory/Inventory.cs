using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Slot[] slots;
    private int _selectSlotNum;
    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        slots[0].SelectSlot();
    }

    public bool AcquireItem(Item _item, int _count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                return true;
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
            slots[_selectSlotNum].UseItem();
    }

}

