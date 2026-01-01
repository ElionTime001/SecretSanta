using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [Header("Item name")]
    [SerializeField] private string itemName;

    public void Interact(GameObject interactor)
    {
        PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();
        string objectName = gameObject.name;

        if (objectName != "gate")
        {
            if (inventory == null)
            {
                OnFailedInteraction();
                return;
            }

            inventory.AddItem(itemName);
            OnSuccessfulInteraction();
        }
        else
        {
            Debug.LogWarning("Interacted with gate");

            if (inventory.HasItem("key"))
            {
                OnSuccessfulInteraction();
            }
            else
            {
                DialogueUI.Instance.ShowDialogue("(Maybe those strange creatures can help me....)");
            }
        }
    }

    private void OnSuccessfulInteraction()
    {
        Destroy(gameObject);
    }

    private void OnFailedInteraction()
    {
        Debug.LogWarning("Interactor has no inventory.");
    }
}
