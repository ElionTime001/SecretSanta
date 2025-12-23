using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        Debug.Log("Cube intereacted with");
    }
}
