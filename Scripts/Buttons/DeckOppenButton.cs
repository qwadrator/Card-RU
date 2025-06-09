using UnityEngine;

public class DeckOppenButton : MonoBehaviour
{
    public GameObject Deck;
    public GameObject Battle;
    public void OpenCloseDeck()
    {
        Debug.Log("Button  DeckOppenButton Clicked");
        if (Deck.activeSelf)
        {
            Deck.SetActive(false);
            Battle.SetActive(true);
        }
        else
        {
            Deck.SetActive(true);
            Battle.SetActive(false);
        }
    }
}
