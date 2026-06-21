using System.Collections.Generic;

public class GameParameters : IGameParameters
{
    //Хранит параметры скорости игры и времени для действий управления
    public float TimeForDown { get; private set; }
    public float LockDelay { get; }

    public float TimeForFastDown { get; }

    public float TimeForStartHorizontalMove { get; }
    public float TimeForFastHorizontalMove { get; }

    private List<SpeedLevel> _speedLevels;

    private int _currentSpeedLevel;

    public GameParameters(GameConfigSO gameConfig, ITetrisField tetrisField)
    {
        tetrisField.OnDeleteLinesEnd += TryIncreaseSpeedLevel;

        TimeForDown = gameConfig.TimeForDown;

        LockDelay = gameConfig.LockDelay;
        TimeForFastDown = gameConfig.TimeForFastDown;
        TimeForStartHorizontalMove = gameConfig.TimeForStartHorizontalMove;
        TimeForFastHorizontalMove = gameConfig.TimeForFastHorizontalMove;

        _speedLevels = new List<SpeedLevel>(gameConfig.SpeedLevels);

        _currentSpeedLevel = 0;
    }

    public void TryIncreaseSpeedLevel(int deletedLines, int allDeletedLines)
    {
        SpeedLevel speedLevel = _speedLevels[_currentSpeedLevel];

        if (allDeletedLines >= speedLevel.LinesCount)
        {
            if (_currentSpeedLevel < _speedLevels.Count - 1)
            {
                _currentSpeedLevel++;

                TimeForDown = _speedLevels[_currentSpeedLevel].Speed;
            }
        }
    }
}
