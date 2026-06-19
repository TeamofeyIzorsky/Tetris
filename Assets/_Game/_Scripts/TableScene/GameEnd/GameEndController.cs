using System;
using UnityEngine;

public class GameEndController : IGameEndController
{
    private IGameScore _gameScore;
    private IGameStateMachine _gameStateMachine;
    private IGameDataManager _gameDataManager;

    public GameEndController(IEndStrategy endStrategy, IGameScore gameScore, IGameManager gameManager, IGameStateMachine gameStateMachine, IGameDataManager gameDataManager)
    {
        _gameScore = gameScore;

        _gameStateMachine = gameStateMachine;

        _gameDataManager = gameDataManager;

        endStrategy.Subscribe(this, gameScore, gameManager);
    }

    public event Action<GameData, RoundData> OnGameEnded;

    public void GameDefeat()
    {
        _gameStateMachine.ChangeState(GameState.Ended);

        RoundData roundData = _gameScore.GetRoundData();

        roundData.isDefeat = true;

        GameData gameData = _gameDataManager.GetGameData();

        OnGameEnded?.Invoke(gameData, roundData);
    }

    public void GameEnd()
    {
        _gameStateMachine.ChangeState(GameState.Ended);

        RoundData roundData = _gameScore.GetRoundData();

        roundData.isDefeat = false;

        GameData gameData = _gameDataManager.GetGameData();

        _gameDataManager.RoundEnd(roundData);


        OnGameEnded?.Invoke(gameData, roundData);
    }

}
