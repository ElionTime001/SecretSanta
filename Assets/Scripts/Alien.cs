using UnityEngine;

public class Alien : MonoBehaviour, IInteractable
{
    [Header("Interaction Requirement")]
    [SerializeField] private string requiredItemName;
    [Header("NPC name")]
    [SerializeField] private string alienName;

    public void Interact(GameObject interactor)
    {
        PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogWarning("Interactor has no inventory.");
            return;
        }

        if (alienName!="photo_alien" && alienName!="ship_alien") {

            if (inventory.HasItem(requiredItemName))
            {
                OnSuccessfulInteraction();
            }
            else
            {
                OnFailedInteraction();
            }

        }
        else
        {

            if (alienName=="photo_alien"){

                DialogueUI.Instance.ShowDialogue("Danger....");
                DialogueUI.Instance.showPhoto();

            } 
            if(alienName=="ship_alien"){

                if (UIItemIndicator.Instance.HasThreeShipParts())
                {
                    DialogueUI.Instance.ShowDialogue("(Oh. It gave me a key.)");
                    inventory.AddItem("key");
                    UIItemIndicator.Instance.LightUpImageThree();
                }
                else
                {
                    DialogueUI.Instance.ShowDialogue("Hehehehehe. Tasty. Machine tasty. More.");
                }


            }

        }

    }

    private void OnSuccessfulInteraction()
    {
        Debug.Log("Alien interaction successful.");
        DialogueUI.Instance.ShowDialogue("Thank you.... you dropped this....");
        UIItemIndicator.Instance.AddShipPart();
    }

    private void OnFailedInteraction()
    {
        Debug.Log($"Alien requires: {requiredItemName}");
        DialogueUI.Instance.ShowDialogue("Food. FOOD. Hungrysdadxasdxwa....");
    }
}
