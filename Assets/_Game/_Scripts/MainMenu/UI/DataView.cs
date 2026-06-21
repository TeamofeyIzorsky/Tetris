using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class DataView : MonoBehaviour
{
    //Класс, который отображает всю статистику и инфомацию по рекордам в Главном меню


    private IGameDataManager _gameDataManager;

    public void Construct(IGameDataManager gameDataManager)
    {
        _gameDataManager  = gameDataManager;

        LocalizationSettings.SelectedLocaleChanged += ShowStatistic;

        ShowStatistic(null);
        ShowGameModsStatistic();
    }


    [SerializeField] private TMP_Text _standartScoreText;
    [SerializeField] private TMP_Text _standartTimeText;

    [SerializeField] private TMP_Text _40linesTimeText;

    [SerializeField] private TMP_Text _blitzScoreText;
    [SerializeField] private TMP_Text _blitzLinesCount;

    [SerializeField] private TMP_Text _playsCount;
    [SerializeField] private TMP_Text _allTime;

    private void ShowGameModsStatistic()
    {
        _standartScoreText.text = _gameDataManager.GetGameData().BestStandartScore.ToString();

        TimeSpan timeSpan = TimeSpan.FromSeconds(_gameDataManager.GetGameData().BestStandartTime);

        // Получаем минуты и секунды в стандартном формате
        string minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        string milliseconds = timeSpan.ToString(@"fff");

        _standartTimeText.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";


        timeSpan = TimeSpan.FromSeconds(_gameDataManager.GetGameData().Best40LinesTime);

        // Получаем минуты и секунды в стандартном формате
        minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        milliseconds = timeSpan.ToString(@"fff");

        _40linesTimeText.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";

        _blitzScoreText.text = _gameDataManager.GetGameData().BestBlirzScore.ToString();
        _blitzLinesCount.text = _gameDataManager.GetGameData().BestBlitzLinesCount.ToString();
    }

    private async void ShowStatistic(Locale locale)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(_gameDataManager.GetGameData().allTime);

        // Получаем минуты и секунды в стандартном формате
        string time = timeSpan.ToString(@"hh\:mm\:ss");

        // Получаем 2 цифры миллисекунд
        _allTime.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "totalTime").Task + " " + time;

        _playsCount.text = await LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Localization", "totalTime").Task + " " + _gameDataManager.GetGameData().gamesPlayed.ToString();

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
