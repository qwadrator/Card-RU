using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameStart : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Battle");
    }
}
