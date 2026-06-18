using System;
using UnityEngine;

public class PauseController : IPauseController
{
    //Управлет состоянием паузы
    private IUpdateManager _updateManager;
    private IPlayerInput _playerInput;

    public event Action<bool> OnChangePauseStatus;

    public PauseController(IPlayerInput playerInput, IUpdateManager updateManager)
    {
        _updateManager = updateManager;
        _playerInput = playerInput;
    }

    public void Tick(float deltaTime)
    {
        if (_playerInput.Pause)
        {
            Pause(!_updateManager.IsPaused);
        }
    }

    //Останавливает игру
    public void Pause(bool pauseStatus)
    {
        _updateManager.Pause(pauseStatus);

        OnChangePauseStatus?.Invoke(pauseStatus);
    }
}
