using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    public Button languageButton;

    private int currentLanguageIndex = 0;

    void Start()
    {
        // Add listener for the button click
        languageButton.onClick.AddListener(SwitchLanguage);
    }

    void SwitchLanguage()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        currentLanguageIndex = (currentLanguageIndex + 1) % locales.Count;
        LocalizationSettings.SelectedLocale = locales[currentLanguageIndex];
    }
}
