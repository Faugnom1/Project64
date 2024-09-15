using UnityEngine;

public class Door : Interactable
{
    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting() && GameManager.Instance.PlayerInteraction.ConsumeKey())
        {
            Destroy(gameObject);
        }
    }
}
