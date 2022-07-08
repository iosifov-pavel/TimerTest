using System;

public static class EventManager
{
    public static EventHandler<float> OnTimerButtonClicked;
    public static EventHandler<float> OnTimerValueUpdate;
    public static EventHandler<float> OnAddNewTimer;
    public static EventHandler OnTimeIsUp;
    public static EventHandler<bool> OnUpdateWindowState;
}
