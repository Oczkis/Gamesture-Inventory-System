
public class EquipmentSlot : Slot
{
    public bool isMainHand;

    protected override void UseSlot()
    {
        DeEquipItem(this);
    }

    public override void PlaceItemOnSlot(Item newItem)
    {
        if (newItem?.GetItemType() == Helpers.ItemType.TwoHand)
        {
            if (Inventory.Instance.OffHandSlot.Item != null)
            {
                if(!DeEquipItem(Inventory.Instance.OffHandSlot))
                    return;
            }
        }
        else if (newItem != null && isMainHand == false && Inventory.Instance.MainHandSlot.Item != null && Inventory.Instance.MainHandSlot.Item.GetItemType() == Helpers.ItemType.TwoHand)
        {
            if (!DeEquipItem(Inventory.Instance.MainHandSlot))
                return;
        }

        _item = newItem;
        UpdateSlotVisual();
    }

    protected override void UpdateSlotVisual()
    {
        base.UpdateSlotVisual();
        Inventory.Instance.ItemEquipped(this);
    }

    public bool DeEquipItem(EquipmentSlot slot = null)
    {
        if (slot == null)
            slot = this;

        Slot emptySlot = Inventory.Instance.EmptySlot;

        if (emptySlot == null)
        {
            MessagesManager.Instance.DisplayMessage("I need more inventory space!");
            return false;
        }

        Inventory.Instance.ItemDeEquipped(slot);
        Inventory.Instance.TrySwapSlots(slot, emptySlot);

        return true;
    }

    public override bool IsItemAccepted(Item item)
    {
        if (item == null)
            return true;

        if (item.GetItemType() == Helpers.ItemType.TwoHand && Inventory.Instance.EmptySlot == null && Inventory.Instance.OffHandSlot.Item != null && Inventory.Instance.MainHandSlot != null)
        {
            MessagesManager.Instance.DisplayMessage("I need more inventory space!");
            return false;
        }
            
        return (isMainHand ? item.GetItemType() == Helpers.ItemType.TwoHand : item.GetItemType() == Helpers.ItemType.Offhand)
                       || item.GetItemType() == Helpers.ItemType.OneHand;
    }
}
