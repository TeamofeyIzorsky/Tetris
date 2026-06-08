using System;
using TMPro;
using UnityEngine;

public class GameScore : PausableBehaviour, IGameScore
{
    private int _linesCount;

    private ComboType _comboType = ComboType.None;
    private int _comboCount = 0;
    private int _score;

    private float _timerTime;



    //Events
    public event Action<float, int, int> OnAfterUpdate;
    public event Action<ComboType, int> OnComboUpdate;

    public event Action<int, int, float> OnDefeat;
    public event Action<int, int, float> OnGameEnd;

    private void Awake()
    {
        G.GameScore = this;
    }


    private void Start()
    {
        G.TetrisField.OnDeleteLinesEnd += IncreaseScore;
        G.TetrisField.OnGameOver += Defeat;

        _score = 0;
        _linesCount = 0;

        switch (G.GameMode)
        {
            case GameMode.Standart:
                _timerTime = 0f;
                break;

            case GameMode.Casual:
                _timerTime = 0f;
                break;

            case GameMode.Lines40:
                _timerTime = 0f;
                break;

            case GameMode.Blitz:
                _timerTime = 120f;
                break;

            default:
                break;
        }
    }

    public override void PausableUpdate()
    {
        switch (G.GameMode)
        {
            case GameMode.Standart:
                IncreaseTime();

                break;

            case GameMode.Casual:
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

        OnAfterUpdate?.Invoke(_timerTime, _score, _linesCount);
    }

    private void IncreaseScore(int linesCount, int allDestroyedLines)
    {
        _linesCount = allDestroyedLines;

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

        Debug.Log(linesCount + " " +  score);

        _score += (int)MathF.Round(score * (1 + 0.25f * _comboCount));

        OnComboUpdate?.Invoke(_comboType, _comboCount);

        if(G.GameMode == GameMode.Lines40 && _linesCount >= 40)
        {
            GameEnd();
        }
    }


    // Update is called once per frame
    private void IncreaseTime()
    {
        _timerTime += Time.deltaTime;
    }

    private void DecreaseTime()
    {
        _timerTime -= Time.deltaTime;

        if (_timerTime <= 0)
        {
            GameEnd();
        }
    }

    private void Defeat()
    {
        if(G.GameMode == GameMode.Standart)
        {
            GameEnd();
            return;
        }

        OnDefeat?.Invoke(_score, _linesCount, _timerTime);
    }

    private void GameEnd()
    {
        OnGameEnd?.Invoke(_score, _linesCount, _timerTime);
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
