using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public interface DoorTrigger
{
    void HandleTrigger();
}

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
    [SerializeField] private bool _eventControlled;
    [SerializeField] private bool _noKeyRequired;

    [SerializeField] private UnityEvent _onDoorOpened;

    private Animator _animator;
    private NavMeshObstacle _navObstacle;
    private DoorTrigger _doorTrigger;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        _navObstacle = GetComponent<NavMeshObstacle>();
        _doorTrigger = GetComponent<DoorTrigger>();

        if (_eventControlled)
        {
            _canInteract = false;
        }
    }

    protected override void Update()
    {
        base.Update();

        if (!_eventControlled) { }
        {
            if (IsPlayerInteracting() && !_messageShown && !GameManager.Instance.PlayerInventory.HasItem(_keyName))
            {
                if (!_noKeyRequired)
                {
                    string message = ((string)TextManager.GetText(_doorLockedTextKey)).Replace("{key}", _keyName.ToFormattedString());
                    MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
                    _messageShown = true;
                }
                SoundEffectsManager.Instance.PlaySoundEffect(_onDoorLockedClip, transform, _onDoorLockedVolume);

            }

            if (IsPlayerInteracting() && GameManager.Instance.PlayerInventory.TryConsumeKey(_keyName) || IsPlayerInteracting() && _noKeyRequired)
            {
                if (!_noKeyRequired)
                {
                    string message = ((string)TextManager.GetText(_doorOpenedTextKey)).Replace("{key}", _keyName.ToFormattedString());
                    MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
                }
                _canInteract = false;
                _animator.SetTrigger("DoorOpen");
                SoundEffectsManager.Instance.PlaySoundEffect(_onDoorOpenedClip, transform, _onDoorOpenedVolume);
                _onDoorOpened.Invoke();
                _navObstacle.enabled = false;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_doorTrigger != null)
        {
            _doorTrigger.HandleTrigger();
        }
    }

    public void OpenDoor()
    {
        _canInteract = false;
        _animator.SetTrigger("DoorOpen");
        SoundEffectsManager.Instance.PlaySoundEffect(_onDoorOpenedClip, transform, _onDoorOpenedVolume);
        _navObstacle.enabled = false;
        _onDoorOpened.Invoke();
    }

    public void SlamDoor()
    {
        _animator.SetTrigger("DoorSlam");
        _navObstacle.enabled = true;
        _canInteract = false;
    }
}
