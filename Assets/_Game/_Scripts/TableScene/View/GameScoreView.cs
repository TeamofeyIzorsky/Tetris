using System;
using TMPro;
using UnityEngine;

public class GameScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _timeValueText;

    [SerializeField] private TMP_Text _linesText;
    [SerializeField] private TMP_Text _linesValueText;

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreValueText;

    [SerializeField] private TMP_Text _comboText;
    [SerializeField] private TMP_Text _comboValueText;

    private void Start()
    {
        G.GameScore.OnAfterUpdate += UpdateView;
        G.GameScore.OnComboUpdate += UpdateComboView;

        _comboText.gameObject.SetActive(false);
        _comboValueText.gameObject.SetActive(false);
    }
    
    private void UpdateView(float time, int score, int lines)
    {
        UpdateTimer(time);

        UpdateScore(score);

        UpdateLines(lines);
    }

    private void UpdateTimer(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);

        // Получаем минуты и секунды в стандартном формате
        string minutesAndSeconds = timeSpan.ToString(@"mm\:ss");

        // Получаем 2 цифры миллисекунд
        string milliseconds = timeSpan.ToString(@"fff");

        _timeValueText.text = $"{minutesAndSeconds}<size=70%>.{milliseconds}";
    }

    private void UpdateScore(int score)
    {
        _scoreValueText.text = score.ToString();
    }

    private void UpdateLines(int lines)
    {
        _linesValueText.text = lines.ToString();
    }

    private void UpdateComboView(ComboType comboType, int comboCount)
    {
        string comboText = "";

        if(comboCount >= 2 && comboType != ComboType.None)
        {
            _comboText.gameObject.SetActive(true);
            _comboValueText.gameObject.SetActive(true);

            switch (comboType)
            {
                case ComboType.Line1:
                    comboText = "1 Line";
                    break;
                case ComboType.Line2:
                    comboText = "2 Lines";
                    break;
                case ComboType.Line3:
                    comboText = "3 Lines";
                    break;
                case ComboType.Tetris:
                    comboText = "Tetris";
                    break;
                default:
                    comboText = "ERROR";
                    break;
            }

            _comboValueText.text = comboText + " x"+ comboCount.ToString();
            return;
        }

        _comboText.gameObject.SetActive(false);
        _comboValueText.gameObject.SetActive(false);
    }
}
