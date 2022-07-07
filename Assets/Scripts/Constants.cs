using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    public const float WindowAnimationTime = .35f;
    public const float AttractionAnimationTime = 1f;
    public static readonly List<float> StartValues = new List<float> { 5f, 10f, 15f };

    public const int SecondsInMinute = 60;
    public const int MinutesInHours = 60;

    public static readonly Color RunningColor = new Color(200/255f, 160/255f, 120/255f);
    public static readonly Color AttractionColor = new Color(200 / 255f, 160 / 255f, 20 / 255f);
    

    public const string SavePath = "/savedata.dat";
}
