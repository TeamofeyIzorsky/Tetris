using System;
using UnityEngine;

public class GameEndManager : IEndGameManager
{
    //Завершает игру и проверяет рекорды
    private GameMode _gameMode;
    private IPauseController _pauseController;

    private IGameDataManager _gameDataManager;

    public GameEndManager(ITicketManager ticketManager, IGameScore gameScore, IPauseController pauseController, IGameDataManager gameDataManager)
    {
        gameScore.OnDefeat += Defeat;
        gameScore.OnGameEnd += GameEnd;

        _gameMode = ticketManager.GetGameTicket().GameMode;

        _pauseController = pauseController;

        _gameDataManager = gameDataManager;
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
        GameOver(false, score, linesCount, timer);
    }

    //Конец игры
    private void GameOver(bool defeat, int score, int linesCount, float timer)
    {
        _pauseController.Pause(true);

        switch (_gameMode)
        {
            case GameMode.Standard:

                int oldBestScore = _gameDataManager.GetGameData().BestStandartScore;
                float oldBestTime = _gameDataManager.GetGameData().BestStandartTime;

                if (!defeat)
                {
                    //_gameDataManager.RoundEnd();
                }

                OnStandartEnd?.Invoke(oldBestScore, score, oldBestTime, timer);


                break;

            case GameMode.Blitz:
                oldBestScore = _gameDataManager.GetGameData().BestBlirzScore;
                int oldLinesCount = _gameDataManager.GetGameData().BestBlitzLinesCount;

                if (!defeat)
                {
                    //_gameDataManager.RoundEnd();
                }

                OnBlitzEnd?.Invoke(defeat, oldBestScore, score, oldLinesCount, linesCount);

                break;

            case GameMode.Lines40:

                oldBestTime = _gameDataManager.GetGameData().Best40LinesTime;

                if (!defeat)
                {
                    //_gameDataManager.RoundEnd();
                }

                On40LinesEnd?.Invoke(defeat, oldBestTime, timer);
                break;
        }
    }

}
