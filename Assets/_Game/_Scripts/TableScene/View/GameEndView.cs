using System;
using TMPro;
using UnityEngine;

public class GameEndView : MonoBehaviour
{
    [SerializeField] private Canvas _gameEndCanvas;

    [SerializeField] private TMP_Text _gameEndStatus;

    [Header("GameMode")]
    [SerializeField] private TMP_Text _gameModeTitle;
    [SerializeField] private TMP_Text _decription;

    [Header("First")]
    [SerializeField] private TMP_Text _firstTitlie;
    [SerializeField] private TMP_Text _firstValue;
    [SerializeField] private TMP_Text _firstBestTitle;
    [SerializeField] private TMP_Text _firstBestValue;

    [Header("Second")]
    [SerializeField] private TMP_Text _secondTitlie;
    [SerializeField] private TMP_Text _secondValue;
    [SerializeField] private TMP_Text _secondBestTitle;
    [SerializeField] private TMP_Text _secondBestValue;


    private void Start()
    {
        _gameEndCanvas.enabled = false;

        G.GameEndManager.OnStandartEnd += StandartEnd;
        G.GameEndManager.OnBlitzEnd += BlitzEnd;
        G.GameEndManager.On40LinesEnd += Lines40End;
    }

    private void StandartEnd(int bestScore, int score, float bestTime, float time)
    {
        _gameEndCanvas.enabled = true;
        _gameEndStatus.text = "Game Ended";

        _gameModeTitle.text = "Standart";
        _decription.text = "Collect as big a score as possible and survive as long as you can.";

        _firstBestTitle.text = "Best Score:";
        _firstBestValue.text = bestScore.ToString();

        _secondBestTitle.text = "Best Time:";

        TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);

        // Получаем минуты и секунды в стандартном формате
        string minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        string milliseconds = timeSpan.ToString(@"fff");

        _secondBestValue.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";

        if (bestScore < score)
        {
            _firstTitlie.text = "Score [New Best!]:";
        }
        else
        {
            _firstTitlie.text = "Score:";
        }

        _firstValue.text = score.ToString();


        if (bestTime < time)
        {
            _secondTitlie.text = "Time [New Best!]:";
        }
        else
        {
            _secondTitlie.text = "Time:";
        }

        timeSpan = TimeSpan.FromSeconds(time);

        // Получаем минуты и секунды в стандартном формате
        minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        milliseconds = timeSpan.ToString(@"fff");

        _secondValue.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";
    }

    private void BlitzEnd(bool defeat, int bestScore, int score, int bestLinesCount, int linesCount)
    {
        _gameEndCanvas.enabled = true;

        if (defeat)
        {
            _gameEndStatus.text = "Defeat!";
        }
        else
        {
            _gameEndStatus.text = "Game Ended";
        }

        _gameModeTitle.text = "Blitz";
        _decription.text = "Break as many lines as possible in 2 minutes.";

        _firstBestTitle.text = "Best Score:";
        _firstBestValue.text = bestScore.ToString();

        _secondBestTitle.text = "Best Lines Count:";
        _secondBestValue.text = bestLinesCount.ToString();


        if (bestScore < score)
        {
            _firstTitlie.text = "Score [New Best!]:";
        }
        else
        {
            _firstTitlie.text = "Score:";
        }

        _firstValue.text = score.ToString();


        if (bestLinesCount < linesCount)
        {
            _secondTitlie.text = "Lines Count [New Best!]:";
        }
        else
        {
            _secondTitlie.text = "Lines Count:";
        }

        _secondValue.text = linesCount.ToString();
    }
    
    private void Lines40End(bool defeat, float bestTime, float time)
    {
        _gameEndCanvas.enabled = true;

        if (defeat)
        {
            _gameEndStatus.text = "Defeat!";
        }
        else
        {
            _gameEndStatus.text = "Game Ended";
        }

        _gameModeTitle.text = "40-Lines";
        _decription.text = "Break 40 lines in as little time as possible.";

        _firstBestTitle.text = "Best Time:";

        _secondBestTitle.text = "Best Time:";

        TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);

        // Получаем минуты и секунды в стандартном формате
        string minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        string milliseconds = timeSpan.ToString(@"fff");

        _firstBestValue.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";


        _secondBestTitle.text = "———:";
        _secondBestValue.text = "———";

        if (bestTime < time)
        {
            _firstTitlie.text = "Time [New Best!]:";
        }
        else
        {
            _firstTitlie.text = "Time:";
        }

        timeSpan = TimeSpan.FromSeconds(time);

        // Получаем минуты и секунды в стандартном формате
        minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        milliseconds = timeSpan.ToString(@"fff");

        _firstValue.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";


        _secondTitlie.text = "———";
        _secondValue.text = "———"; ;
    }
}
