using System;
using UnityEngine;

[Serializable]
public struct SpeedLevel
{
    //Скорость и сколько линий надо уничтожить для данной скорости

    [SerializeField] public int LinesCount;

    [SerializeField] public float Speed;
}
