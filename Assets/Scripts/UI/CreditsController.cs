using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject _outroPanel;
    [SerializeField] private GameObject _creditsPanel;

    [Header("Credits")]
    [SerializeField] private float _creditsScrollSpeed;
    [SerializeField] private float _creditsEndPositionY;

    [Header("Outro")]
    [SerializeField] private float _outroTextDuration;
    [SerializeField] private float _outroTextMessageSpeed;

    private TextMeshProUGUI _outroTextMesh;
    private RectTransform _creditsRect;
    private float _outroTextTimer;

    private void Start()
    {
        _outroTextTimer = _outroTextDuration;
        _outroTextMesh = _outroPanel.GetComponentInChildren<TextMeshProUGUI>();
        _creditsRect = _creditsPanel.GetComponent<RectTransform>();

        StartCoroutine(TypeOutroText());
    }

    private void Update()
    {
        _outroTextTimer -= Time.deltaTime;

        if (_outroTextTimer < 0)
        {
            _outroPanel.SetActive(false);

            ScrollCredits();

            if (IsAtEndTargetPosition())
            {
                SceneManager.LoadScene("StartScreen");
            }
        }
    }

    private void ScrollCredits()
    {
        _creditsRect.anchoredPosition += new Vector2(0, _creditsScrollSpeed * Time.deltaTime);
    }

    private bool IsAtEndTargetPosition()
    {
        return Mathf.Abs(_creditsRect.anchoredPosition.y - _creditsEndPositionY) <= 0.01f;
    }

    private IEnumerator TypeOutroText()
    {
        _outroTextMesh.ForceMeshUpdate();

        for (int i = 0; i < _outroTextMesh.textInfo.characterCount; i++)
        {
            _outroTextMesh.maxVisibleCharacters = i + 1;
            yield return new WaitForSeconds(_outroTextMessageSpeed);
        }
    }
}
