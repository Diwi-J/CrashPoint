using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Class", menuName = "Item/Consumable")]
public class ConsumableClass : ItemClass
{
    [Header("Consumable")]//Data specific to consumables
    public float healthAdded;
    public float thirstAdded;
    public float insanitySubtracted;
    public float sleepAdded;
    public float hungerAdded;
    public int healthRestoreAmount;


    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return null; }
    public override MiscClass GetMisc() { return null; }
    public override ConsumableClass GetConsumable() { return null; }
}
