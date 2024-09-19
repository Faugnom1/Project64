using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalDoor : MonoBehaviour, DoorTrigger
{
    private Door _door;

    private void Start()
    {
        _door = GetComponent<Door>();
        _door.SetInteractable(false);
    }

    public void HandleTriggerEnter()
    {
        _door.SetInteractable(true);
    }

    public void HandleTriggerExit()
    {
        _door.SetInteractable(false);
    }
}
