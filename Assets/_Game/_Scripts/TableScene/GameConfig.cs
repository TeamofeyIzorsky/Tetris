using System.Collections.Generic;
using UnityEngine;

public struct GameConfig
{
    //Хранит в себе данные скоростей и тему для данного раунда

    public ThemeSO Theme { get; private set; }

    public float TimeForDown { get; private set;}
    public float LockDelay { get; private set; }

    public float TimeForFastDown { get; private set; }

    public float TimeForStartHorizontalMove { get; private set; }
    public float TimeForFastHorizontalMove { get; private set; }


    public void Init()
    {
        Theme = G.GResources.ThemeSOs[0];

        TimeForDown = G.GResources.TimeForDown;
        LockDelay = G.GResources.LockDelay;
        TimeForFastDown = G.GResources.TimeForFastDown;
        TimeForStartHorizontalMove = G.GResources.TimeForStartHorizontalMove;
        TimeForFastHorizontalMove = G.GResources.TimeForFastHorizontalMove;
    }

    public void NewDownSpeed(float timeForDown)
    {
        TimeForDown = timeForDown;
    }
}
