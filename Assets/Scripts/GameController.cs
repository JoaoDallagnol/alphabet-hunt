using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject gameOver;
    public static GameController instance;
    void Start() {
        instance = this;
    }

    public void GameOver() {
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame(string sceneName) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
