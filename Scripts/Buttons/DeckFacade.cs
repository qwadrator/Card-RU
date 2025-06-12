using UnityEngine;
using UnityEngine.SceneManagement;
public class DeckFacade : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private DeckManager _deckManager;
    private static DeckFacade _instance;
    public static DeckFacade Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _deckManager = GetComponent<DeckManager>();
        if (_deckManager == null)
        {
            _deckManager = gameObject.AddComponent<DeckManager>();
        }
    }
    public void HeroDrowClicked() => _deckManager.HeroDeckDrow();
    public void HeroBurnClicked() => _deckManager.HeroDeckBurn();
    public void HeroDiscardClicked() => _deckManager.HeroDeckDiscard();
    public void EnemyDrowClicked() =>  _deckManager.EnemyDeckDrow();
    public void EnemyDiscardClicked() =>  _deckManager.EnemyDeckDiscard();
}