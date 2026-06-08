using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DataView : MonoBehaviour
{
    [SerializeField] private TMP_Text _standartScoreText;
    [SerializeField] private TMP_Text _standartTimeText;

    [SerializeField] private TMP_Text _40linesTimeText;

    [SerializeField] private TMP_Text _blitzScoreText;
    [SerializeField] private TMP_Text _blitzLinesCount;

    [SerializeField] private TMP_Text _playsCount;
    [SerializeField] private TMP_Text _allTime;

    private void Start()
    {
        LocalizationSettings.SelectedLocaleChanged += ShowStatistic;

        ShowStatistic(null);
        ShowGameModsStatistic();
    }
    private void ShowGameModsStatistic()
    {
        _standartScoreText.text = G.DataManager.currentGameData.BestStandartScore.ToString();

        TimeSpan timeSpan = TimeSpan.FromSeconds(G.DataManager.currentGameData.BestStandartTime);

        // Получаем минуты и секунды в стандартном формате
        string minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        string milliseconds = timeSpan.ToString(@"fff");

        _standartTimeText.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";


        timeSpan = TimeSpan.FromSeconds(G.DataManager.currentGameData.Best40LinesTime);

        // Получаем минуты и секунды в стандартном формате
        minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        milliseconds = timeSpan.ToString(@"fff");

        _40linesTimeText.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";

        _blitzScoreText.text = G.DataManager.currentGameData.BestBlirzScore.ToString();
        _blitzLinesCount.text = G.DataManager.currentGameData.BestBlitzLinesCount.ToString();
    }

    private void ShowStatistic(Locale locale)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(G.DataManager.currentGameData.allTime);

        // Получаем минуты и секунды в стандартном формате
        string time = timeSpan.ToString(@"hh\:mm\:ss");

        // Получаем 2 цифры миллисекунд

        _allTime.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "roundsFinished") + " " + time;

        _playsCount.text = LocalizationSettings.StringDatabase.GetLocalizedString("Localization", "totalTime") + " " + G.DataManager.currentGameData.gamesPlayed.ToString();

    }

    private void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += ShowStatistic;
    }
    private void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= ShowStatistic;
    }
    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= ShowStatistic;
    }
}
