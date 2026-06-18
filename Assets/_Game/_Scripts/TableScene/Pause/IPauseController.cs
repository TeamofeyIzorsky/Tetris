using System;
using UnityEngine;

public interface IPauseController : IUpdatable
{
    public event Action<bool> OnChangePauseStatus;
    //public bool GetIsPauseStatus();
    public void Pause(bool isPause);
}
