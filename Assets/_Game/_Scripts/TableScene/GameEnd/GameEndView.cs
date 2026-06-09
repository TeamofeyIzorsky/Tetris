using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

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
        _gameEndStatus.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "gameEnded");

        _gameModeTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "standart");

        _decription.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "standartDescription");


        _firstBestTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "bestScore");
        _firstBestValue.text = bestScore.ToString();

        _secondBestTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "bestTime");

        TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);

        // Получаем минуты и секунды в стандартном формате
        string minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        string milliseconds = timeSpan.ToString(@"fff");

        _secondBestValue.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";

        if (bestScore < score)
        {
            _firstTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "scoreNewBest");
        }
        else
        {
            _firstTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "scoreEnd");
        }

        _firstValue.text = score.ToString();


        if (bestTime < time)
        {
            _secondTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "timeNewBest");
        }
        else
        {
            _secondTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "timeEnd");
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
            _gameEndStatus.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "defeat");
        }
        else
        {
            _gameEndStatus.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "gameEnded");
        }

        _gameModeTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "blitz");
        _decription.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "blitzDescription");

        _firstBestTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "bestScore");
        _firstBestValue.text = bestScore.ToString();

        _secondBestTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "bestLinesCountEnd");
        _secondBestValue.text = bestLinesCount.ToString();


        if (!defeat && bestScore < score)
        {
            _firstTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "scoreNewBest");
        }
        else
        {
            _firstTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "scoreEnd");
        }

        _firstValue.text = score.ToString();


        if (!defeat && bestLinesCount < linesCount)
        {
            _secondTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "linesNewBest");
        }
        else
        {
            _secondTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "linesEnd");
        }

        _secondValue.text = linesCount.ToString();
    }
    
    private void Lines40End(bool defeat, float bestTime, float time)
    {
        _gameEndCanvas.enabled = true;

        if (defeat)
        {
            _gameEndStatus.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "defeat");
        }
        else
        {
            _gameEndStatus.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "gameEnded");
        }

        _gameModeTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "40Lines");
        _decription.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "40LinesDescription");

        _firstBestTitle.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "bestTime");

        TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);

        // Получаем минуты и секунды в стандартном формате
        string minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        string milliseconds = timeSpan.ToString(@"fff");

        _firstBestValue.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";


        _secondBestTitle.text = "———:";
        _secondBestValue.text = "———";

        if (!defeat && bestTime > time)
        {
            _firstTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "timeNewBest");
        }
        else
        {
            _firstTitlie.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "timeEnd");
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
