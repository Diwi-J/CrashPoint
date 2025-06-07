using UnityEngine;

public class ItemPickup1 : MonoBehaviour
{
    public ItemData1 itemData;
    public int Amount = 1;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<PlayerInventory1>().inventory;

            if (inventory.AddItem(ItemData1, Amount))
            {
                Destroy(gameObject);
            }
        }
    }*/
}
