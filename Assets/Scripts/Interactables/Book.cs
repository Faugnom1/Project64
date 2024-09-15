using UnityEngine;

public class Book : Interactable
{
    [SerializeField] private string _bookTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onReadClip;
    [SerializeField] private float _onReadClipVolume;

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting() && !_messageShown)
        {
            // Play sound effect
            SoundEffectsManager.Instance.PlaySoundEffect(_onReadClip, transform, _onReadClipVolume);

            // Show message
            _messageShown = true;
            MessageManager.Instance.ShowMessage(TextManager.GetText(_bookTextKey), _messageType, _messageSpeed);
        }
    }
}
