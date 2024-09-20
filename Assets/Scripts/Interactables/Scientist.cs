using System.Collections;
using UnityEngine;

public class Scientist : Interactable
{
    [Header("Message Properties")]
    [SerializeField] private string _textKey;
    [SerializeField] private string _choiceQuestionTextKey;
    [SerializeField] private string _choiceYesTextKey;
    [SerializeField] private string _choiceNoTextKey;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip _onReadClip;
    [SerializeField] private float _onReadClipVolume;

    [Header("Reward Properties")]
    [SerializeField] private RewardItemSO _rewardItem;

    private bool _itemGivenToPlayer;

    protected override void Start()
    {
        base.Start();

        if (_choiceQuestionTextKey != null)
        {
            ChoiceMessageManager.Instance.OnYesClick.AddListener(PlayMessageOnYes);
        }

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
            // MessageManager.Instance.ShowMessage(TextManager.GetText(_textKey), _messageType, _messageSpeed, gameObject);
            ChoiceMessageManager.Instance.ShowMessage(TextManager.GetText("scientist_1a_Question").ToString(), _messageSpeed, gameObject);
        }
    }

    private void PlayMessageOnYes(GameObject obj)
    {
        if (gameObject == obj)
        {
            MessageManager.Instance.ShowMessage(TextManager.GetText(_choiceYesTextKey), _messageType, _messageSpeed, gameObject);
            ChoiceMessageManager.Instance.OnYesClick.RemoveListener(PlayMessageOnYes);
        }
    }

    private void GivePlayerItem(GameObject obj)
    {
        if (gameObject == obj && !_itemGivenToPlayer)
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
