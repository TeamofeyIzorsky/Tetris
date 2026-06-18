using System.Collections.Generic;
using UnityEngine;

public struct GameConfig
{
    //Хранит в себе данные скоростей и тему для данного раунда

    public GameConfig(GameResources resources)
    {
        Theme = resources.ThemeSOs[0];

        TimeForDown = resources.TimeForDown;
        LockDelay = resources.LockDelay;
        TimeForFastDown = resources.TimeForFastDown;
        TimeForStartHorizontalMove = resources.TimeForStartHorizontalMove;
        TimeForFastHorizontalMove = resources.TimeForFastHorizontalMove;
    }

    public ThemeSO Theme { get; private set; }

    public float TimeForDown { get; private set;}
    public float LockDelay { get; private set; }

    public float TimeForFastDown { get; private set; }

    public float TimeForStartHorizontalMove { get; private set; }
    public float TimeForFastHorizontalMove { get; private set; }


    public void NewDownSpeed(float timeForDown)
    {
        TimeForDown = timeForDown;
    }
}
