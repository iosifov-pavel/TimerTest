using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddTimerWindow : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _input;
    [SerializeField]
    private TMP_Text _timeAmountText;
    [SerializeField]
    private Button _okButton;
    [SerializeField]
    private Button _closeButton;

    private float _timerValue;

    private void Awake()
    {
        EventManager.OnTimeIsUp += OnTimeIsUp;
        _input.onValueChanged.AddListener(CheckInput);
        _okButton.onClick.AddListener(OkButtonAction);
        _closeButton.onClick.AddListener(() => ShowWindow(false));
        _okButton.interactable = false;
        ShowWindow(false);
    }

    private void OnTimeIsUp(object sender, EventArgs e)
    {
        _input.text = default;
        ShowWindow(false);
    }

    public void ShowWindow(bool state)
    {
        gameObject.SetActive(state);
        EventManager.OnUpdateWindowState(this, state);
    }

    private void CheckInput(string text)
    {
        _timeAmountText.gameObject.SetActive(text.Length > 0);
        if (text.Length > Constants.MaxTimerLength)
        {
            text = text.Substring(0, Constants.MaxTimerLength);
        }
        if (int.TryParse(text, out var seconds))
        {
            var hours = 0;
            var minutes = 0;
            if (seconds >= Constants.ValueEnoughForHours)
            {
                hours = seconds / Constants.ValueEnoughForHours;
                seconds = seconds - hours * Constants.ValueEnoughForHours;
            }
            if (seconds >= Constants.ValueEnoughForMinutes)
            {
                minutes = seconds / Constants.ValueEnoughForMinutes;
                seconds = seconds - minutes * Constants.ValueEnoughForMinutes;
            }
            var result = seconds + minutes * Constants.SecondsInMinute + hours * Constants.SecondsInMinute * Constants.MinutesInHours;
            _timeAmountText.text = $"{hours:0#}:{minutes:0#}:{seconds:0#}";
            _timerValue = result;
            _okButton.interactable = true;
            return;
        }
        _okButton.interactable = false;
    }

    private void OkButtonAction()
    {
        _input.text = default;
        EventManager.OnAddNewTimer?.Invoke(this, _timerValue);
        ShowWindow(false);
    }
}
