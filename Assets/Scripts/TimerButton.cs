using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerButton : MonoBehaviour
{
    [SerializeField]
    private Button _timerButton;
    [SerializeField]
    private TMP_Text _timerText;

    private TimerData _timerData;
    private float _counter;
    private WaitForSeconds _secondWait;

    private void Awake()
    {
        _timerButton.onClick.AddListener(ShowTimerWindow);
        _secondWait = new WaitForSeconds(1);
    }

    private void OnDestroy()
    {
        _timerButton.onClick.RemoveAllListeners();
    }

    public void SetData(TimerData timerData, bool withAnimation = false)
    {
        _timerData = timerData;
        SetTimerText();
        if(!withAnimation)
        {
            return;
        }
        StartCoroutine(ButtonAnimationRoutine());
    }

    private void SetTimerText()
    {
        _timerText.text = TimerController.GetFormattedTime(_timerData.Time);
    }

    private void ShowTimerWindow()
    {
        EventManager.OnTimerButtonClicked?.Invoke(this, _timerData.Time);
    }

    public void StartCountDown()
    {
        StartCoroutine(StartCountodwnRoutine());
    }

    private IEnumerator ButtonAnimationRoutine()
    {
        var timer = 0f;
        while(true)
        {
            timer += Time.deltaTime;
            if(timer > _timerData.Time)
            {
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator StartCountodwnRoutine()
    {
        while(true)
        {
            yield return _secondWait;
            _timerData.Time = Mathf.Max(0, _timerData.Time-1);
            SetTimerText();
            EventManager.OnTimerValueUpdate?.Invoke(this, _timerData.Time);
            if (_timerData.Time == 0)
            {
                _timerData.Reset();
                SetTimerText();
                yield break;
            }
        }
    }
}
