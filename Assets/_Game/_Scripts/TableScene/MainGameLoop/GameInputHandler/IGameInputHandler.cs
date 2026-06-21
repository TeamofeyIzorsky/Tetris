using System;
using UnityEngine;

public interface IGameInputHandler : IPauseUpdatable
{
    public event Action OnHardDrop;
    public event Action OnHold;
    public event Action OnRotate;
    public event Action OnFastDown;
    public event Action<Vector2Int> OnHorizonralMove;
}
