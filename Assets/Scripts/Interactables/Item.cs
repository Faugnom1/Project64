using UnityEngine;

public enum ItemName
{
    GENERAL_KEY,
    LAB_KEY,
    UNDERGROUND_KEY
}

public class Item : Interactable
{
    [field: SerializeField] public ItemName ItemName { get; private set; }

    protected virtual void AddToPlayerInventory()
    {
        GameManager.Instance.PlayerInventory.AddToInventory(this);
    }
}
