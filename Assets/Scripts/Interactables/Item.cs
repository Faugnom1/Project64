using UnityEngine;

public enum ItemName
{
    KEY
}

public class Item : Interactable
{
    [field: SerializeField] public ItemName Name { get; private set; }

    [SerializeField] private string _itemTextKey;

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting())
        {
            if (!_messageShown)
            {
                _messageShown = true;
                MessageManager.Instance.ShowMessage(TextManager.GetText(_itemTextKey), _messageType, _messageSpeed);
            }

            GameManager.Instance.PlayerInteraction.AddToInventory(this);
            gameObject.SetActive(false);
        }
    }
}
