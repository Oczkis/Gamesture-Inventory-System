using UnityEngine;

[CreateAssetMenu(menuName = "Item/Two Hand")]
public class TwoHandItem : Item
{
    public override Helpers.ItemType GetItemType()
    {
        return Helpers.ItemType.TwoHand;
    }

    public override void UseItem(Slot slot)
    {
        Inventory.Instance.TrySwapSlots(slot, Inventory.Instance.MainHandSlot);
    }
}

