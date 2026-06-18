using System;
using UnityEngine;

public class GameEndManager : IEndGameManager
{
    //Завершает игру и проверяет рекорды
    private GameMode _gameMode;

    private IPlayerInput _playerInput;
    private IPauseController _pauseController;

    public GameEndManager(GameMode gameMode, IGameScore gameScore, IPlayerInput playerInput, IPauseController pauseController)
    {
        gameScore.OnDefeat += Defeat;
        gameScore.OnGameEnd += GameEnd;

        _gameMode = gameMode;

        _playerInput = playerInput;
        _pauseController = pauseController;
    }

    public event Action<int, int, float, float> OnStandartEnd;
    public event Action<bool, int, int, int, int> OnBlitzEnd;
    public event Action<bool, float, float> On40LinesEnd;

    //Если игрок проиграл (проиграть можно только в блице или 40 линий)
    private void Defeat(int score, int linesCount, float timer)
    {
        GameOver(true, score, linesCount, timer);
    }

    //Если игрок завершил игру
    private void GameEnd(int score, int linesCount, float timer)
    {
        G.DataManager.currentGameData.GamePlayed();

        GameOver(false, score, linesCount, timer);
    }

    //Конец игры
    private void GameOver(bool defeat, int score, int linesCount, float timer)
    {
        _pauseController.Pause(true);

        switch (_gameMode)
        {
            case GameMode.Standart:

                int oldBestScore = G.DataManager.currentGameData.BestStandartScore;
                float oldBestTime = G.DataManager.currentGameData.BestStandartTime;

                if (!defeat)
                {
                    G.DataManager.currentGameData.BestScore(score, GameMode.Standart);
                    G.DataManager.currentGameData.BestTime(timer, GameMode.Standart);
                }

                OnStandartEnd?.Invoke(oldBestScore, score, oldBestTime, timer);


                break;

            case GameMode.Blitz:
                oldBestScore = G.DataManager.currentGameData.BestBlirzScore;
                int oldLinesCount = G.DataManager.currentGameData.BestBlitzLinesCount;

                if (!defeat)
                {
                    G.DataManager.currentGameData.BestScore(score, GameMode.Blitz);
                    G.DataManager.currentGameData.BestLinesCount(linesCount, GameMode.Blitz);
                }

                OnBlitzEnd?.Invoke(defeat, oldBestScore, score, oldLinesCount, linesCount);

                break;

            case GameMode.Lines40:

                oldBestTime = G.DataManager.currentGameData.Best40LinesTime;

                if (!defeat)
                {
                    G.DataManager.currentGameData.BestTime(timer, GameMode.Lines40);
                }

                On40LinesEnd?.Invoke(defeat, oldBestTime, timer);
                break;
        }
    }

}
