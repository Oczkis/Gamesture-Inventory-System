using UnityEngine;

public class ShowcaseSettings : MonoBehaviour
{
    [Header("Settings")]
    public int inventorySpace;
    public int numberOfStartItems;

    void Start()
    {
        for (int i = 0; i < inventorySpace; i++)
        {
            Inventory.Instance.AddInventorySlot();
        }

        if(numberOfStartItems > inventorySpace)
            numberOfStartItems = inventorySpace;

        PopulateInventoryWithRandomItems();
    }

    private void PopulateInventoryWithRandomItems()
    {
        for (int i = 0; i < numberOfStartItems; i++)
        {
            int randomQualityIndex = Random.Range(0, System.Enum.GetNames(typeof(Helpers.Quality)).Length);
            int itemTypeIndex = i % System.Enum.GetNames(typeof(Helpers.ItemType)).Length;

            ItemCreator.Instance.GenerateItem((Helpers.Quality)randomQualityIndex, (Helpers.ItemType)itemTypeIndex);
        }
    }
}
