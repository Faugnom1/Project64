using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] private Item _rewardItem;
    [SerializeField] private ItemName _name;

    [SerializeField] private UnityEvent _onRewardComplete;
    [SerializeField] private UnityEvent _onInteract;

    private bool _itemGivenToPlayer;
    private bool _choiceGivenToPlayer;
    private Item _newRewardItem;

    protected override void Start()
    {
        base.Start();

        MessageManager.Instance.OnMessageRead.AddListener(DoMessageReadAction);

        if (_rewardItem != null)
        {
            _newRewardItem = Instantiate(_rewardItem, new Vector3(0, 0, 0), Quaternion.identity);
            _newRewardItem.ItemName = _name;
        }

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
    }

    protected override void Update()
    {
        base.Update();

        if (IsPlayerInteracting() && !_messageShown)
        {
            _canInteract = false;

            // Play sound effect
            SoundEffectsManager.Instance.PlaySoundEffect(_onReadClip, transform.position, _onReadClipVolume);

            // Show message
            _messageShown = true;
            MessageManager.Instance.ShowMessage(TextManager.GetText(_textKey), _messageType, _messageSpeed, gameObject);

            _onInteract?.Invoke();
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
            else if (_choiceGivenToPlayer)
            {
                GivePlayerItem();
            }
        }
    }

    private void PlayMessageOnYes(GameObject obj)
    {
        if (_choiceYesTextKey != null && _choiceYesTextKey != "" && gameObject == obj)
        {
            MessageManager.Instance.ShowMessage(TextManager.GetText(_choiceYesTextKey), _messageType, _messageSpeed, gameObject);
            _onRewardComplete?.Invoke();
        }
    }

    private void PlayMessageOnNo(GameObject obj)
    {
        if (gameObject == obj)
        {
            GivePlayerItem();
        }
    }

    private void GivePlayerItem()
    {
        if (_rewardItem != null && !_itemGivenToPlayer)
        {
            _itemGivenToPlayer = true;
            GameManager.Instance.PlayerInventory.AddToInventory(_newRewardItem);
            StartCoroutine(RewardMessage());
        }
    }

    private IEnumerator RewardMessage()
    {
        MessageManager.Instance.ShowMessage("...", _messageType, _messageSpeed);

        yield return new WaitForSeconds(1f);

        string message = ((string)TextManager.GetText("reward_item")).Replace("{item}", _newRewardItem.ItemName.ToFormattedString());
        MessageManager.Instance.ShowMessage(message, _messageType, _messageSpeed);
    }
}
