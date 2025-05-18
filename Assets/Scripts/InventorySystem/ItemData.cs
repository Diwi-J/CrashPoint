using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]   //https://www.youtube.com/watch?v=SGz3sbZkfkg&ab_channel=GameDevGuide/n
                                                                        //https://www.youtube.com/watch?v=pmBv0Cagx_o&ab_channel=GameDevBeginner
public class ItemData : ScriptableObject
{
    [Header("Item Details")]
    public string ItenName;
    public int MaxStack = 1;

    public ItemType itemType;

    [Header("Item Assets/Sprites")]
    public Sprite ItemIcon;
    public GameObject ItemPrefab;

    public enum ItemType
    {
        Weapon,
        Food,
        Drink,
        Files,
        Key
    }

    //Weapns Specific Fields
    [Header("Item Properties")]
    public float Damage;
    public float AttackRange;
    public float AttackRate;

    //Consumable Specific Fields
    [Header("Item Special Properties")]
    public bool RestoHealth;
    public bool RestoreHydration;
    public bool RestoreHunger;


}
