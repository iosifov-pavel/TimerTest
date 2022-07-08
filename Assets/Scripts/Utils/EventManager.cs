using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager 
{
    public static EventHandler<float> OnTimerButtonClicked;
    public static EventHandler<float> OnTimerValueUpdate;
    public static EventHandler<float> OnAddNewTimer;
}
