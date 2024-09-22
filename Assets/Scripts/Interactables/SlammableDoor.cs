using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlammableDoor : MonoBehaviour, DoorTrigger
{
    [SerializeField] private AudioClip _doorSlamClip;
    [SerializeField] private UnityEvent _onDoorSlammed;

    private Door _door;

    private bool _wasDoorSlammed;

    private void Start()
    {
        _door = GetComponent<Door>();
    }

    public void HandleTriggerEnter()
    {
        if (!_wasDoorSlammed)
        {
            _wasDoorSlammed = true;
            _door.SlamDoor();
            SoundEffectsManager.Instance.PlaySoundEffect(_doorSlamClip, transform.position);

            if (_onDoorSlammed != null)
            {
                _onDoorSlammed.Invoke();
            }
        }
    }

    public void HandleTriggerExit()
    {

    }
}
