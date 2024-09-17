using UnityEngine;

public enum ItemName
{
    FLARE,
    GENERAL_KEY,
    LAB_KEY,
    UNDERGROUND_KEY
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
