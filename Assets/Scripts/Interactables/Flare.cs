using System.Collections;
using UnityEngine;

public class Flare : Item
{
    [Header("Message Properties")]
    [SerializeField] private string _itemTextKey;

    [Header("Flare Stats")]
    [SerializeField] private float _lifetime;

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
        _canInteract = false;
        StartCoroutine(FlareOpen());
    }

    public IEnumerator FlareOpen()
    {
        _animator.SetTrigger("FlareOpen");
        MessageManager.Instance.ShowMessage(TextManager.GetText("flare_drop"), _messageType, _messageSpeed);

        yield return new WaitForSeconds(_lifetime);

        Destroy(gameObject);
    }
}
