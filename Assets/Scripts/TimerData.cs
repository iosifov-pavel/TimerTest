using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimerData : MonoBehaviour
{
    private float _timeValue;
    private float _currentValue;
    private int _id;

    public bool AlreadyStarted => _currentValue != -1;
    public float Time => _timeValue;

    public TimerData(int id, float time, float currentValue = -1)
    {
        _id = id;
        _timeValue = time;
        _currentValue = currentValue;
    }
}
