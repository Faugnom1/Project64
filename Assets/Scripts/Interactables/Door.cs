using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public interface DoorTrigger
{
    void HandleTriggerEnter();
    void HandleTriggerExit();
}

public class Door : Interactable
{
    [Header("Message Properties")]
    [SerializeField] private string _doorLockedTextKey;
    [SerializeField] private string _doorOpenedTextKey;

    [Header("Key Requirements")]
    [SerializeField] private ItemName _keyName;
    [SerializeField] private bool _eventControlled;
    [SerializeField] private bool _noKeyRequired;

    [SerializeField] private UnityEvent _onDoorOpened;

    private Animator _animator;
    private NavMeshObstacle _navObstacle;
    private DoorTrigger _doorTrigger;
    private ShadowCaster2D _shadowCaster;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        _navObstacle = GetComponent<NavMeshObstacle>();
        _doorTrigger = GetComponent<DoorTrigger>();
        _shadowCaster = GetComponent<ShadowCaster2D>();

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
            }

            if (IsPlayerInteracting() && GameManager.Instance.PlayerInventory.TryConsumeKey(_keyName) || (IsPlayerInteracting() && _noKeyRequired))
            {
                if (!_noKeyRequired)
                {
                    string message = ((string)TextManager.GetText(_doorOpenedTextKey)).Replace("{key}", _keyName.ToFormattedString());
                    MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
                }
                _canInteract = false;
                _animator.SetTrigger("DoorOpen");
                _onDoorOpened.Invoke();
                _navObstacle.enabled = false;
                _shadowCaster.enabled = false;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (_doorTrigger != null)
        {
            _doorTrigger.HandleTriggerEnter();
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (_doorTrigger != null)
        {
            _doorTrigger.HandleTriggerExit();
        }
    }

    public void SetInteractable(bool interactable)
    {
        _canInteract = interactable;
    }

    public void OpenDoor()
    {
        _canInteract = false;
        _animator.SetTrigger("DoorOpen");
        _navObstacle.enabled = false;
        _onDoorOpened.Invoke();
        _shadowCaster.enabled = false;
    }

    public void SlamDoor()
    {
        _animator.SetTrigger("DoorSlam");
        _navObstacle.enabled = true;
        _canInteract = false;
    }
}
