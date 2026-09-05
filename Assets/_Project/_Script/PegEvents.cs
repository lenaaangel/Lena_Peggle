using System;

public static class PegEvents
{
    public static event Action<PegHitData> Hit;

    public static void RaiseHit(PegHitData data)
    {
        Hit?.Invoke(data);
    }
}
