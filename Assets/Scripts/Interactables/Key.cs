using UnityEngine;
using UnityEngine.Events;

public class Key : Item
{
    [Header("Message Properties")]
    [SerializeField] private string _itemTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onInteractClip;
    [SerializeField] private float _onInteractClipVolume;

    [SerializeField] private UnityEvent _onPickup;

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting())
        {
            // Play sound effect
            SoundEffectsManager.Instance.PlaySoundEffect(_onInteractClip, transform, _onInteractClipVolume);

            // Show message
            if (!_messageShown)
            {
                _messageShown = true;
                string message = ((string)TextManager.GetText(_itemTextKey)).Replace("{key}", ItemName.ToFormattedString());
                MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
            }

            // Add to inventory and stop updates/render
            AddToPlayerInventory();
            gameObject.SetActive(false);
            _onPickup.Invoke();
        }
    }
}
