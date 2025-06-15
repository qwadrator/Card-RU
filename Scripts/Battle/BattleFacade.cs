using UnityEngine;

public class BattleFacade : MonoBehaviour
{
    private static BattleFacade _instance;
    public static BattleFacade Instance => _instance;

    [SerializeField] private BattleControl _battleControl;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    public void EndTurn() => _battleControl.EndTurn();
}