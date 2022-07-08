using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private TimerButton _buttonPrefab;
    [SerializeField]
    private TimerWindow _windowPrefab;
    [SerializeField]
    private Button _addTimerButtonPrefab;
    [SerializeField]
    private AddTimerWindow _addTimerWindowPrefab;
    [SerializeField]
    private RectTransform _buttonsContainer;
    [SerializeField]
    private RectTransform _timerWindowPosition;
    [SerializeField]
    private RectTransform _addTimerButtonContainer;
    [SerializeField]
    private RectTransform _addTimerWindowContainer;

    private TimerWindow _window;
    private AddTimerWindow _addTimerWindow;
    private Button _addTimerButton;
    private List<TimerData> _timers;
    private void Awake()
    {
        EventManager.OnAddNewTimer += OnAddNewTimer;
        _timers = new List<TimerData>();
        LoadData();
        SetPresets();
        SetWindow();
        SetAddTimerButton();
        CreateButtons();
    }

    private void OnDestroy()
    {
        EventManager.OnAddNewTimer -= OnAddNewTimer;
    }

    private void OnAddNewTimer(object sender, float newTimerValue)
    {
        var newTimerIndex = _timers.Count;
        var newTimer = new TimerData(newTimerIndex, newTimerValue);
        _timers.Add(newTimer);
        var newButton = Instantiate(_buttonPrefab, _buttonsContainer);
        newButton.SetData(newTimer);
    }
    private void LoadData()
    {
        var savedata = SaveManager.LoadData();
        if(savedata == null)
        {
            return;
        }
        if(savedata.Data?.Count == 0)
        {
            return;
        }
        _timers = savedata.Data;
    }
    private void SetPresets()
    {
        if (_timers.Count > 0)
        {
            return;
        }
        foreach (var baseValue in Constants.StartValues)
        {
            var indexOfValue = Constants.StartValues.IndexOf(baseValue);
            var baseTimer = new TimerData(indexOfValue, baseValue);
            _timers.Add(baseTimer);
        }
    }
    private void SetWindow()
    {
        if (_window == null)
        {
            _window = Instantiate(_windowPrefab, _timerWindowPosition);
        }
    }
    private void SetAddTimerButton()
    {
        if (_addTimerButton == null)
        {
            _addTimerButton = Instantiate(_addTimerButtonPrefab, _addTimerButtonContainer);
            _addTimerButton.onClick.AddListener(AddTimer);
        }
    }
    private void CreateButtons()
    {
        foreach (var timer in _timers)
        {
            var timerButton = Instantiate(_buttonPrefab, _buttonsContainer);
            timerButton.SetData(timer, withAnimation: true);
        }
    }
    private void AddTimer()
    {
        if (_addTimerWindow == null)
        {
            _addTimerWindow = Instantiate(_addTimerWindowPrefab, _addTimerWindowContainer);
        }
        _addTimerWindow.ShowWindow(true);
    }

    public static string GetFormattedTime(float seconds)
    {
        var minutes = (int)seconds / Constants.SecondsInMinute;
        var hours = minutes / Constants.MinutesInHours;
        var resulrString = String.Format("{0:0#}:{1:0#}:{2:0#}", hours, minutes - Constants.MinutesInHours * hours, seconds - minutes * Constants.SecondsInMinute);
        return resulrString;
    }

    private void OnApplicationFocus(bool focus)
    {
        if( !focus)
        {
            var saveData = new SaveData(_timers);
            SaveManager.SaveData(saveData);
        }
    }
}
