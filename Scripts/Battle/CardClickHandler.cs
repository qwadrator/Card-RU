using Cards;
using UnityEngine;
using UnityEngine.UI;

public class CardClickHandler : MonoBehaviour
{
    private Button button;
    private AbstractCard card;
    private AbstractGameCharacter Char; 
    private BattleControl battleControl;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Card prefab has no Button component!");
            return;
        }

        button.onClick.AddListener(OnCardClicked);
        battleControl = FindAnyObjectByType<BattleControl>();
        if (battleControl == null)
        {
            Debug.LogError("BattleControl not found in scene!");
        }
    }

    public void SetCard(AbstractCard cardData, AbstractGameCharacter ch)
    {
        card = cardData;
        Char = ch;
    }

    public void EnableInteraction(bool enable)
    {
        if (button != null)
            button.interactable = enable;
    }

    private void OnCardClicked()
    {
        if (card != null && Char != null && battleControl != null)
        {
            Debug.Log($"Карта {card.NAME} использована!");
            card.Use(SelectedGameCharacter.Hero, Enemies.Enemy);
            Char.DECKDISCARD.CARDS.Add(card);
            Char.HAND.CARDS.Remove(card);
            Destroy(gameObject);
            EnableInteraction(false);
            battleControl.DisplayHeroHand();
        }
    }
}