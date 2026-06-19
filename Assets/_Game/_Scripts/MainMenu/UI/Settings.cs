using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _languageDropdown;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Language")];

            _languageDropdown.value = PlayerPrefs.GetInt("Language");
        }
    }

    public void ChangeLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
        PlayerPrefs.SetInt("Language", languageIndex);
    }
}
