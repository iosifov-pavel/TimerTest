using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimerData
{
    private float _timeValue;
    private float _currentValue;
    private int _id;

    public bool Started => _currentValue != -1;
    public float Time
    {
        get
        {
            if (_currentValue == -1)
            {
                return _timeValue;
            }
            return _currentValue;
        }
        set
        {
            _currentValue = value;
        }
    }
    public TimerData(int id, float time, float currentValue = -1)
    {
        _id = id;
        _timeValue = time;
        _currentValue = currentValue;
    }

    public void Reset()
    {
        _currentValue = -1;
    }
}
