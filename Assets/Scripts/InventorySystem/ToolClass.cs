using UnityEngine;


[CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Tool")]
public class ToolClass : ItemClass
{
    [Header("Tool")] // Data specific to tools
    public ToolType toolType;
    public enum ToolType
    {
        AK47,
        Axe,
        FishingRod,
        Rock,
    }

   
    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return this; }
    public override MiscClass GetMisc() { return null; }
    public override ConsumableClass GetConsumable() { return null; }
}
