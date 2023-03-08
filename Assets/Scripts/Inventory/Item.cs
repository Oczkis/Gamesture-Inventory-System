using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    [HideInInspector] public string itemName;
    [HideInInspector] public string statDescription;
    [HideInInspector] public string flavourDescription;
    [HideInInspector] public int quantity;
    [HideInInspector] public Sprite qualityBackground;

    [HideInInspector] public bool MaxStack => quantity >= maxQuantity;

    public Sprite itemIcon;
    public Mesh itemModel;

    public int maxQuantity;  
    public string basicItemName;

    public virtual Helpers.ItemType GetItemType()
    {
        return Helpers.ItemType.OneHand;
    }

    public virtual void UseItem(Slot slot)
    {

    }
}
