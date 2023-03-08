
public class ThrashSlot : Slot
{
    public override void PlaceItemOnSlot(Item newItem)
    {
        MessagesManager.Instance.DisplayMessage("Destroyed " + newItem.itemName);
    }
}
