using UnityEngine;

public enum ItemName
{
    FLARE,
    ROOM_KEY,
    ARMORY_KEY,
    LAB_KEY,
    OFFICE_WING_KEY,
    EXIT_KEY,
    SCIENTIST_OFFICE_KEY,
    SECRET_LAB_KEY
}

public class Item : Interactable
{
    [field: SerializeField] public ItemName ItemName { get; set; }

    protected virtual bool AddToPlayerInventory()
    {
        return GameManager.Instance.PlayerInventory.AddToInventory(this);
    }

    public virtual void Consume()
    {

    }
}
