using UnityEngine;
using UnityEngine.SceneManagement;

public class CardGameStateMachine
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("ChooseCharacter");
    }
    public void ContinueGame()
    {
        if (PlayerPrefs.GetString("Save") == "Yes")
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void OpenStatistics()
    {
        SceneManager.LoadScene("Statistic");
    }
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartNewRun()
    {
        SceneManager.LoadScene("Battle");
    }
}