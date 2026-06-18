using System.Collections.Generic;
using UnityEngine;

public interface IGameConfig
{
    public float TimeForDown {  get; }
    public float LockDelay { get; }

    public float TimeForFastDown { get; }

    public float TimeForStartHorizontalMove { get; }
    public float TimeForFastHorizontalMove { get; }

    public List<SpeedLevel> SpeedLevels { get; }
}
