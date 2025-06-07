using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots = 20;
    public List<InventorySlot> items = new List<InventorySlot>();

    public void AddItem(Item item, int amount = 1)
    {
        // Check if item is stackable and already in inventory
        if (item.isStackable)
        {
            foreach (InventorySlot slot in items)
            {
                if (slot.item == item)
                {
                    slot.AddAmount(amount);
                    Debug.Log($"Added {amount} more of {item.name}");
                    return;
                }
            }
        }

        // If not stackable or not found, add new slot if there's space
        if (items.Count < maxSlots)
        {
            items.Add(new InventorySlot(item, amount));
            Debug.Log($"Added new item: {item.name} x{amount}");
        }
        else
        {
            Debug.Log("Inventory is full!");
        }
    }

    public void RemoveItem(Item item, int amount = 1)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                items[i].amount -= amount;
                if (items[i].amount <= 0)
                {
                    items.RemoveAt(i);
                    Debug.Log($"{item.name} removed completely.");
                }
                return;
            }
        }

        Debug.Log("Item not found in inventory.");
    }

    public bool HasItem(Item item)
    {
        foreach (InventorySlot slot in items)
        {
            if (slot.item == item)
                return true;
        }
        return false;
    }
}
