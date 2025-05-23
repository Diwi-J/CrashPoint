using UnityEngine;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public PlayerInventory playerInventory;

    private List<InventorySlotUI> slotUIs = new List<InventorySlotUI>();

    void Start()
    { 
        GenerateSlots();
        UpdateUI(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryMenu.SetActive(!inventoryMenu.activeSelf);
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            if (inventoryPanel.activeSelf)
                UpdateUI();
        }
    }

    void GenerateSlots()
    {
        for (int i = 0; i < playerInventory.inventory.MaxSlots; i++)
        {
            GameObject slotGO = Instantiate(slotPrefab, inventoryPanel.transform);
            InventorySlotUI slotUI = slotGO.GetComponent<InventorySlotUI>();
            slotUIs.Add(slotUI);
        }
    }

    public void UpdateUI()
    {
        var items = playerInventory.inventory.items;

        for (int i = 0; i < slotUIs.Count; i++)
        {
            if (i < items.Count)
            {
                slotUIs[i].SetSlot(items[i]);
            }
            else
            {
                slotUIs[i].ClearSlot();
            }
        }
    }
}

