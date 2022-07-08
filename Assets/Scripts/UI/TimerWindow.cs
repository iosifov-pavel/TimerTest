using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerWindow : MonoBehaviour
{
    [SerializeField]
    private RectTransform _selfRect;
    [SerializeField]
    private TMP_Text _timerText;
    [SerializeField]
    private AnimationCurve _windowAnimation;
    [SerializeField]
    private AnimationCurve _closeAnimation;
    [Header("Buttons")]
    [SerializeField]
    private LongPressButton _minusButton;
    [SerializeField]
    private LongPressButton _plusButton;
    [SerializeField]
    private Button _startButton;
    [SerializeField]
    private Button _closeButton;

    private float _timerTime;
    private TimerButton _currentButton;

    private void Awake()
    {
        EventManager.OnTimeIsUp += OnTimeIsUp;
        EventManager.OnTimerButtonClicked += OnTimerButtonClicked;
        EventManager.OnTimerValueUpdate += OnTimerValueUpdate;
        _startButton.onClick.AddListener(StartTimer);
        _minusButton.SetCallback(ChangeTimer, false);
        _plusButton.SetCallback(ChangeTimer, true);
        _closeButton.onClick.AddListener(() => ShowWindow(false));
        ShowWindow(false);
    }


    private void OnEnable()
    {
        StartCoroutine(ShowWindowRoutine(true));
    }
    private void OnTimeIsUp(object sender, EventArgs e)
    {
        _currentButton = default;
        ShowWindow(false);
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
        _startButton.interactable = !_currentButton.Started;
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
        EventManager.OnUpdateWindowState(this, state);
    }

    private IEnumerator ShowWindowRoutine(bool isOpened, Action afterAnimationCallback = null)
    {
        var timer = 0f;
        var targetanimation = isOpened ? _windowAnimation : _closeAnimation;
        var endScale = isOpened ? Vector3.one : Vector3.zero;
        while (true)
        {
            timer += Time.deltaTime;
            var animationValue = targetanimation.Evaluate(timer / Constants.WindowAnimationTime);
            _selfRect.localScale = new Vector3(animationValue, animationValue, animationValue);
            if (timer > Constants.WindowAnimationTime)
            {
                _selfRect.localScale = endScale;
                afterAnimationCallback?.Invoke();
                yield break;
            }
            yield return null;
        }
    }

    private void StartTimer()
    {
        _currentButton.StartCountDown();
        _currentButton = null;
        StartCoroutine(ShowWindowRoutine(false, () => ShowWindow(false)));
    }

    private void ChangeTimer(float valueChange)
    {
        var newValue = Mathf.Max(0, _timerTime + valueChange);
        SetData(newValue);
        _currentButton?.UpdateTime(_timerTime);
    }
}
