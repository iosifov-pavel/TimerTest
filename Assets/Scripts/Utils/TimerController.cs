using System;
using System.Collections.Generic;
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
    [SerializeField]
    private CanvasGroup _buttonsGroup;

    private TimerWindow _window;
    private AddTimerWindow _addTimerWindow;
    private Button _addTimerButton;
    private List<TimerData> _timers;
    private void Awake()
    {
        EventManager.OnAddNewTimer += OnAddNewTimer;
        EventManager.OnUpdateWindowState += OnUpdateWindowState;
        _timers = new List<TimerData>();
        LoadData();
        SetPresets();
        SetWindow();
        SetAddTimerButton();
        CreateButtons();
    }

    private void OnUpdateWindowState(object sender, bool windowState)
    {
        _buttonsGroup.alpha = windowState ? 0 : 1;
    }

    private void OnAddNewTimer(object sender, float newTimerValue)
    {
        var newTimerIndex = _timers.Count;
        var newTimer = new TimerData(newTimerValue);
        _timers.Add(newTimer);
        var newButton = Instantiate(_buttonPrefab, _buttonsContainer);
        newButton.SetData(newTimer);
    }
    private void LoadData()
    {
        var savedata = SaveManager.LoadData();
        if (savedata == null)
        {
            return;
        }
        if (savedata.Timers?.Count == 0)
        {
            return;
        }
        _timers = savedata.Timers;
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
            var baseTimer = new TimerData(baseValue);
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
        // использовал именно это событие, так как оно чуток более надёжно, чем OnApplicationQuit
        // а сохранять данные каждую секунду, как изменились таймеры считаю не самым лучшим вариантом
        // Как альтернатива можно было бы сохранять данные каждые n секунд, но решил оставить в таком виде
        if (!focus)
        {
            var saveData = new SaveData(_timers);
            SaveManager.SaveData(saveData);
        }
    }
}
