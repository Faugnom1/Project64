using UnityEngine;

public class Door : Interactable
{
    [Header("Message Properties")]
    [SerializeField] private string _doorLockedTextKey;
    [SerializeField] private string _doorOpenedTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onDoorLockedClip;
    [SerializeField] private float _onDoorLockedVolume;
    [SerializeField] private AudioClip _onDoorOpenedClip;
    [SerializeField] private float _onDoorOpenedVolume;

    [Header("Key Requirements")]
    [SerializeField] private ItemName _keyName;

    private bool _wasSlammed;

    private Animator _animator;

    protected override void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (_canInteract && IsPlayerInteracting() && !_messageShown && !GameManager.Instance.PlayerInventory.HasKey(_keyName))
        {
            _messageShown = true;
            SoundEffectsManager.Instance.PlaySoundEffect(_onDoorLockedClip, transform, _onDoorLockedVolume);
            MessageManager.Instance.ShowMessage(TextManager.GetText(_doorLockedTextKey), _messageType, _messageSpeed);
        }

        if (_canInteract && IsPlayerInteracting() && GameManager.Instance.PlayerInventory.ConsumeKey(_keyName))
        {
            _canInteract = false;
            _animator.SetTrigger("DoorOpen");
            SoundEffectsManager.Instance.PlaySoundEffect(_onDoorOpenedClip, transform, _onDoorOpenedVolume);
            MessageManager.Instance.ShowMessage(TextManager.GetText(_doorOpenedTextKey), _messageType, _messageSpeed);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!_wasSlammed)
        {
            SlamDoor();
        }
    }

    public void SlamDoor()
    {
        _animator.SetTrigger("DoorSlam");
        _canInteract = false;
        _wasSlammed = true;
    }
}
