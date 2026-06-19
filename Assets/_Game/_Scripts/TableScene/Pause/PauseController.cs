public class PauseController : IPauseController
{
    //Управлет состоянием паузы
    private IPlayerInput _playerInput;
    private IGameStateMachine _gameStateMachine;

    public PauseController(IPlayerInput playerInput, IGameStateMachine gameStateMachine)
    {
        _playerInput = playerInput;

        _gameStateMachine = gameStateMachine;
    }

    public void Tick(float deltaTime)
    {
        if (_playerInput.Pause)
        {
            switch(_gameStateMachine.CurrentState)
            {
                case GameState.Paused:
                    _gameStateMachine.ChangeState(GameState.Gameplay);
                    break;

                case GameState.Gameplay:
                    _gameStateMachine.ChangeState(GameState.Paused);
                    break;

                default:
                    return;
            }
        }
    }

    public void Pause(bool isPaused)
    {
        switch (_gameStateMachine.CurrentState)
        {
            case GameState.Paused:
                if(!isPaused)
                    _gameStateMachine.ChangeState(GameState.Gameplay);
                break;

            case GameState.Gameplay:
                if (isPaused)
                    _gameStateMachine.ChangeState(GameState.Paused);
                break;

            default:
                return;
        }
    }
}
