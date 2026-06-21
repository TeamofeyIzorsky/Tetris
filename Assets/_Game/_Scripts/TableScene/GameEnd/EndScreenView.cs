using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class EndScreenView : MonoBehaviour
{
    //Отображает экран конца игры

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

    public void Construct(IGameEndController endGameManager)
    {
        _gameEndCanvas.enabled = false;

        endGameManager.OnGameEnded += GameEnded;
    }


    private void GameEnded(GameData previosRecords, RoundData roundData)
    {
        if(roundData.GameMode == GameMode.Standard)
        {
            StandartEnd(previosRecords.BestStandartScore, roundData.Score, previosRecords.BestStandartTime, roundData.Time);
        }
        else if(roundData.GameMode == GameMode.Blitz)
        {
            BlitzEnd(roundData.isDefeat, previosRecords.BestBlirzScore, roundData.Score, previosRecords.BestBlitzLinesCount, roundData.LinesDestroyed);
        }
        else if(roundData.GameMode == GameMode.Lines40)
        {
            Lines40End(roundData.isDefeat, previosRecords.Best40LinesTime, roundData.Time);
        }
    }

    private async void StandartEnd(int bestScore, int score, float bestTime, float time)
    {
        Cursor.visible = true;

        _gameEndCanvas.enabled = true;
        _gameEndStatus.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "gameEnded").Task;

        _gameModeTitle.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "standart").Task;

        _decription.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "standartDescription").Task;


        _firstBestTitle.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "bestScore").Task;
        _firstBestValue.text = bestScore.ToString();

        _secondBestTitle.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "bestTime").Task;

        TimeSpan timeSpan = TimeSpan.FromSeconds(bestTime);

        // Получаем минуты и секунды в стандартном формате
        string minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        string milliseconds = timeSpan.ToString(@"fff");

        _secondBestValue.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";

        if (bestScore < score)
        {
            _firstTitlie.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "scoreNewBest").Task;
        }
        else
        {
            _firstTitlie.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "scoreEnd").Task;
        }

        _firstValue.text = score.ToString();


        if (bestTime < time)
        {
            _secondTitlie.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "timeNewBest").Task;
        }
        else
        {
            _secondTitlie.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "timeEnd").Task;
        }

        timeSpan = TimeSpan.FromSeconds(time);

        // Получаем минуты и секунды в стандартном формате
        minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        milliseconds = timeSpan.ToString(@"fff");

        _secondValue.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";
    }

    private async void BlitzEnd(bool defeat, int bestScore, int score, int bestLinesCount, int linesCount)
    {
        Cursor.visible = true;

        _gameEndCanvas.enabled = true;

        if (defeat)
        {
            _gameEndStatus.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "defeat").Task;
        }
        else
        {
            _gameEndStatus.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "gameEnded").Task;
        }

        _gameModeTitle.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "blitz").Task;
        _decription.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "blitzDescription").Task;


        _firstBestTitle.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "bestScore").Task;
        _firstBestValue.text = bestScore.ToString();

        _secondBestTitle.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "bestLinesCountEnd").Task;
        _secondBestValue.text = bestLinesCount.ToString();


        if (!defeat && bestScore < score)
        {
            _firstTitlie.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "scoreNewBest").Task;
        }
        else
        {
            _firstTitlie.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "scoreEnd").Task;
        }

        _firstValue.text = score.ToString();



        if (!defeat && bestLinesCount < linesCount)
        {
            _secondTitlie.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "linesNewBest").Task;
        }
        else
        {
            _secondTitlie.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "linesEnd").Task;
        }

        _secondValue.text = linesCount.ToString();
    }
    
    private async void Lines40End(bool defeat, float bestTime, float time)
    {
        Cursor.visible = true;

        _gameEndCanvas.enabled = true;

        if (defeat)
        {
            _gameEndStatus.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "defeat").Task;
        }
        else
        {
            _gameEndStatus.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "gameEnded").Task;
        }

        _gameModeTitle.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "40Lines").Task;
        _decription.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "40LinesDescription").Task;

        _firstBestTitle.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "bestTime").Task;

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
