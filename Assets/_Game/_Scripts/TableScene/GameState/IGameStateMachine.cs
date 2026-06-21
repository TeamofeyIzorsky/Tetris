using System;
using UnityEngine;

public interface IGameStateMachine
{
    public GameState CurrentState { get; }

    public event Action<GameState> OnStateChanged;

    public void ChangeState(GameState state);
}
