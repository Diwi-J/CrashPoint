using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public int Amount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<PlayerInventory>().inventory;

            if (inventory.AddItem(itemData, Amount))
            {
                Destroy(gameObject);
            }
        }
    }
}
