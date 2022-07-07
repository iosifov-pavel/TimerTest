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
    private Button _okButton;

    private float _timerValue;

    private void Awake()
    {
        _input.onValueChanged.AddListener(CheckInput);
        _okButton.onClick.AddListener(OkButtonAction);
        ShowWindow(false);
    }

    public void ShowWindow(bool state)
    {
        gameObject.SetActive(state);
    }

    private void CheckInput(string value)
    {
        _okButton.interactable = false;
        if (float.TryParse(value, out _timerValue))
        {
            _okButton.interactable = true;
        }
    }

    private void OkButtonAction()
    {
        EventManager.OnAddNewTimer?.Invoke(this, _timerValue);
        ShowWindow(false);
    }
}
