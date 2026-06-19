using System;

public enum GameState
{
    NotStarted,
    Gameplay,
    Paused,
    Ended,
}

public class GameStateMachine : IGameStateMachine
{
    private GameState _currentState;

    public GameState CurrentState => _currentState;

    public GameStateMachine()
    {
        _currentState = GameState.NotStarted;
    }

    public event Action<GameState> OnStateChanged;

    public void ChangeState(GameState state)
    {

        if (state == _currentState) return;

        _currentState = state;

        OnStateChanged?.Invoke(_currentState);

    }
}
