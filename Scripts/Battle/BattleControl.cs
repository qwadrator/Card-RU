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
        CharacterCheck();
        BattleStartTrigger();
        TurnHero();
    }
    public void CharacterCheck()
    {
        CheckHero();
        CreateEnemy();
        
    }
    public void CheckHero()
    {
        if (SelectedGameCharacter.Hero != null)
        {
            Hp.text = SelectedGameCharacter.Hero.MAXHP.ToString();
            HeroSprite.sprite = SelectedGameCharacter.HeroSprite;

            SelectedGameCharacter.Hero.DECKDRAW.CARDS.Clear();
           SelectedGameCharacter.Hero.DECKDRAW.CARDS.AddRange(SelectedGameCharacter.Hero.DECK.CARDS);
        }
        else
        {
            SelectedGameCharacter.Hero = new Hero1();
            Debug.Log("Hero Created");

            Hp.text = SelectedGameCharacter.Hero.MAXHP.ToString();
            HeroSprite.sprite = SelectedGameCharacter.HeroSprite;

            SelectedGameCharacter.Hero.DECKDRAW.CARDS.Clear();
            SelectedGameCharacter.Hero.DECKDRAW.CARDS.AddRange(SelectedGameCharacter.Hero.DECK.CARDS); 
        }
        
        //Debug.LogError("Hero not created!");
    }
    public void CreateEnemy()
    {
        
    }
    public static void BattleStartTrigger()
    {
        if (EventManager.OnBattleStart == null || EventManager.OnBattleStart.Count == 0)
            return;
        foreach (Action action in EventManager.OnBattleStart)
        {
            action?.Invoke();
        }
        SelectedGameCharacter.Hero.DECKDRAW.CARDS = SelectedGameCharacter.Hero.DECK.CARDS;
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
        Debug.Log("Добор героя " + SelectedGameCharacter.Hero.HANDDRAW);
        Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
        SelectedGameCharacter.Hero.Draw();
        Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
        DisplayHandCards(SelectedGameCharacter.Hero.HAND);
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

    private void SetupCardUI(Transform cardTransform, AbstractCard card)
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
