using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private GameObject _interactBubble;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Interactable"))
        {
            _interactBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _interactBubble.SetActive(false);
    }
}
