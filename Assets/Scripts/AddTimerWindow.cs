using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        _input.onValueChanged.AddListener( CheckInput );
        _okButton.onClick.AddListener(OkButtonAction);
        _closeButton.onClick.AddListener(() => ShowWindow(false));
        ShowWindow(false);
    }

    public void ShowWindow(bool state)
    {
        gameObject.SetActive(state);
    }

    private void CheckInput(string text)
    {
        _timeAmountText.gameObject.SetActive(text.Length > 0);
        if( text.Length > Constants.MaxTimerLength)
        {
            text = text.Substring(0, Constants.MaxTimerLength);
        }
        if (float.TryParse(text, out var seconds))
        {
            var hours = 0;
            var minutes = 0;
            if(seconds >= 10000)
            {
                hours = (int) seconds / 10000;
                seconds = seconds - hours * 10000;
            }
            if(seconds >= 100)
            {
                minutes = (int) seconds / 100;
                seconds = seconds - minutes * 100;
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
        EventManager.OnAddNewTimer?.Invoke(this, _timerValue);
        ShowWindow(false);
    }
}
