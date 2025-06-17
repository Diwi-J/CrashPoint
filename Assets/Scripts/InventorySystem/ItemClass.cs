using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item")] // Data common to all items
    public string itemName;
    public Sprite itemIcon;
    public bool isStackable = true;
    public bool isConsumable;

    public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
    public abstract MiscClass GetMisc();
    public abstract ConsumableClass GetConsumable();
}
