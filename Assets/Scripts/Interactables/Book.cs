using System.Collections;
using UnityEngine;

public class Book : Interactable
{
    [Header("Message Properties")]
    [SerializeField] private string _bookTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onReadClip;
    [SerializeField] private float _onReadClipVolume;

    [Header("Reward Properties")]
    [SerializeField] private RewardItemSO _rewardItem;

    private bool _itemGivenToPlayer;

    protected override void Start()
    {
        base.Start();

        if (_rewardItem != null)
        {
            MessageManager.Instance.OnMessageRead.AddListener(GivePlayerItem);
        }
    }

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting() && !_messageShown)
        {
            // Play sound effect
            SoundEffectsManager.Instance.PlaySoundEffect(_onReadClip, transform, _onReadClipVolume);

            // Show message
            _messageShown = true;
            MessageManager.Instance.ShowMessage(TextManager.GetText(_bookTextKey), _messageType, _messageSpeed, gameObject);
        }
    }

    private void GivePlayerItem(GameObject book)
    {
        if (gameObject == book && !_itemGivenToPlayer)
        {
            _itemGivenToPlayer = true;
            GameManager.Instance.PlayerInventory.AddToInventory(_rewardItem.Item);
            StartCoroutine(RewardMessage());
            MessageManager.Instance.OnMessageRead.RemoveListener(GivePlayerItem);
        }
    }

    private IEnumerator RewardMessage()
    {
        MessageManager.Instance.ShowMessage(".....", _messageType, _messageSpeed, gameObject);

        yield return new WaitForSeconds(2f);

        string message = ((string)TextManager.GetText(_rewardItem.TextKey)).Replace("{item}", _rewardItem.Name.ToFormattedString());
        MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
    }
}
