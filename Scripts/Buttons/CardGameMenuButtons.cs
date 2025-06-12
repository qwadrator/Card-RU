using UnityEngine;

public class CardGameMenuButtons : MonoBehaviour
{
    public GameObject continueButton;

    private void Start()
    {
        if (PlayerPrefs.GetString("Save") == "Yes" && continueButton != null)
        {
            continueButton.SetActive(true);
        }
    }

    public void OnNewGameClicked() => GameManager.Instance.StartNewGame();
    public void OnContinueClicked() => GameManager.Instance.ContinueGame();
    public void OnSettingsClicked() => GameManager.Instance.OpenSettings();
    public void OnStatisticsClicked() => GameManager.Instance.OpenStatistics();
    public void OnExitClicked() => GameManager.Instance.QuitGame();
}