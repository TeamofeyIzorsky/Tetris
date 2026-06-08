using System;
using UnityEngine;

public interface IPauseManager
{
    public event Action<bool> OnChangePauseStatus;

    public bool GetIsPauseStatus();
    public void Pause(bool isPause);
}
