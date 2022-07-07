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

    private void Awake()
    {
        _timerButton.onClick.AddListener(ShowTimerWindow);
    }

    private void OnDestroy()
    {
        _timerButton.onClick.RemoveAllListeners();
    }

    public void SetData(TimerData timerData)
    {
        _timerData = timerData;
    }

    private void ShowTimerWindow()
    {
        EventManager.OnTimerButtonClicked?.Invoke(this, _timerData.Time);
    }
}
