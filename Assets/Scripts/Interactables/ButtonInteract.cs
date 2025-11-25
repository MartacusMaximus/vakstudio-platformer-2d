using UnityEngine;

public class ButtonInteract : MonoBehaviour, IInteractable
{
    public string GetInteractPrompt() => "[E] Press Button";

    public void Interact(PlayerInteraction player)
    {
        Debug.Log("Button pressed!");
        // Trigger animation, open door, etc.
    }
}

