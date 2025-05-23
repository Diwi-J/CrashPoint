using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    public Text amountText;

    private ItemInstance item;

    public void SetSlot(ItemInstance itemInstance)
    {
        item = itemInstance;

        if (item != null && item.itemData != null)
        {
            icon.sprite = item.itemData.ItemIcon;
            icon.enabled = true;

            if (item.itemData.MaxStack > 1)
                amountText.text = item.Amount.ToString();
            else
                amountText.text = "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        amountText.text = "";
    }
}
