using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class Inventory1: ScriptableObject
{
    public int MaxSlots = 8;
    public List<ItemInstance> items = new List<ItemInstance>();
   

    public bool AddItem(ItemData1 itemData, int Amount = 1)
    {
        foreach (ItemInstance item in items)
        {
            if (item.itemData == itemData && item.Amount < itemData.MaxStack)
            {

                item.Amount += Amount;
                return true;

            }
        }

        if (items.Count < MaxSlots)
        {
            items.Add(new ItemInstance(itemData, Amount));
            return true;
        }

        Debug.Log("Inventory full!");
        return false;
    }

    public void RemoveItem(ItemInstance item)
    {
        items.Remove(item);
    }
}

