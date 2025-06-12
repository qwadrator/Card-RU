using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameStart : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.StartNewRun();
    }
}
