using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private CardGameStateMachine _stateMachine;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _stateMachine = new CardGameStateMachine();
    }
    public void StartNewGame() => _stateMachine.StartNewGame();
    public void ContinueGame() => _stateMachine.ContinueGame();
    public void OpenSettings() => _stateMachine.OpenSettings();
    public void OpenStatistics() => _stateMachine.OpenStatistics();
    public void ExitToMainMenu() => _stateMachine.ExitToMainMenu();
    public void QuitGame() => _stateMachine.QuitGame();
    public void StartNewRun() => _stateMachine.StartNewRun();
}

