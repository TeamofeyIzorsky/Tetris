using UnityEngine;

public interface IGameParameters
{
    public float TimeForDown { get; }
    public float LockDelay { get; }

    public float TimeForFastDown { get; }

    public float TimeForStartHorizontalMove { get; }
    public float TimeForFastHorizontalMove { get; }

    public void TryIncreaseSpeedLevel(int deletedLines, int allDeletedLines);
}
