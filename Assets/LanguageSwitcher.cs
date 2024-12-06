using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class LanguageSwitcher : MonoBehaviour
{
    public Button languageButton;

    private static int currentLanguageIndex = 0;

    void Awake()
    {
        if(languageButton == null)
        {
            Debug.LogWarning("no Button");
        }
        else
        {
            languageButton.onClick.AddListener(SwitchLanguage);
        } 

        var locales = LocalizationSettings.AvailableLocales.Locales;
        currentLanguageIndex = (currentLanguageIndex + 0) % locales.Count;
        LocalizationSettings.SelectedLocale = locales[currentLanguageIndex];
    }

    void SwitchLanguage()
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        currentLanguageIndex = (currentLanguageIndex + 1) % locales.Count;
        LocalizationSettings.SelectedLocale = locales[currentLanguageIndex];
    }
}
