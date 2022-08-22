using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    {
        Used,
        Ingredient,
        ETC
    }
    public string itemName;
    public ItemType itemtype;
    public Sprite itemImage;
    public GameObject itemPrefab;
}
