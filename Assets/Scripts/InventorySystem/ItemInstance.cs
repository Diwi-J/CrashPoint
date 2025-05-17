using UnityEngine;

[System.Serializable] 
public class ItemInstance
{
    public ItemData itemData;
    public int Amount;

    public ItemInstance(ItemData data, int amount = 1)
    {
        itemData = data;
        Amount = amount;
    }
}
