using UnityEngine;

public enum ItemName
{
    KEY
}

public class Item : Interactable
{
    [field: SerializeField] public ItemName ItemName { get; private set; }
    [SerializeField] private string _itemTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onInteractClip;
    [SerializeField] private float _onInteractClipVolume;

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
                MessageManager.Instance.ShowMessage(TextManager.GetText(_itemTextKey), _messageType, _messageSpeed);
            }

            // Add to inventory and stop updates/render
            GameManager.Instance.PlayerInventory.AddToInventory(this);
            gameObject.SetActive(false);
        }
    }
}
