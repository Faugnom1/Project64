using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> _inventory;

    public void AddToInventory(Item item)
    {
        _inventory.Add(item);
    }

    private int GetKeyIndex(ItemName keyName)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].ItemName == keyName)
            {
                return i;
            }
        }

        return -1;
    }

    public bool HasKey(ItemName keyName)
    {
        return GetKeyIndex(keyName) != -1;
    }

    public bool ConsumeKey(ItemName keyName)
    {
        int keyIndex = GetKeyIndex(keyName);

        if (keyIndex != -1)
        {
            _inventory.RemoveAt(keyIndex);
            return true;
        }

        // No key was found
        return false;
    }
}
