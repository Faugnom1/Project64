using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> _inventory;

    public void AddToInventory(Item item)
    {
        _inventory.Add(item);
    }

    public bool HasKey()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].ItemName == ItemName.KEY)
            {
                return true;
            }
        }

        return false;
    }

    public bool ConsumeKey()
    {
        // Try to consume a key
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].ItemName == ItemName.KEY)
            {
                _inventory.RemoveAt(i);
                return true;
            }
        }

        // No key was found
        return false;
    }
}
