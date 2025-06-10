using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActivated;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !menuActivated)
        {
            Time.timeScale = 0f; // Pause the game
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && menuActivated)
        {
            Time.timeScale = 1f; // Resume the game
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
    }
}