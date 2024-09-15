using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventScriptable
{
    void TakeControl();
    void ReturnControl();

    void SetRotation(Quaternion rotation);
    void SetStartPosition(Vector2 position);
    void SetEndPosition(Vector2 position);
}
