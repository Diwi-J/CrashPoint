using UnityEngine;

[CreateAssetMenu(fileName = "New Misc Class", menuName = "Item/Misc")]
public class MiscClass : ItemClass
{

    //Data specific to misc items
    public override ItemClass GetItem() { return this; }
    public override ToolClass GetTool() { return null; }
    public override MiscClass GetMisc() { return this; }
    public override ConsumableClass GetConsumable() { return null; }
}
