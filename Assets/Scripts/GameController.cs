using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class GameController : MonoBehaviour {

    public GameObject gameOver;
    public GameObject gameEnd;
    public static GameController instance;
    public TextMeshProUGUI alphabetText1;
    public TextMeshProUGUI alphabetText2;
    public Button difficultyButton;
    private string firstHalf = "ABCDEFGHIJKLM";
    private string secondHalf = "NOPQRSTUVWXYZ";
    private Color defaultColor = Color.gray;
    private Color highlightColor = Color.white;
    private HashSet<string> collectedLetters = new HashSet<string>();
    private bool isHard;
    private int totalScore;
    public Text scoreText;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    public LocalizedString difficultyLocalizedString;

    void Start() {
        instance = this;
        difficultyLocalizedString.StringChanged += OnDifficultyStringChanged;
        UpdateDifficultyUI();
        UpdateAlphabetUI("");
    }

    void OnEnable() {
        LocalizationSettings.SelectedLocaleChanged += OnLocaleChanged;
    }

    void OnDisable() {
        LocalizationSettings.SelectedLocaleChanged -= OnLocaleChanged;
        difficultyLocalizedString.StringChanged -= OnDifficultyStringChanged;
    }

    void OnLocaleChanged(Locale newLocale) {
        UpdateDifficultyUI();
    }

    void UpdateDifficultyUI() {
        if (PlayerPrefs.HasKey("isHard")) {
            isHard = PlayerPrefs.GetInt("isHard") == 1;
            string difficultyText = isHard ? "HARD" : "EASY";

            string currentLocale = LocalizationSettings.SelectedLocale.Identifier.Code;
            if (currentLocale == "pt-BR") {
                difficultyText = isHard ? "DIFICIL" : "FACIL";
            }

            // Atualiza os argumentos da string localizada
            difficultyLocalizedString.Arguments = new object[] { difficultyText };

            // Atualiza a string localizada
            difficultyLocalizedString.RefreshString();

            // Define a cor do botão com base na dificuldade
            Color newColor = isHard ? new Color(0.73f, 0, 0.04f) : new Color(0.05f, 0.69f, 0.24f);
            difficultyButton.image.color = newColor;
        }
    }

    void OnDifficultyStringChanged(string localizedText) {
        difficultyButton.GetComponentInChildren<TextMeshProUGUI>().text = localizedText;
    }

    public void GameOver() {
        gameOver.SetActive(true);
        Time.timeScale = 0;
        SoundFXManager.instance.PlaySoundFXClip(loseSound, transform, 1f);
    }

    public void RestartGame(string sceneName) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void UpdateAlphabetUI(string collectedLetter)
    {
        collectedLetters.Add(collectedLetter);
        alphabetText1.text = GetFormattedText(firstHalf, collectedLetter);
        alphabetText2.text = GetFormattedText(secondHalf, collectedLetter);
    }

    public void GameEnd() {
        gameEnd.SetActive(true);
        Time.timeScale = 0;
        SoundFXManager.instance.PlaySoundFXClip(winSound, transform, 1f);
    }

    private string GetFormattedText(string text, string collectedLetter)
    {
        string newText = "";
        
        foreach (char letter in text)
        {
             if (collectedLetters.Contains(letter.ToString()))
            {
                newText += $"<color=#{ColorUtility.ToHtmlStringRGB(highlightColor)}>{letter}</color> ";
            }
            else
            {
                newText += $"<color=#{ColorUtility.ToHtmlStringRGB(defaultColor)}>{letter}</color> ";
            }
        }

        return newText;
    }

    public void AddScore() {
        if (isHard) {
            totalScore += 2;
        } else {
            totalScore += 1;
        }
    }

    public void DecreaseScore() {
        if (isHard) {
            totalScore -= 2;
        } else {
            totalScore -= 1;  
        }
    }

    public void UpdateScore () {
        scoreText.text = totalScore.ToString();
    }

    public void ChangeDificulty() {
        isHard = !isHard;
        PlayerPrefs.SetInt("isHard", isHard ? 1 : 0);
        PlayerPrefs.Save();
        UpdateDifficultyUI();
    }
}
