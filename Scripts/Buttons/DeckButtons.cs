using UnityEngine;

public class DeckButtons : MonoBehaviour
{
    public void OnHeroDeckDrowClicked() => DeckFacade.Instance.HeroDrowClicked();
    public void OnHeroDeckBurnClicked() => DeckFacade.Instance.HeroBurnClicked();
    public void OnHeroDeckDiscardClicked() => DeckFacade.Instance.HeroDiscardClicked();
    public void OnEnemyDeckDrowClicked() => DeckFacade.Instance.EnemyDrowClicked();
    public void OnEnemyDiscardClicked() => DeckFacade.Instance.EnemyDiscardClicked();

}