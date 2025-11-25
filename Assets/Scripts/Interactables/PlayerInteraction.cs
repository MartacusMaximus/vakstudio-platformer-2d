using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public InputAction interactAction;
    public GameObject promptUI;
    public TMPro.TextMeshProUGUI promptText;

    private IInteractable currentInteractable;

    void OnEnable()
    {
        interactAction.Enable();
    }

    void OnDisable()
    {
        interactAction.Disable();
    }

    void Update()
    {
        if (currentInteractable != null)
        {
            promptUI.SetActive(true);
            promptText.text = currentInteractable.GetInteractPrompt();

            if (interactAction.WasPressedThisFrame())
                currentInteractable.Interact(this);
        }
        else
        {
            promptUI.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interact))
            currentInteractable = interact;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interact) && interact == currentInteractable)
            currentInteractable = null;
    }
}
