using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName = "Elixer of Life";
    public Sprite icon;
    public bool isStackable = true;

}
