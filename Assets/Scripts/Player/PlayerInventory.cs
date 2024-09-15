using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> _inventory;

    public void AddToInventory(Item item)
    {
        _inventory.Add(item);
    }

    private int GetKeyIndex()
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].ItemName == ItemName.KEY)
            {
                return i;
            }
        }

        return -1;
    }

    public bool HasKey()
    {
        return GetKeyIndex() != -1;
    }

    public bool ConsumeKey()
    {
        int keyIndex = GetKeyIndex();

        if (keyIndex != -1)
        {
            _inventory.RemoveAt(keyIndex);
            return true;
        }

        // No key was found
        return false;
    }
}
