using UnityEngine;

public class GameDataManager : MonoBehaviour, IGameDataManager
{
    //Класс, который заносит рекорды, количество сыграных игр и общее время в игре в GameData, а потом вызывает сохранение или загрузку

    private ISaveManager _saveManager;
    private GameData _gameData;


    public GameDataManager Construct(ISaveManager saveManager)
    {
        _saveManager = saveManager;

        _gameData = _saveManager.DeserializeSave();

        return this;
    }

    private void Update()
    {
        _gameData.allTime += Time.deltaTime;
    }

    public void RoundEnd(RoundData roundData)
    {
        _gameData.gamesPlayed++;

        if (roundData.isDefeat)
        {
            return;
        }

        switch (roundData.GameMode)
        {
            case GameMode.Standard:
                StandardRound(roundData);
                break;

            case GameMode.Blitz:
                BlitzRound(roundData);
                break;

            case GameMode.Lines40:
                Lines40Round(roundData);
                break;

            default:
                return;
        }
    }

    private void StandardRound(RoundData roundData)
    {
        if(roundData.Score > _gameData.BestStandartScore)
        {
            _gameData.BestStandartScore = roundData.Score;
        }

        if (roundData.Time > _gameData.BestStandartTime)
        {
            _gameData.BestStandartTime = roundData.Time;
        }
    }

    private void BlitzRound(RoundData roundData)
    {
        if (roundData.LinesDestroyed > _gameData.BestBlitzLinesCount)
        {
            _gameData.BestBlitzLinesCount = roundData.LinesDestroyed;
        }

        if (roundData.Score > _gameData.BestBlirzScore)
        {
            _gameData.BestBlirzScore = roundData.Score;
        }
    }

    private void Lines40Round(RoundData roundData)
    {
        if (roundData.Time < _gameData.Best40LinesTime)
        {
            _gameData.Best40LinesTime = roundData.Time;
        }
    }

    public GameData GetGameData()
    {
        return _gameData;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            _saveManager.SerializeSave(_gameData);
        }
    }

    private void OnApplicationQuit()
    {
        _saveManager.SerializeSave(_gameData);
    }
}
