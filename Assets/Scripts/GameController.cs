using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject gameOver;
    public static GameController instance;
    public TextMeshProUGUI alphabetText1;
    public TextMeshProUGUI alphabetText2;
    private string firstHalf = "ABCDEFGHIJKLM";
    private string secondHalf = "NOPQRSTUVWXYZ";
    private Color defaultColor = Color.gray;
    private Color highlightColor = Color.white;
    private HashSet<string> collectedLetters = new HashSet<string>();
    void Start() {
        instance = this;
        UpdateAlphabetUI("");
    }

    public void GameOver() {
        gameOver.SetActive(true);
        Time.timeScale = 0;
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
}
