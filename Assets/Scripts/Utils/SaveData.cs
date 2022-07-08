using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public List<TimerData> Timers;

    public SaveData(List<TimerData> timers)
    {
        Timers = timers;
    }
}
