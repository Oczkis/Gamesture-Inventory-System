using UnityEngine;

public class CharacterVisualManager : MonoBehaviour
{
    public MeshFilter mainHandMesh;
    public MeshFilter offHandMesh;
    public Mesh emptyHand;
    [SerializeField] private Animator animator;

    void OnEnable()
    {
        Inventory.OnItemEquipped += CharacterUpdateHandVisuals;
    }

    void OnDisable()
    {
        Inventory.OnItemEquipped -= CharacterUpdateHandVisuals;
    }

    private void CharacterUpdateHandVisuals(EquipmentSlot equipmentSlot)
    {
        MeshFilter meshFilter = equipmentSlot.isMainHand ? mainHandMesh : offHandMesh;
        Item item = equipmentSlot.Item;
        meshFilter.mesh = item == null ? emptyHand : item.itemModel;

        string hand = equipmentSlot.isMainHand ? "RightHand" : "LeftHand";
        string position = "Idle";

        if(item != null)
        {
            position = equipmentSlot.Item.GetItemType().ToString();
        }
        animator.Play(hand + position);
    }
}
