using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _fill;

    public void UpdateHPBar(PlayerHealthEvent hpEvent)
    {
        _fill.fillAmount = hpEvent.CurrentHP / hpEvent.MaxHP;
    }
}
