using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Settings : MonoBehaviour
{
    //Класс настроек игры, пока реализована только смена языка

    [SerializeField] private TMP_Dropdown _languageDropdown;

    private IEnumerator Start()
    {
        var initOperation = LocalizationSettings.InitializationOperation;

        yield return initOperation;

        if (initOperation.Status != UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Не удалось инициализировать систему локализации.");
            yield break;
        }


        if (PlayerPrefs.HasKey("Language"))
        {
            LocalizationSettings.Instance.SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Language")]);
            _languageDropdown.value = PlayerPrefs.GetInt("Language");
        }
    }

    public void ChangeLanguage(int languageIndex)
    {
        LocalizationSettings.Instance.SetSelectedLocale(LocalizationSettings.AvailableLocales.Locales[languageIndex]);

        PlayerPrefs.SetInt("Language", languageIndex);
    }
}
