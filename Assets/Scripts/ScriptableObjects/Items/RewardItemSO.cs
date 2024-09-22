using UnityEngine;

[CreateAssetMenu(fileName = "RewardItemSO", menuName = "ScriptableObjects/RewardItem")]
public class RewardItemSO : ScriptableObject
{
    [Header("Reward Properties")]
    public Item Item;
    public ItemName Name;
    public string TextKey;

    private void OnEnable()
    {
        if (Item != null)
        {
            Item.ItemName = Name;
        }
    }
}
