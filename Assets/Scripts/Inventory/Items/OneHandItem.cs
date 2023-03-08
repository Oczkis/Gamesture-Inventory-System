using UnityEngine;

[CreateAssetMenu(menuName = "Item/One Hand")]
public class OneHandItem : Item
{
    public override Helpers.ItemType GetItemType()
    {
        return Helpers.ItemType.OneHand;
    }

    public override void UseItem(Slot slot)
    {
        Slot mainHandSlot = Inventory.Instance.MainHandSlot;
        Slot offHandSlot = Inventory.Instance.OffHandSlot;

        if (mainHandSlot.Item == null)
        {
            Inventory.Instance.TrySwapSlots(slot, mainHandSlot);
            return;
        }
        else if (mainHandSlot.Item.GetItemType() == Helpers.ItemType.TwoHand)
        {
            Inventory.Instance.TrySwapSlots(slot, mainHandSlot);
            return;
        }

        if (offHandSlot.Item == null)
        {
            Inventory.Instance.TrySwapSlots(slot, offHandSlot);
            return;
        }

        Inventory.Instance.TrySwapSlots(slot, mainHandSlot);
    }
}
