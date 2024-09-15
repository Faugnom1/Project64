using UnityEngine;

public class Book : Interactable
{
    [SerializeField] private string _bookTextKey;

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting() && !_messageShown)
        {
            _messageShown = true;
            MessageManager.Instance.ShowMessage(TextManager.GetText(_bookTextKey), _messageType, _messageSpeed);
        }
    }
}
