using UnityEngine;

[System.Serializable] 
public class ItemInstance
{
    public ItemData1 itemData;
    public int Amount;

    public ItemInstance(ItemData1 data, int amount = 1)
    {
        itemData = data;
        Amount = amount;
    }
}
