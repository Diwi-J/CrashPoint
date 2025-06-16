using UnityEngine;
using UnityEngine.Rendering;

/*public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int quantity;

    [SerializeField]
    private Sprite sprite;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inventoryManager.AddItem(itemName, quantity, sprite);
            Destroy(gameObject);
            Debug.Log("Item collected: " + itemName + ", Quantity: " + quantity);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Item collected: " + itemName + ", Quantity: " + quantity);
            Destroy(gameObject);
        }
    }

}*/