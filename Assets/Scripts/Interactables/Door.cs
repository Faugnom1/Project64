using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private string _doorLockedTextKey;
    [SerializeField] private string _doorOpenedTextKey;

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting() && !_messageShown && !GameManager.Instance.PlayerInteraction.HasKey())
        {
            _messageShown = true;
            MessageManager.Instance.ShowMessage(TextManager.GetText(_doorLockedTextKey), _messageType, _messageSpeed);
        }

        if (IsPlayerInteracting() && GameManager.Instance.PlayerInteraction.ConsumeKey())
        {
            MessageManager.Instance.ShowMessage(TextManager.GetText(_doorOpenedTextKey), _messageType, _messageSpeed);
            Destroy(gameObject);
        }
    }
}
