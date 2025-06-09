using System;
using System.Collections.Generic;
using Cards;

using Scripts.Effects;
using UnityEngine;
using UnityEngine.UI;

public class BattleControl : MonoBehaviour
{
    public GameObject Hand;
    public Text Hp;
    public Image HeroSprite;
    public AbstractDeck TempDeck;
    public GameObject cardPrefab;
    public float cardSpacing = 100f;
    void Start()
    {
        HeroCheck();
        BattleStartTrigger();
        TurnHero();
    }
    public void HeroCheck()
    {
        if (SelectedGameCharacter.Hero != null)
        {
            Hp.text = SelectedGameCharacter.Hero.MAXHP.ToString();
            HeroSprite.sprite = SelectedGameCharacter.HeroSprite;
            SelectedGameCharacter.Hero.DECKDRAW = SelectedGameCharacter.Hero.DECK;
        }
        else
        {
            SelectedGameCharacter.Hero = new Hero1();
            Hp.text = SelectedGameCharacter.Hero.MAXHP.ToString();
            SelectedGameCharacter.Hero.DECKDRAW = SelectedGameCharacter.Hero.DECK;
            Debug.Log("Hero Created");
            //Debug.LogError("Hero not created!");
        }
    }
    public static void BattleStartTrigger()
    {
        if (EventManager.OnBattleStart == null || EventManager.OnBattleStart.Count == 0)
            return;
        foreach (Action action in EventManager.OnBattleStart)
        {
            action?.Invoke();
        }
        SelectedGameCharacter.Hero.DECKDRAW = SelectedGameCharacter.Hero.DECK;
    }
    public static void TurnStartTrigger()
    {
        if (EventManager.OnTurnStart == null || EventManager.OnTurnStart.Count == 0)
            return;
        foreach (Action action in EventManager.OnTurnStart)
        {
            action?.Invoke();
        }
    }
    public void TurnHero()
    {
        TurnStartTrigger();
        Debug.Log(SelectedGameCharacter.Hero.HANDDRAW);
        SelectedGameCharacter.Hero.Draw(SelectedGameCharacter.Hero.HANDDRAW);
        TempDeck = SelectedGameCharacter.Hero.HAND;
        DisplayHandCards(TempDeck);
    }
    private void DisplayHandCards(AbstractDeck deck)
    {
        foreach (Transform child in Hand.transform)
        {
            Destroy(child.gameObject);
        }

        if (deck == null || deck.CARDS == null || deck.CARDS.Count == 0)
        {
            Debug.Log("Нет карт для отображения в руке");
            return;
        }

        const float maxContainerWidth = 1200f;
        const float cardWidth = 200f;
        int cardCount = deck.CARDS.Count;

        float availableSpace = maxContainerWidth - (cardCount * cardWidth);
        float spacing = 0f;
        if (availableSpace > 0 && cardCount > 1)
        {
            spacing = availableSpace / (cardCount - 1);
        }
        else if (cardCount > 1)
        {
            spacing = (maxContainerWidth - cardWidth) / (cardCount - 1) - cardWidth;
        }
        
        float totalWidth = (cardCount - 1) * (cardWidth + spacing);
        float startX = -totalWidth / 1.7f;

        for (int i = 0; i < cardCount; i++)
        {
            GameObject cardInstance = Instantiate(cardPrefab, Hand.transform);
            float cardPositionX = startX + i * (cardWidth + spacing) + cardWidth / 2;
            cardInstance.transform.localPosition = new Vector3(cardPositionX, 0, 0);

            SetupCardUI(cardInstance.transform, deck.CARDS[i]);
        }
    }

    private void SetupCardUI(Transform cardTransform, AbstractCards card)
    {
        Text description = cardTransform.Find("Description")?.GetComponent<Text>();
        Image cardImage = cardTransform.Find("CardPicture")?.GetComponent<Image>();

        if (description == null || cardImage == null)
        {
            Debug.LogError("Не найдены компоненты в префабе карты!");
            return;
        }

        description.text = card.RAWDESCRIPTION;
        Sprite loadedSprite = Resources.Load<Sprite>(card.IMG);
        
        if (loadedSprite != null)
        {
            cardImage.sprite = loadedSprite;
        }
        else
        {
            Debug.LogError($"Не удалось загрузить спрайт: {card.IMG}");
            cardImage.color = Color.red;
        }
    }

    public void EnemyTurn()
    {

    }
}
