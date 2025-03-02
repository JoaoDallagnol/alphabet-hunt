using UnityEngine;

public class AlphabetGuideCollector : MonoBehaviour
{
    [SerializeField] private GameObject alphabetGuideUI;
    void Start()
    {
        managerAlphabetGuide();
    }

    private void managerAlphabetGuide() {
        if (PlayerPrefs.HasKey("isHard")) {
            bool isHard = PlayerPrefs.GetInt("isHard") == 1;
            if (isHard) {
                alphabetGuideUI.SetActive(false);
            } else {
                alphabetGuideUI.SetActive(true);
            }
        }
    }
}
