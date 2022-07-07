using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerWindow : MonoBehaviour
{
    [SerializeField]
    private RectTransform _selfRect;
    [SerializeField]
    private TMP_Text _timerText;
    [SerializeField]
    private AnimationCurve _windowAnimation;
    [Header("Buttons")]
    [SerializeField]
    private Button _minusButton;
    [SerializeField]
    private Button _plusButton;
    [SerializeField]
    private Button _startButton;

    private float _timerTime;

    private void Awake()
    {
        EventManager.OnTimerButtonClicked += OnTimerButtonClicked;
        ShowWindow(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ShowWindowRoutine(true));
    }

    private void OnDestroy()
    {
        EventManager.OnTimerButtonClicked -= OnTimerButtonClicked;
    }

    private void OnTimerButtonClicked(object sender, float timerValue)
    {
        SetData(timerValue);
        ShowWindow(true);
    }

    private void SetData(float timerValue)
    {
        _timerTime = timerValue;
    }

    private void ShowWindow(bool state)
    {
        gameObject.SetActive(state);
    }

    private IEnumerator ShowWindowRoutine( bool isOpened)
    {
        yield return null;
    }
}
