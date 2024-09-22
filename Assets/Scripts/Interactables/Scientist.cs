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
    private bool _choiceGivenToPlayer;
    private bool _playerChoseYes;

    protected override void Start()
    {
        base.Start();

        MessageManager.Instance.OnMessageRead.AddListener(DoMessageReadAction);

        if (_choiceQuestionTextKey != null)
        {
            ChoiceMessageManager.Instance.OnYesClick.AddListener(PlayMessageOnYes);
            ChoiceMessageManager.Instance.OnNoClick.AddListener(PlayMessageOnNo);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);

        _choiceGivenToPlayer = false;
        _playerChoseYes = false;
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
            MessageManager.Instance.ShowMessage(TextManager.GetText(_textKey), _messageType, _messageSpeed, gameObject);
        }
    }

    private void DoMessageReadAction(GameObject obj)
    {
        if (gameObject == obj)
        {
            if (!_choiceGivenToPlayer)
            {
                _choiceGivenToPlayer = true;
                ChoiceMessageManager.Instance.ShowMessage(TextManager.GetText(_choiceQuestionTextKey).ToString(), _messageSpeed, gameObject);
            }
            else if (_choiceGivenToPlayer && _playerChoseYes)
            {
                GivePlayerItem();
            }
        }
    }

    private void PlayMessageOnYes(GameObject obj)
    {
        if (_choiceYesTextKey != null && _choiceYesTextKey != "" && gameObject == obj)
        {
            _playerChoseYes = true;
            MessageManager.Instance.ShowMessage(TextManager.GetText(_choiceYesTextKey), _messageType, _messageSpeed, gameObject);
        }
    }

    private void PlayMessageOnNo(GameObject obj)
    {
        if (_choiceNoTextKey != null && _choiceNoTextKey != "" && gameObject == obj)
        {
            _playerChoseYes = false;
            MessageManager.Instance.ShowMessage(TextManager.GetText(_choiceNoTextKey), _messageType, _messageSpeed, gameObject);
        }
    }

    private void GivePlayerItem()
    {
        if (_rewardItem != null && !_itemGivenToPlayer)
        {
            _itemGivenToPlayer = true;
            GameManager.Instance.PlayerInventory.AddToInventory(_rewardItem.Item);
            StartCoroutine(RewardMessage());
        }
    }

    private IEnumerator RewardMessage()
    {
        MessageManager.Instance.ShowMessage("...", _messageType, _messageSpeed);

        yield return new WaitForSeconds(1f);

        string message = ((string)TextManager.GetText(_rewardItem.TextKey)).Replace("{item}", _rewardItem.Name.ToFormattedString());
        MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
    }
}
