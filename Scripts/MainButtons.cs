using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class ConvasButtons : MonoBehaviour
{
    public GameObject Continue;
    private void Start()
    {
        if (PlayerPrefs.GetString("Save") == "Yes"  && gameObject.name == "Continue")
        {
            Continue.SetActive(true);                                   //настройки сохраняются при выходе из игры
        }
    }
    public void NewGame()
    {
        SceneManager.LoadScene("ChooseCharacter");
    }
    public void Settings(){
        SceneManager.LoadScene("Settings");
    }

    public void Statistic(){
        SceneManager.LoadScene("Statistic");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
