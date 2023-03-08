using UnityEngine;
using TMPro;

public class ItemCreator : MonoBehaviour
{
    private static ItemCreator _instance;

    public static ItemCreator Instance { get { return _instance; } }

    public string[] randomTitles = new string[0];
    public string[] randomFlavourDescriptions = new string[0];
    [SerializeField] Sprite[] itemQualityBackground = new Sprite[0];

    public Helpers.Quality selectedQuality;
    public Helpers.ItemType selectItemType = Helpers.ItemType.OneHand;

    public Item[] oneHandedItems = new Item[0];
    public Item[] twoHandedItems = new Item[0];
    public Item[] offHandedItems = new Item[0];
    public Item[] consumableItems = new Item[0];

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void SetQuality(TMP_Dropdown dropdown)
    {
        selectedQuality = (Helpers.Quality)dropdown.value;
    }

    public void SetWieldStyle(TMP_Dropdown dropdown)
    {
        selectItemType = (Helpers.ItemType)dropdown.value;
    }

    public void CreateItemButton()
    {
        CreateItem();
    }

    public void CreateItem(Item item = null)
    {
        if (item == null)
        {
            GenerateItem();
        }
        else
        {
            Inventory.Instance.AddItem(item);
        }       
    }

    public Item GenerateItem(Helpers.Quality quality = default, Helpers.ItemType itemType = default)
    {
        if (quality == default)
            quality = selectedQuality;

        if (itemType == default)
            itemType = selectItemType;

        Item item = Instantiate(GetItemByType(itemType));

        string itemName = GenerateItemName(item.basicItemName, quality);
        string flavour = itemType == Helpers.ItemType.Consumable ? "My polish friend said it is from Ladybug, but I thought apples are vegetarian" : randomFlavourDescriptions[Random.Range(0, randomFlavourDescriptions.Length)];
        string description = GenerateDescription(quality, itemType);
        int quantity = itemType == Helpers.ItemType.Consumable ? Random.Range(1, 10) : 1;

        item.itemName = itemName;
        item.flavourDescription = flavour;
        item.statDescription = description;
        item.quantity = quantity;
        item.qualityBackground = itemType == Helpers.ItemType.Consumable ? itemQualityBackground[0] : itemQualityBackground[(int)quality];

        CreateItem(item);

        return item;
    }

    private Item GetItemByType(Helpers.ItemType itemType)
    {
        switch (itemType)
        {
            case Helpers.ItemType.OneHand: return oneHandedItems[Random.Range(0, oneHandedItems.Length)];
            case Helpers.ItemType.TwoHand: return twoHandedItems[Random.Range(0, twoHandedItems.Length)];
            case Helpers.ItemType.Offhand: return offHandedItems[Random.Range(0, offHandedItems.Length)];
            default: return consumableItems[Random.Range(0, consumableItems.Length)];
        }
    }

    public string GenerateItemName(string basicName, Helpers.Quality quality)
    {
        switch (quality)
        {
            case Helpers.Quality.Rare: return "<color=green>" + basicName + " " + randomTitles[Random.Range(0, randomTitles.Length)] + "</color>";
            case Helpers.Quality.Epic: return "<color=purple>" + basicName + " " + randomTitles[Random.Range(0, randomTitles.Length)] + "</color>";
            case Helpers.Quality.Legendary: return "<color=red>" + basicName + " " + randomTitles[Random.Range(0, randomTitles.Length)] + "</color>";
            default : return "<color=white>" + basicName + " " + randomTitles[Random.Range(0, randomTitles.Length)] + "</color>";
        }
    }

    private string GenerateDescription(Helpers.Quality quality, Helpers.ItemType itemType)
    {
        if (itemType == Helpers.ItemType.Consumable)
            return itemType.ToString() + "\n" + "Eat Me :)";

        int power = Random.Range(1, 11) * (int)quality +1 * (itemType == Helpers.ItemType.TwoHand ? 3 : 1);
        int defense = Random.Range(1, 6) * (int)quality +1 * (itemType == Helpers.ItemType.Offhand ? 3 : 1);
        int strength = Random.Range(1, 3) * (int)quality +1 * (itemType == Helpers.ItemType.OneHand ? 3 : 1);
        
        return itemType.ToString() + "\n" + "Power : " + power.ToString() + "\n" + "Defense : " + defense.ToString() + "\n" + "Strength : " + strength.ToString();
    }
}
