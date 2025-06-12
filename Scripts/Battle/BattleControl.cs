using System.Collections.Generic;
using Cards;

using Scripts.Effects;
using UnityEngine;
using UnityEngine.UI;

public class BattleControl : MonoBehaviour
{
    [Header("Hero")]
    public GameObject HandHero;
    public List<Text> HpHero;
    public Image HeroSprite;
    public GameObject cardPrefab;

    [Space(20)]

    [Header("Enemy")]
    public Text HpEnemy;
    public Image EnemySprite;
    public GameObject HandEnemy;
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
            HpHero[0].text = SelectedGameCharacter.Hero.MAXHP.ToString();
            HpHero[1].text = SelectedGameCharacter.Hero.MAXHP.ToString();
            HeroSprite.sprite = SelectedGameCharacter.HeroSprite;
            SelectedGameCharacter.Hero.HeroEvents();

            SelectedGameCharacter.Hero.DECKDRAW.CARDS.Clear();
            SelectedGameCharacter.Hero.DECKDRAW.CARDS.AddRange(SelectedGameCharacter.Hero.DECK.CARDS);
        }
        else
        {
            SelectedGameCharacter.Hero = new Hero1();
            Debug.Log("Hero Created");

            HpHero[0].text = SelectedGameCharacter.Hero.MAXHP.ToString();
            HpHero[1].text = SelectedGameCharacter.Hero.MAXHP.ToString();
            HeroSprite.sprite = SelectedGameCharacter.HeroSprite;
            SelectedGameCharacter.Hero.HeroEvents();

            SelectedGameCharacter.Hero.DECKDRAW.CARDS.Clear();
            SelectedGameCharacter.Hero.DECKDRAW.CARDS.AddRange(SelectedGameCharacter.Hero.DECK.CARDS); 
        }
        
        //Debug.LogError("Hero not created!");
    }
    public void CreateEnemy()
    {
        if (Enemies.Enemy != null)
        {
            HpEnemy.text = Enemies.Enemy.MAXHP.ToString();
            EnemySprite.sprite = Enemies.EnemySprite;

            Enemies.Enemy.DECKDRAW.CARDS.Clear();
            Enemies.Enemy.DECKDRAW.CARDS.AddRange(Enemies.Enemy.DECK.CARDS);
        }
        else
        {
            Enemies.Enemy = new Monster1();
            Debug.Log("Monster Created");

            HpEnemy.text = Enemies.Enemy.MAXHP.ToString();
            HeroSprite.sprite = Enemies.EnemySprite;

            Enemies.Enemy.DECKDRAW.CARDS.Clear();
            Enemies.Enemy.DECKDRAW.CARDS.AddRange(Enemies.Enemy.DECK.CARDS); 
        }
    }
    public static void BattleStartTrigger()
    {
        if (EventManager.OnBattleStart == null || EventManager.OnBattleStart.Count == 0)
            return;
        for (int j = EventManager.OnBattleStart.Count - 1; j >= 0; j--)
		{
			EventManager.OnBattleStart[j]?.Invoke();
		}
        SelectedGameCharacter.Hero.DECKDRAW.CARDS = SelectedGameCharacter.Hero.DECK.CARDS;
    }
    public static void TurnStartTrigger()
    {
        if (EventManager.OnTurnStart == null || EventManager.OnTurnStart.Count == 0)
            return;
        for (int j = EventManager.OnTurnStart.Count - 1; j >= 0; j--)
		{
            Debug.Log("OnTurnStart начат тут что-то есть");
			EventManager.OnTurnStart[j]?.Invoke();
		}
    }
    public void TurnHero()
    {
        TurnStartTrigger();
        DrawCards();
    }
    public void DrawCards()
    {
        Debug.Log("Добор героя " + SelectedGameCharacter.Hero.HANDDRAW);
        Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
        SelectedGameCharacter.Hero.Draw();
        //Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
        DisplayHandCards(SelectedGameCharacter.Hero.HAND, HandHero, 1200f);

        Debug.Log("Добор Врага " + Enemies.Enemy.HANDDRAW);
        Enemies.Enemy.Draw();
        DisplayHandCards(Enemies.Enemy.HAND, HandEnemy, 540f);
    }
    private void DisplayHandCards(AbstractDeck deck, GameObject Hand, float maxContainerWidth)
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
        Text SP = cardTransform.Find("Sp")?.GetComponent<Text>();
        Image cardImage = cardTransform.Find("CardPicture")?.GetComponent<Image>();

        if (description == null || cardImage == null|| SP == null)
        {
            Debug.LogError("Не найдены компоненты в префабе карты!");
            return;
        }
        SP.text = card.SP.ToString();
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
    public void EndTurn()
    {
        EnemyTurn();
    }

    public void EnemyTurn()
    {
        foreach (AbstractCard card in Enemies.Enemy.HAND.CARDS)
        {
            card.Use();
        }
        TurnHero();
    }
}
