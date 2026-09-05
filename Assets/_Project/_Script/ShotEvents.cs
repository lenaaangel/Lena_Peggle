using System;

public static class ShotEvents
{
    public static event Action<bool> ShotEnded;

    public static void RaiseShotEnded(bool freeBall)
    {
        ShotEnded?.Invoke(freeBall);
    }
}
