using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public List<TimerData> Data;

    public SaveData(List<TimerData> data)
    {
        Data = data;
    }
}
