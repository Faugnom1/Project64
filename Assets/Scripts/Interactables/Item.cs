using UnityEngine;

public enum ItemName
{
    ROOM_KEY,
    ARMORY_KEY
}

public class Item : Interactable
{
    [field: SerializeField] public ItemName ItemName { get; private set; }

    protected virtual void AddToPlayerInventory()
    {
        GameManager.Instance.PlayerInventory.AddToInventory(this);
    }
}
