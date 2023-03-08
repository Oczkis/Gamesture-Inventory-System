using UnityEngine;

[CreateAssetMenu(menuName = "Item/Off Hand")]
public class OffHandItem : Item
{
    public override Helpers.ItemType GetItemType()
    {
        return Helpers.ItemType.Offhand;
    }

    public override void UseItem(Slot slot)
    {
        Inventory.Instance.TrySwapSlots(slot, Inventory.Instance.OffHandSlot);
    }
}
