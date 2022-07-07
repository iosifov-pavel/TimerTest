using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TimerButton _buttonPrefab;
    [SerializeField]
    private RectTransform _buttonsContainer;

    private List<TimerData> _timers;
    private List<float> _baseValues;
    private void Awake()
    {
        _timers = new List<TimerData>();
        _baseValues = new List<float> { 60f, 120f, 180f };
        SetPresets();
        CreateButtons();
    }

    private void SetPresets()
    {
        foreach( var baseValue in _baseValues )
        {
            var indexOfValue = _baseValues.IndexOf(baseValue);
            var baseTimer = new TimerData(indexOfValue, baseValue);
            _timers.Add(baseTimer);
        }
    }

    private void CreateButtons()
    {
        foreach (var timer in _timers)
        {
            var timerButton = Instantiate(_buttonPrefab, _buttonsContainer);
            timerButton.SetData(timer);
        }
    }
}
