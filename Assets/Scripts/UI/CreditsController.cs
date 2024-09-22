using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _beforeScrollDuration;
    [SerializeField] private float targetYPosition;

    private RectTransform _rectTransform;
    private float _beforeScrollTimer;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _beforeScrollTimer = _beforeScrollDuration;
    }

    private void Update()
    {
        _beforeScrollTimer -= Time.deltaTime;

        if (_beforeScrollTimer < 0)
        {
            _rectTransform.anchoredPosition += new Vector2(0, _scrollSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(_rectTransform.anchoredPosition.y - targetYPosition) <= 0.01f)
        {
            SceneManager.LoadScene("StartScreen");
        }
    }
}
