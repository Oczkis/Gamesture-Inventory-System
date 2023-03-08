using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumables")]
public class ConsumableItem : Item
{
    public override Helpers.ItemType GetItemType()
    {
        return Helpers.ItemType.Consumable;
    }

    public override void UseItem(Slot slot)
    {
        MessagesManager.Instance.DisplayMessage("That was a delicious " + itemName);
        AudioManager.Instance.PlayRandomAudioSound("Eat");
        slot.RemoveQuantity(1);
    }
}
