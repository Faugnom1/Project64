using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _interactBubble;
    [SerializeField] private List<Item> _inventory;

    #region ==================Collision Detection=====================

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Interactable"))
        {
            _interactBubble.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Interactable"))
        {
            _interactBubble.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Interactable"))
        {
            _interactBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _interactBubble.SetActive(false);
    }

    #endregion

    #region ==================Inventory Methods=======================

    public void AddToInventory(Item item)
    {
        _inventory.Add(item);
    }

    public bool ConsumeKey()
    {
        // Try to consume a key
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].Name == ItemName.KEY)
            {
                _inventory.RemoveAt(i);
                return true;
            }
        }

        // No key was found
        return false;
    }

    #endregion
}
