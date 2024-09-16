using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private string _doorLockedTextKey;
    [SerializeField] private string _doorOpenedTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onDoorLockedClip;
    [SerializeField] private float _onDoorLockedVolume;
    [SerializeField] private AudioClip _onDoorOpenedClip;
    [SerializeField] private float _onDoorOpenedVolume;

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting() && !_messageShown && !GameManager.Instance.PlayerInventory.HasKey())
        {
            // Play sound effect
            SoundEffectsManager.Instance.PlaySoundEffect(_onDoorLockedClip, transform, _onDoorLockedVolume);

            // Show message
            _messageShown = true;
            MessageManager.Instance.ShowMessage(TextManager.GetText(_doorLockedTextKey), _messageType, _messageSpeed);
        }

        if (IsPlayerInteracting() && GameManager.Instance.PlayerInventory.ConsumeKey())
        {
            // Play sound effect
            SoundEffectsManager.Instance.PlaySoundEffect(_onDoorOpenedClip, transform, _onDoorOpenedVolume);

            // Show message
            MessageManager.Instance.ShowMessage(TextManager.GetText(_doorOpenedTextKey), _messageType, _messageSpeed);
            Destroy(gameObject);
        }
    }
}
