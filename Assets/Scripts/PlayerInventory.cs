using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory (Debug)")]
    [SerializeField] private List<string> items = new List<string>();

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

   public void AddItem(string itemName)
    {  
        if (items.Contains(itemName))
            return;

        items.Add(itemName);
        Debug.Log($"Added item: {itemName}");

        switch (itemName)
        {
            case "weed":
                UIItemIndicator.Instance.LightUpImageOne();
                break;

            case "meat":
                Debug.Log($"Light up meat");
                UIItemIndicator.Instance.LightUpImageTwo();
                break;

            case "ship":
                UIItemIndicator.Instance.AddShipPart();
                break;
        }
    }
    public void RemoveItem(string itemName)
    {
        if (items.Remove(itemName))
        {
            Debug.Log($"Removed item: {itemName}");
        }
    }
}
