using UnityEngine;

public enum ItemName {
    KEY
}

public class Item : Interactable
{
    [field: SerializeField] public ItemName Name { get; private set; }

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting())
        {
            GameManager.Instance.PlayerInteraction.AddToInventory(this);
            gameObject.SetActive(false);
        }
    }
}
