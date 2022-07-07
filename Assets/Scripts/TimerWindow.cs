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
    private TimerButton _currentButton;

    private void Awake()
    {
        EventManager.OnTimerButtonClicked += OnTimerButtonClicked;
        EventManager.OnTimerValueUpdate += OnTimerValueUpdate;
        _startButton.onClick.AddListener(StartTimer);
        ShowWindow(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ShowWindowRoutine(true));
    }

    private void OnDestroy()
    {
        EventManager.OnTimerValueUpdate -= OnTimerValueUpdate;
        EventManager.OnTimerButtonClicked -= OnTimerButtonClicked;
        _startButton.onClick.RemoveAllListeners();
    }

    private void OnTimerValueUpdate(object sender, float newValue)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        if (_currentButton == null)
        {
            return;
        }
        var button = sender as TimerButton;
        if (button != _currentButton)
        {
            return;
        }
        SetData(newValue);
    }

    private void OnTimerButtonClicked(object sender, float timerValue)
    {
        _currentButton = sender as TimerButton;
        SetData(timerValue);
        ShowWindow(true);
    }

    private void SetData(float timerValue)
    {
        _timerTime = timerValue;
        _timerText.text = TimerController.GetFormattedTime(timerValue);
    }

    private void ShowWindow(bool state)
    {
        gameObject.SetActive(state);
    }

    private IEnumerator ShowWindowRoutine(bool isOpened)
    {
        var timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            var animationValue = _windowAnimation.Evaluate(timer / Constants.WindowAnimationTime);
            _selfRect.localScale = new Vector3(animationValue, animationValue, animationValue);
            if (timer > Constants.WindowAnimationTime)
            {
                _selfRect.localScale = Vector3.one;
                yield break;
            }
            yield return null;
        }
    }

    private void StartTimer()
    {
        _currentButton.StartCountDown();
        _currentButton = null;
        ShowWindow(false);
    }
}
