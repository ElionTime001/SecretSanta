using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Cube intereacted with");
    }
}
