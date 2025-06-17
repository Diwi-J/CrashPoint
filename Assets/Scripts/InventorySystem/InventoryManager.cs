using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using System;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject itemCursor; //Cursor that follows the mouse when moving items
    [SerializeField] private GameObject slotHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;
    [SerializeField] private GameObject inventoryPanel;

    [SerializeField] private SlotClass[] startingItems;

    
    private SlotClass[] items;

    private bool isOpen = false;
    private GameObject[] slots;
    public GameObject dropPrefab;

    private SlotClass movingSlot;
    private SlotClass tempSlot;
    private SlotClass originalSlot;
    bool isMovingItem;

    private void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SlotClass();
        }

        for (int i = 0; i < startingItems.Length; i++)
        {
            if (startingItems[i] != null && startingItems[i].GetItem() != null)
            {
                items[i] = new SlotClass(startingItems[i].GetItem(), startingItems[i].GetQuantity());
            }
        }

        // Set all UI slots
        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }

        RefreshUI();

        //These lines now check for null before calling Add/Remove
        if (itemToAdd != null)
            Add(itemToAdd, 1);


        Add(itemToAdd, 1);
        Remove(itemToRemove);
    }
    private void Update()
    {
        itemCursor.SetActive(isMovingItem);
        itemCursor.transform.position = Input.mousePosition;
        if (isMovingItem)
        {
            itemCursor.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            inventoryPanel.SetActive(isOpen);
            Time.timeScale = 0;
        }

        if (Input.GetMouseButtonDown(0)) //Clicking mouse on inventory
        {
            //Find closest slot (slot clicked on)
            if (isMovingItem)
            { 
                EndItemMove();
            }
            else 
                BeginItemMove();
        }
    }

    #region InventoryUtils
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;

                if (items[i].GetItem().isStackable)
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].GetQuantity().ToString();
                else
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0";
            }
            catch 
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

            }
        }
    }

    //Add an item to the inventory/list
    public bool Add(ItemClass item, int quantity)
    {
        if (item == null)
        {
            Debug.LogError("InventoryManager.Add() received NULL item");
            return false;
        }

        SlotClass slot = Contains(item);
        if (slot != null && slot.GetItem().isStackable)
        {
            slot.AddQuantity(1);
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null)
                {
                    items[i].AddItem(item, quantity);
                    break;
                }
            }
        }

        RefreshUI();
        return true;
    }



    //Removes item from inventory/list
    public bool Remove(ItemClass item)
    {
        // Items.Remove(item);
        SlotClass temp = Contains(item);
        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
            temp.SubQuantity(1);
            else
            {
                int slotToRemoveIndex = 0;

                for (int i = 0; i < items.Length; i++)
                {

                    if (items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }
                items[slotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }   

        RefreshUI();
        return true;
    }

    public SlotClass Contains(ItemClass item)
    {
        if (item == null)
        {
            Debug.LogError(" Contains() called with NULL item!");
            return null;
        }

        Debug.Log(" Contains() checking for item: " + item.itemName);

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                Debug.LogError(" items[" + i + "] is NULL!");
                continue;
            }

            var slotItem = items[i].GetItem();

            if (slotItem == null)
            {
                Debug.Log("items[" + i + "] is empty.");
                continue;
            }

            Debug.Log(" items[" + i + "] contains: " + slotItem.itemName);

            if (slotItem == item)
            {
                Debug.Log(" Found matching item in slot " + i);
                return items[i];
            }
        }

        Debug.Log(" Item not found in inventory.");
        return null;
    }



    #endregion InventoryUtils

    #region MovingSlotsUtils
    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
            return false; // No item to move

        movingSlot = new SlotClass(originalSlot.GetItem(), originalSlot.GetQuantity());
        originalSlot.Clear();
        isMovingItem = true;
        RefreshUI();
        return true;
    }
    private SlotClass GetClosestSlot()
    {
        //Debug.Log(Input.mousePosition);

        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
                return items[i];
        }

        return null;
    }
    private bool EndItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            Add(movingSlot.GetItem(), movingSlot.GetQuantity()); // No slot found, add item back to inventory
            movingSlot.Clear();
        }
        else
            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem())
                {
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        movingSlot.Clear();
                    }
                    else
                    {
                        return false;
                    }

                }
            else
            {
                tempSlot = new SlotClass(originalSlot.GetItem(), originalSlot.GetQuantity());
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity());
                RefreshUI();
                return true;

            }
        }
        else
        {
            originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
            movingSlot.Clear();
        }
        isMovingItem = false;
        RefreshUI();
        return true;
    }
    #endregion MovingSlotsUtils

    public GameObject itemDropPrefab; // assign a prefab that has ItemPickup on it

    public void Drop(ItemClass item)
    {
        if (item == null)
        {
            Debug.LogWarning("Tried to drop a null item.");
            return;
        }

        if (!Remove(item))
        {
            Debug.LogWarning("Item not found in inventory to drop.");
            return;
        }

        Vector3 dropPosition = GameObject.FindWithTag("Player").transform.position + Vector3.right;
        GameObject dropped = Instantiate(itemDropPrefab, dropPosition, Quaternion.identity);

        ItemPickup pickup = dropped.GetComponent<ItemPickup>();
        if (pickup != null)
        {
            pickup.itemData = item;
            pickup.quantity = 1;
        }

        Debug.Log("Dropped item: " + item.itemName);
    }


    public void UseItem(ItemClass item)
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Remove(item);
        }

        if (item.isConsumable)
        {
            // Example effect
            Debug.Log("You used " + item.itemName);

            Remove(item);
        }
    }

}