using UnityEngine;

public readonly struct PegHitData
{
    public readonly PegType Type;
    public readonly float BaseScore;

    public PegHitData(PegType type, int baseScore)
    {
        Type = type;
        BaseScore = baseScore;
    }
}
