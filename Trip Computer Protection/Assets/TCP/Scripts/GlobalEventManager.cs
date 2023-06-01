using System;

public static class GlobalEventManager
{
    public static Action TacticalPhaseOn;
    public static Action TacticalPhaseOff;

    public static void SendTacticalPhaseOn()
    {
        TacticalPhaseOn.Invoke();
    }

    public static void SendTacticalPhaseOff()
    {
        TacticalPhaseOff.Invoke();
    }
}
