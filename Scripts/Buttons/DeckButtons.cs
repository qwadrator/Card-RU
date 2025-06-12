using UnityEngine;

public class DeckButtons : MonoBehaviour
{
    public void OnDeckHeroDrowClicked() => DeckFacade.Instance.HeroDrowClicked();
    public void OnDeckHeroBurnClicked() => DeckFacade.Instance.HeroBurnClicked();
    public void OnDeckHeroDiscardClicked() => DeckFacade.Instance.HeroDiscardClicked();

}