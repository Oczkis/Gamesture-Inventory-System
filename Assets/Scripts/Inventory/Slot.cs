using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private Sprite emptyIconSprite;
    [SerializeField] private Image slotItemIcon;
    [SerializeField] private Image qualityBackground;
    [SerializeField] private TMP_Text quantityText;

    public bool Stackable(Item toStackWith) =>_item !=null && _item.itemIcon == toStackWith.itemIcon && !_item.MaxStack;

    protected Item _item;
    public Item Item { get { return _item; } }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button != PointerEventData.InputButton.Right || _item == null) { return; }
        AudioManager.Instance.PlayRandomAudioSound("Click");
        UseSlot();
    }

    public void OnDrag(PointerEventData eventData)
    {
        UIManager.Instance.SetDraggedPosition(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(_item == null) { return; }
        AudioManager.Instance.PlayRandomAudioSound("Click");        
        Inventory.Instance.StartSlotDrag(this);
        slotItemIcon.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {       
        if (_item != null)
            Inventory.Instance.PointerEnterSlot(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        foreach (RaycastResult result in raycastResults)
        {
            Slot toSlot = result.gameObject.GetComponent<Slot>();

            if (toSlot != null)            
                Inventory.Instance.TrySwapSlots(this, toSlot);
        }
        
        Inventory.Instance.StopSlotDrag();
        slotItemIcon.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.Instance.PointerExitSlot();
    }

    protected virtual void UseSlot()
    {

    }

    public virtual void PlaceItemOnSlot(Item newItem)
    {
        _item = newItem;
        UpdateSlotVisual();
    }

    protected virtual void UpdateSlotVisual()
    {
        if (_item != null)
        {
            qualityBackground.sprite = _item.qualityBackground;
            quantityText.text = _item.quantity.ToString();
        }

        slotItemIcon.sprite = _item == null ? emptyIconSprite : _item.itemIcon;
        qualityBackground.gameObject.SetActive(_item != null);
        quantityText.gameObject.SetActive(_item != null && _item.quantity > 1);
    }

    public void RemoveQuantity(int quantity)
    {
        _item.quantity -= quantity;

        if (_item.quantity == 0)
        {
            Inventory.Instance.ThrashItem(this);
        }
        else
        {
            UpdateSlotVisual();
        }        
    }

    public void AddQuantity(int quantity)
    {
        _item.quantity += quantity;
        UpdateSlotVisual();
    }

    public virtual bool IsItemAccepted(Item item)
    {
        return true;
    }
}
