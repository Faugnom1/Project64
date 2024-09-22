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
    [SerializeField] private Item _rewardItem;
    [SerializeField] private ItemName _name;

    private bool _itemGivenToPlayer;
    private Item _newRewardItem;

    protected override void Start()
    {
        base.Start();

        if (_rewardItem != null)
        {
            _newRewardItem = Instantiate(_rewardItem, new Vector3(0, 0, 0), Quaternion.identity);
            _newRewardItem.ItemName = _name;
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
            GameManager.Instance.PlayerInventory.AddToInventory(_newRewardItem);
            StartCoroutine(RewardMessage());
            MessageManager.Instance.OnMessageRead.RemoveListener(GivePlayerItem);
        }
    }

    private IEnumerator RewardMessage()
    {
        MessageManager.Instance.ShowMessage("...", _messageType, _messageSpeed, gameObject);

        yield return new WaitForSeconds(1f);

        string message = ((string)TextManager.GetText("reward_item")).Replace("{item}", _newRewardItem.ItemName.ToFormattedString());
        MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
    }
}
