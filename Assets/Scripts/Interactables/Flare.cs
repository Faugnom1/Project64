using UnityEngine;

public class Flare : Item
{
    [Header("Message Properties")]
    [SerializeField] private string _itemTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onInteractClip;
    [SerializeField] private float _onInteractClipVolume;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting())
        {
            bool isAdded = AddToPlayerInventory();

            if (isAdded)
            {
                _canInteract = false;

                // Play sound effect
                SoundEffectsManager.Instance.PlaySoundEffect(_onInteractClip, transform, _onInteractClipVolume);

                // Show message
                if (!_messageShown)
                {
                    _messageShown = true;
                    MessageManager.Instance.ShowMessage(TextManager.GetText(_itemTextKey), _messageType, _messageSpeed);
                }

                // Stop updates/render
                gameObject.SetActive(false);
            }
            else
            {
                if (!_messageShown)
                {
                    _messageShown = true;
                    MessageManager.Instance.ShowMessage(TextManager.GetText("max_item_capcity"), _messageType, _messageSpeed);
                }
            }
        }
    }

    public override void Consume()
    {
        _animator.SetTrigger("FlareOpen");
        _canInteract = false;
    }
}
