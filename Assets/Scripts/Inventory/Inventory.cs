using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;

    public static Inventory Instance { get { return _instance; } }

    private List<InventorySlot> _slots = new List<InventorySlot>();

    public static Action<EquipmentSlot> OnItemEquipped;
    public static Action<EquipmentSlot> OnItemDeEquipped;
    public static Action<Slot> OnSlotPointerEnter;
    public static Action<Slot> OnSlotStartDrag;
    
    public static Action OnSlotPointerExit;   
    public static Action OnSlotStopDrag;

    [HideInInspector] public EquipmentSlot MainHandSlot { get { return _mainHandSlot; } }
    [HideInInspector] public EquipmentSlot OffHandSlot { get { return _offHandSlot; } }  
    [HideInInspector] public Slot EmptySlot { get { return _slots.FirstOrDefault(x => x.Item == null); } }

    [Header("References")]
    [SerializeField] private InventorySlot _inventorySlotPrefab;
    [SerializeField] private Transform _slotsParent;
    [SerializeField] private Slot _thrashSlot;
    [SerializeField] private EquipmentSlot _mainHandSlot;
    [SerializeField] private EquipmentSlot _offHandSlot;

    void Awake()
    {
        if(_instance == null)
            _instance = this;
    }

    public void PointerEnterSlot(Slot slot)
    {
        OnSlotPointerEnter?.Invoke(slot);
    }

    public void PointerExitSlot()
    {
        OnSlotPointerExit?.Invoke();
    }

    public void StartSlotDrag(Slot slot)
    {
        OnSlotStartDrag?.Invoke(slot);
    }

    public void StopSlotDrag()
    {
        OnSlotStopDrag?.Invoke();
    }

    public void ItemEquipped(EquipmentSlot equipmentSlot)
    {
        OnItemEquipped?.Invoke(equipmentSlot);
    }

    public void ItemDeEquipped(EquipmentSlot equipmentSlot)
    {
        OnItemDeEquipped?.Invoke(equipmentSlot);
    }

    public void AddInventorySlot()
    {
        _slots.Add(Instantiate(_inventorySlotPrefab, _slotsParent));
    }

    public void TrySwapSlots(Slot fromSlot, Slot toSlot)
    {
        if (!toSlot.IsItemAccepted(fromSlot.Item) || !fromSlot.IsItemAccepted(toSlot.Item)) { return; }

        if (toSlot.Item != null && toSlot.Stackable(fromSlot.Item))
        {
            int quantity = CalculateTransfer(fromSlot.Item, toSlot.Item);

            toSlot.AddQuantity(quantity);
            fromSlot.RemoveQuantity(quantity);

            return;
        }

        SwapSlots(fromSlot, toSlot);
    }

    private void SwapSlots(Slot fromSlot, Slot toSlot)
    {
        Item tmp = fromSlot.Item;

        fromSlot.PlaceItemOnSlot(toSlot.Item);
        toSlot.PlaceItemOnSlot(tmp);
    }

    public bool AddItem(Item newItem)
    {
        if (newItem.maxQuantity > 1)
        {
            Slot slotForNewItem = FindFirstSameItemSlot(newItem);

            if (slotForNewItem != null)
            { 
                int quantity = CalculateTransfer(newItem, slotForNewItem.Item);

                slotForNewItem.AddQuantity(quantity);

                newItem.quantity -= quantity;

                if (newItem.quantity > 0)
                    AddItem(newItem);

                return true;
            }
        }

        if (EmptySlot == null) { return false; }

        EmptySlot.PlaceItemOnSlot(newItem);

        return true;
    }

    public void ThrashItem(Slot slot)
    {
        SwapSlots(slot, _thrashSlot);
    }

    public Slot FindFirstSameItemSlot(Item item)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.Item != null)
            {
                Item itemOnSlot = slot.Item;
                if (itemOnSlot.itemIcon == item.itemIcon && !itemOnSlot.MaxStack)
                    return slot;
            }
        }

        return null;
    }

    private int CalculateTransfer(Item fromItem, Item toItem)
    { 
        int toSlotSpace = toItem.maxQuantity - toItem.quantity;
        int transferQuantity = fromItem.quantity;

        if(toSlotSpace < transferQuantity)
        {
            transferQuantity = toSlotSpace;
        }

        return transferQuantity;
    }
}
