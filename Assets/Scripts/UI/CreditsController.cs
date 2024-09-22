using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private float _beforeScrollDuration;

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
    }
}
