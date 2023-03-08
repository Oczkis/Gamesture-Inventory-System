using UnityEngine;

public class MessagesManager : MonoBehaviour
{
    private static MessagesManager _instance;
    public static MessagesManager Instance { get { return _instance; } }

    public Message[] messages = new Message[0];
    public float messagesDuration;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    void Start()
    {
        Inventory.OnItemEquipped += MessageManagerHandleItemEquipped;
    }

    void OnDisable()
    {
        Inventory.OnItemEquipped -= MessageManagerHandleItemEquipped;
    }

    private void MessageManagerHandleItemEquipped(EquipmentSlot eQSlot)
    {
        Item item = eQSlot.Item;

        if(item != null)
            DisplayMessage("Equipped " + eQSlot.Item.itemName);
    }

    public void DisplayMessage(string messageText)
    {
        Message newMessage = GetFristNonActiveMessage();

        newMessage.DisplayMessage(messageText, messagesDuration);

        AudioManager.Instance.PlayRandomAudioSound("Message");
    }

    private Message GetFristNonActiveMessage()
    {
        foreach (Message message in messages)
        {
            if (message.gameObject.activeSelf)
                continue;

            return message;
        }

        return messages[^1];
    }

}
