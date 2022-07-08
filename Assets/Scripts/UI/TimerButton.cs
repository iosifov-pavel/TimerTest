using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerButton : MonoBehaviour
{
    [SerializeField]
    private Image _buttonBack;
    [SerializeField]
    private Button _timerButton;
    [SerializeField]
    private TMP_Text _timerText;
    [SerializeField]
    private AnimationCurve _attractionCurve;

    private TimerData _timerData;
    private bool _inAttractionState;

    public bool Started => _timerData != null && _timerData.Started;

    private void Awake()
    {
        _timerButton.onClick.AddListener(ButtonAction);
    }

    private void OnDestroy()
    {
        _timerButton.onClick.RemoveAllListeners();
    }

    public void SetData(TimerData timerData, bool withAnimation = false)
    {
        _timerData = timerData;
        SetTimerText();
        if (!withAnimation)
        {
            return;
        }
        StartCoroutine(ButtonAnimationRoutine());
        if (_timerData.Started)
        {
            StartCountDown();
        }
    }

    private void SetTimerText()
    {
        _timerText.text = TimerController.GetFormattedTime(_timerData.Time);
    }

    private void ButtonAction()
    {
        if (_inAttractionState)
        {
            _inAttractionState = false;
            return;
        }
        EventManager.OnTimerButtonClicked?.Invoke(this, _timerData.Time);
    }
    public void UpdateTime(float newTime)
    {
        _timerData.Time = newTime;
        SetTimerText();
    }

    public void StartCountDown()
    {
        StartCoroutine(StartCountodwnRoutine());
    }

    private IEnumerator ButtonAnimationRoutine()
    {
        var timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > _timerData.Time)
            {
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator StartCountodwnRoutine()
    {
        _buttonBack.color = Constants.RunningColor;
        var timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            var changeInSeconds = (int)timer;
            if(changeInSeconds != 0)
            {
                _timerData.Time = Mathf.Max(0, _timerData.Time - changeInSeconds);
                timer -= changeInSeconds;
                SetTimerText();
                EventManager.OnTimerValueUpdate?.Invoke(this, _timerData.Time);
            }
            if (_timerData.Time == 0)
            {
                _timerData.Reset();
                SetTimerText();
                break;
            }
            yield return null;
        }
        _inAttractionState = true;
        StartCoroutine(AttractionRoutine());
    }

    private IEnumerator AttractionRoutine()
    {
        var timer = 0f;
        _buttonBack.color = Constants.AttractionColor;
        while (_inAttractionState)
        {
            timer += Time.deltaTime;
            var animationValue = _attractionCurve.Evaluate(timer);
            transform.localScale = new Vector3(animationValue, animationValue, animationValue);
            if (timer >= Constants.AttractionAnimationTime)
            {
                timer = 0f;
            }
            yield return null;
        }
        _buttonBack.color = Color.white;
        transform.localScale = Vector3.one;
    }
}
