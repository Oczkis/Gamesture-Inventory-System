
public class InventorySlot : Slot
{
    protected override void UseSlot()
    {
        _item.UseItem(this);
    }
}
