using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Item> _inventory;
    [SerializeField] private GameObject _flareUICounter;

    private PlayerInput _playerInput;
    private int _currentFlareCount;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Player.Use.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Player.Use.Disable();
    }

    private void Update()
    {
        bool wasUsePressed = _playerInput.Player.Use.WasPressedThisFrame();

        if (wasUsePressed && _currentFlareCount > 0)
        {
            UseItem(ItemName.FLARE);
        }
    }

    public bool AddToInventory(Item item)
    {
        if (item.ItemName == ItemName.FLARE)
        {
            int flareCount = GetItemCount(ItemName.FLARE);

            if (flareCount < 3)
            {
                _currentFlareCount = flareCount;
                _inventory.Add(item);
                _flareUICounter.transform.GetChild(flareCount).gameObject.SetActive(true);
            }

            return flareCount < 3;
        }
        else
        {
            _inventory.Add(item);
        }

        return true;
    }

    private int GetItemCount(ItemName itemName)
    {
        int count = 0;

        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].ItemName == itemName)
            {
                count++;
            }
        }

        return count;
    }

    private int GetItemIndex(ItemName itemName)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].ItemName == itemName)
            {
                return i;
            }
        }

        return -1;
    }

    public bool HasItem(ItemName itemName)
    {
        return GetItemIndex(itemName) != -1;
    }

    public bool TryConsumeKey(ItemName keyName)
    {
        int keyIndex = GetItemIndex(keyName);

        if (keyIndex != -1)
        {
            _inventory.RemoveAt(keyIndex);
            return true;
        }

        // No key was found
        return false;
    }

    public void UseItem(ItemName itemName)
    {
        int itemIndex = GetItemIndex(itemName);

        Item item = _inventory[itemIndex];
        item.transform.position = new Vector2(transform.position.x, transform.position.y);
        item.gameObject.SetActive(true);
        item.Consume();

        _inventory.RemoveAt(itemIndex);

        if (itemName == ItemName.FLARE)
        {
            _flareUICounter.transform.GetChild(_currentFlareCount).gameObject.SetActive(false);
        }
    }
}
