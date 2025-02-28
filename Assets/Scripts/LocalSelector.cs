using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalSelector : MonoBehaviour
{
    private bool active = false;

    private void Start() {
        int Id = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(Id);
    }
    public void ChangeLocale(int localeId) {
        if (active) {
            return;
        }
        StartCoroutine(SetLocal(localeId));
    }
    IEnumerator SetLocal(int _LocalId) {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_LocalId];
        PlayerPrefs.SetInt("LocaleKey", _LocalId);
        active = false;
    }
}
