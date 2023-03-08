using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    [SerializeField] private TMP_Text itemNameText, statDescriptionText, itemFlavourText;
    [SerializeField] private GameObject itemToolTipPanel;
    [SerializeField] private GameObject quitGamePopUp;

    [SerializeField] private RectTransform _dragPlane;
    RectTransform dragIconRectTransform;
    public Image draggingIcon;
    
    void Awake()
    {
        if (_instance == null)
            _instance = this;

        dragIconRectTransform = draggingIcon.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            quitGamePopUp.SetActive(!quitGamePopUp.activeSelf);
        }
    }

    void OnEnable()
    {
        Inventory.OnSlotStartDrag += UIManagerOnSlotStartDrag;
        Inventory.OnSlotStopDrag += UIManagerOnSlotStopDrag;

        Inventory.OnSlotPointerEnter += UIManagerOnPointerEnterSlot;
        Inventory.OnSlotPointerExit += UIManagerOnPointerExitSlot;
    }

    void OnDisable()
    {
        Inventory.OnSlotStartDrag -= UIManagerOnSlotStartDrag;
        Inventory.OnSlotStopDrag -= UIManagerOnSlotStopDrag;

        Inventory.OnSlotPointerEnter -= UIManagerOnPointerEnterSlot;
        Inventory.OnSlotPointerExit -= UIManagerOnPointerExitSlot;
    }

    private void UIManagerOnSlotStartDrag(Slot slot)
    {
        draggingIcon.gameObject.SetActive(true);
        draggingIcon.sprite = slot.Item.itemIcon;
    }

    private void UIManagerOnSlotStopDrag()
    {
        draggingIcon.gameObject.SetActive(false);
    }

    private void UIManagerOnPointerEnterSlot(Slot slot)
    {
        Item item = slot.Item;
        itemNameText.text = item.itemName;
        statDescriptionText.text = item.statDescription;
        itemFlavourText.text = item.flavourDescription;
        itemToolTipPanel.SetActive(true);
    }

    private void UIManagerOnPointerExitSlot()
    {
        itemToolTipPanel.SetActive(false);
    }

    public void SetDraggedPosition(PointerEventData data)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_dragPlane, data.position, data.pressEventCamera, out Vector3 mousePos))
        {
            dragIconRectTransform.position = mousePos;
        }
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
