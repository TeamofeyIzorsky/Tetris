using System;
using UnityEngine;

public class PauseManager : MonoBehaviour, IPauseManager
{
    //Управлет состоянием паузы

    private bool _paused;

    public event Action<bool> OnChangePauseStatus;

    public bool GetIsPauseStatus()
    {
        return _paused;
    }

    private void Awake()
    {
        G.PauseManager = this;
    }

    private void Update()
    {
        if (G.PlayerInput.Pause)
        {
            Pause(!_paused);
        }
    }

    //Останавливает игру
    public void Pause(bool pauseStatus)
    {
        _paused = pauseStatus;

        OnChangePauseStatus?.Invoke(pauseStatus);
    }
}
