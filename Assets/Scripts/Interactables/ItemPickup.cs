using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public string itemName = "Mysterious Object";

    public string GetInteractPrompt()
    {
        return "[E] Pick up " + itemName;
    }

    public void Interact(PlayerInteraction player)
    {
        Debug.Log("Picked up " + itemName);
        Destroy(gameObject);
    }
}
