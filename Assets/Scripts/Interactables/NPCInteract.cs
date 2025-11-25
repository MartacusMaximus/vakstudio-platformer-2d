using UnityEngine;


public class NPCInteract : MonoBehaviour, IInteractable
{
    public string GetInteractPrompt() => "[E] Talk";

    public void Interact(PlayerInteraction player)
    {
        Debug.Log("NPC talking...");
        // Dialogue system here
    }
}
