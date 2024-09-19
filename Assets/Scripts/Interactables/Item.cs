using UnityEngine;

public enum ItemName
{
    FLARE,
    ROOM_KEY,
    ARMORY_KEY,
    LAB_KEY,
    EXIT_KEY
}

public class Item : Interactable
{
    [field: SerializeField] public ItemName ItemName { get; private set; }

    protected virtual bool AddToPlayerInventory()
    {
        return GameManager.Instance.PlayerInventory.AddToInventory(this);
    }

    public virtual void Consume()
    {

    }
}
