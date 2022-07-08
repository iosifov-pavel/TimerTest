using UnityEngine;

[System.Serializable]
public class TimerData
{
    [SerializeField]
    private float _timeValue;
    [SerializeField]
    private float _currentValue;

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
    public TimerData(float time, float currentValue = -1)
    {
        _timeValue = time;
        _currentValue = currentValue;
    }

    public void Reset()
    {
        _currentValue = -1;
    }
}
