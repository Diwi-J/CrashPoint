using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string playerTag = "Player";
    public ItemClass itemData;      // Reference to the item being picked up
    public int quantity = 1;        // Quantity to add
    public AudioClip pickupSound;

       private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag))
            return;

        Debug.Log("Trigger entered by: " + other.name);

        if (itemData == null)
        {
            Debug.LogError("ItemPickup ERROR: itemData is NULL on " + gameObject.name);
            return;
        }

        var invManager = FindObjectOfType<InventoryManager>();

        if (invManager == null)
        {
            Debug.LogError("ItemPickup ERROR: InventoryManager not found in scene!");
            return;
        }

        Debug.Log("Trying to add item: " + itemData.itemName);

        bool added = invManager.Add(itemData, quantity);

        if (added)
        {
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Inventory full or could not add item: " + itemData.itemName);
        }
    }


}
