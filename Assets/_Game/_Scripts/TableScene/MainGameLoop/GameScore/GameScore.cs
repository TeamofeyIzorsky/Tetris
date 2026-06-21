using System;
using TMPro;
using UnityEngine;

public class GameScore : IGameScore
{
    //Класс, который считает счет и стастистику во время игры

    private RoundData _roundData;

    public GameScore(ITicketManager ticketManager, ITetrisField tetrisField)
    {
        tetrisField.OnDeleteLinesEnd += IncreaseScore;

        _roundData = new RoundData();

        _roundData.GameMode = ticketManager.GetGameTicket().GameMode;
        _roundData.GameMode = ticketManager.GetGameTicket().GameMode;

        switch (_roundData.GameMode)
        {
            case GameMode.Standard:
                _roundData.Time = 0f;
                break;
            case GameMode.Lines40:
                _roundData.Time = 0f;
                break;

            case GameMode.Blitz:
                _roundData.Time = 120f;
                break;

            default:
                break;
        }
    }

    private ComboType _comboType = ComboType.None;
    private int _comboCount = 0;

    public bool IsPausable { get => true; set => throw new NotImplementedException(); }

    //Events
    public event Action<RoundData> OnAfterUpdate;
    public event Action<ComboType, int> OnComboUpdate;

    public event Action OnTimeOver;
    public event Action On40Lines;

    public void Tick(float deltaTime)
    {
        switch (_roundData.GameMode)
        {
            case GameMode.Standard:
                IncreaseTime();

                break;

            case GameMode.Lines40:
                IncreaseTime();

                break;

            case GameMode.Blitz:
                DecreaseTime();
                break;

            default:

                break;
        }

        OnAfterUpdate?.Invoke(_roundData);
    }

    private void IncreaseScore(int linesCount, int allDestroyedLines)
    {
        _roundData.LinesDestroyed = allDestroyedLines;

        ComboType currentComboType = ComboType.None;

        int score = 0;

        switch (linesCount)
        {
            case 1:
                currentComboType = ComboType.Line1;
                score = 100;
                break;

            case 2:
                currentComboType = ComboType.Line2;
                score = 300;
                break;

            case 3:
                currentComboType= ComboType.Line3;
                score = 500;
                break;

            case 4:
                currentComboType = ComboType.Tetris;
                score = 800;
                break;
        }

        if(currentComboType == _comboType)
        {
            _comboCount++;
        }
        else
        {
            _comboType = currentComboType;
            _comboCount = 1;
        }

        //Debug.Log(linesCount + " " +  score);

        _roundData.Score += (int)MathF.Round(score * (1 + 0.25f * _comboCount));

        OnComboUpdate?.Invoke(_comboType, _comboCount);

        if(_roundData.LinesDestroyed >= 40)
        {
            On40Lines?.Invoke();
        }
    }

    private void IncreaseTime()
    {
        _roundData.Time += Time.deltaTime;
    }

    private void DecreaseTime()
    {
        _roundData.Time -= Time.deltaTime;

        if (_roundData.Time <= 0)
        {
            OnTimeOver?.Invoke();
        }
    }

    public RoundData GetRoundData()
    {
        return _roundData;
    }
}

public enum ComboType
{
    None,
    Line1,
    Line2,
    Line3,
    Tetris
}
