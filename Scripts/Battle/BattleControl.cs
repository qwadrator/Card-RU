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
    public Text MaxHpHero;
    public GameObject HeroBlock;
    public Image HeroSprite;
    public GameObject cardPrefab;

    [Space(20)]

    [Header("Enemy")]
    public Text HpEnemy;
    public Image EnemySprite;
    public GameObject EnemyBlock;
    public GameObject HandEnemy;

    [Space(20)]

    [Header("Different")]
    public float cardSpacing = 100f;
    public GameObject Battle;
    public GameObject ENDCONDITION;
    public Text EndText;


    void Start()
    {
        CheckEvents();
        CharacterCheck();
        BattleStartTrigger();
        TurnHero();
    }
    public void CheckEvents()
    {
        if (EventManager.ShowBlock.Count == 0 && EventManager.ShowDMG.Count == 0)
        {
            Debug.Log("Event ADD");
            EventManager.AddEvent(ShowDamage, "ShowDMG");
            EventManager.AddEvent(ShowBlock, "ShowBlock");
            EventManager.AddEvent(() =>
			{
				BlockDisplay();
			}
			, "OnTurnStart", oneTime: false);
        }
    }
    private void ShowDamage(object[] args)
    {
        AbstractGameCharacter target = (AbstractGameCharacter)args[0];
        int damage = (int)args[1];
        Debug.Log($"Показываем урон: {damage} на {target.NAME}");
        HpHero[0].text = SelectedGameCharacter.Hero.TEMPHP.ToString();
        HpHero[1].text = SelectedGameCharacter.Hero.TEMPHP.ToString();
        BlockDisplay();
        HpEnemy.text = Enemies.Enemy.TEMPHP.ToString();
        CheckEndBattle();
    }
    private void ShowBlock()
    {
        BlockDisplay();
    }
    private void BlockDisplay()
    {
        Debug.Log("Display Started");
        if (SelectedGameCharacter.Hero.BLOCK > 0)
        {
            Debug.Log("Display Block");
            HeroBlock.GetComponent<Text>().text = SelectedGameCharacter.Hero.BLOCK.ToString();
            HeroBlock.SetActive(true);
        }
        else HeroBlock.SetActive(false);
        if (Enemies.Enemy.BLOCK > 0)
        {
            EnemyBlock.GetComponent<Text>().text = Enemies.Enemy.BLOCK.ToString();
            EnemyBlock.SetActive(true);
        }
        else EnemyBlock.SetActive(false);
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
            MaxHpHero.text = "/" + SelectedGameCharacter.Hero.MAXHP.ToString();
            HpHero[0].text = SelectedGameCharacter.Hero.TEMPHP.ToString();
            HpHero[1].text = SelectedGameCharacter.Hero.TEMPHP.ToString();
            HeroSprite.sprite = SelectedGameCharacter.HeroSprite;

            SelectedGameCharacter.Hero.HeroEvents();


            SelectedGameCharacter.Hero.DECKDRAW.Clear();
            SelectedGameCharacter.Hero.DECKDRAW.AddRange(SelectedGameCharacter.Hero.DECK.CARDS);
        }
        else
        {
            SelectedGameCharacter.Hero = new Hero2();
            Debug.Log("Hero Created");

            MaxHpHero.text = "/" + SelectedGameCharacter.Hero.MAXHP.ToString();
            HpHero[0].text = SelectedGameCharacter.Hero.TEMPHP.ToString();
            HpHero[1].text = SelectedGameCharacter.Hero.TEMPHP.ToString();
            HeroSprite.sprite = SelectedGameCharacter.HeroSprite;
            SelectedGameCharacter.Hero.HeroEvents();

            SelectedGameCharacter.Hero.DECKDRAW.Clear();
            SelectedGameCharacter.Hero.DECKDRAW.AddRange(SelectedGameCharacter.Hero.DECK.CARDS);
        }

        //Debug.LogError("Hero not created!");
    }
    public void CreateEnemy()
    {
        if (Enemies.Enemy != null)
        {
            HpEnemy.text = Enemies.Enemy.MAXHP.ToString();
            EnemySprite.sprite = Enemies.EnemySprite;

            Enemies.Enemy.DECKDRAW.Clear();
            Enemies.Enemy.DECKDRAW.AddRange(Enemies.Enemy.DECK.CARDS);
        }
        else
        {
            Enemies.Enemy = new Monster1();
            Debug.Log("Monster Created");

            HpEnemy.text = Enemies.Enemy.MAXHP.ToString();
            HeroSprite.sprite = Enemies.EnemySprite;

            Enemies.Enemy.DECKDRAW.Clear();
            Enemies.Enemy.DECKDRAW.AddRange(Enemies.Enemy.DECK.CARDS);
        }
    }
    public static void BattleStartTrigger()
    {
        if (EventManager.OnBattleStart == null || EventManager.OnBattleStart.Count == 0)
            return;
        for (int j = EventManager.OnBattleStart.Count - 1; j >= 0; j--)
        {
            EventManager.OnBattleStart[j]?.Invoke();
            Debug.Log("Battle Start Event Triggered");
        }
        SelectedGameCharacter.Hero.DECKDRAW.Clear();
        for (int i = 0; i < SelectedGameCharacter.Hero.DECK.Count(); i++)
        {
            SelectedGameCharacter.Hero.DECKDRAW.Add(SelectedGameCharacter.Hero.DECK.CARDS[i]);
        }
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
    public static void TurnEndTrigger()
    {
        if (EventManager.OnTurnEnd == null || EventManager.OnTurnEnd.Count == 0)
            return;
        for (int j = EventManager.OnTurnEnd.Count - 1; j >= 0; j--)
        {
            Debug.Log("OnTurnEnd начат тут что-то есть");
            EventManager.OnTurnEnd[j]?.Invoke();
        }
    }
    public void TurnHero()
    {
        TurnStartTrigger();
        DrawCards();
    }
    public void DrawCards()
    {
        //Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());

        DisplayHeroHand();
        DisplayHandCards(Enemies.Enemy, HandEnemy, 540f);
    }
    public void DisplayHeroHand()
    {
        DisplayHandCards(SelectedGameCharacter.Hero, HandHero, 1200f);
    }
    private void DisplayHandCards(AbstractGameCharacter Character, GameObject Hand, float maxContainerWidth)
    {
        List<AbstractCard> handCards = Character.HAND;
        foreach (Transform child in Hand.transform)
        {
            Destroy(child.gameObject);
        }

        if (handCards == null || handCards.Count == 0)
        {
            Debug.Log("Нет карт для отображения в руке");
            return;
        }

        const float cardWidth = 200f;
        int cardCount = handCards.Count;

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
            //Debug.Log($"Карта {i}: SP = {deck.CARDS[i].SP}, имя = {deck.CARDS[i].NAME}");
            SetupCardUI(cardInstance.transform, handCards[i]);
            CardClickHandler clickHandler = cardInstance.GetComponent<CardClickHandler>();
            if (clickHandler == null)
            {
                clickHandler = cardInstance.AddComponent<CardClickHandler>();
            }
            if (handCards.Count > 2)
            {
                clickHandler.SetCard(handCards[i], Character);
                clickHandler.EnableInteraction(true); 
            }
        }
    }

    private void SetupCardUI(Transform cardTransform, AbstractCard card)
    {
        Text description = cardTransform.Find("Description")?.GetComponent<Text>();
        Text SP = cardTransform.Find("Sp")?.GetComponent<Text>();
        Image cardImage = cardTransform.Find("CardPicture")?.GetComponent<Image>();
        if (description == null || cardImage == null || SP == null)
        {
            Debug.LogError("Не найдены компоненты в префабе карты!");
            return;
        }

        //Debug.Log(card.SP + "  SP КартЫ:");
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
        TurnEndTrigger();
        EnemyTurn();
        DiscardCards();
        TurnHero();
    }
    public void DiscardCards()
    {
        SelectedGameCharacter.Hero.Discard();
        Enemies.Enemy.Discard();
    }

    public void EnemyTurn()
    {

        Debug.Log(Enemies.Enemy.HAND.Count);
        foreach (AbstractCard card in Enemies.Enemy.HAND)
        {
            Debug.Log("CARD USED");
            card.Use(SelectedGameCharacter.Hero, Enemies.Enemy);
            CheckEndBattle();
        }
    }
    public void CheckEndBattle()
    {
        if (SelectedGameCharacter.Hero.CheckLIfe())
        {
            if (Enemies.Enemy.CheckLIfe())
            {
                return;
            }
            else
            {
                DisplayBattleEnd(true);
            }
        }
        else
        {
            DisplayBattleEnd(false);
        }
    }
    public void DisplayBattleEnd(bool win)
    {
        Battle.SetActive(false);
        ENDCONDITION.SetActive(true);
        if (win)
        {
            EndText.text = "You Win";
        }
        else
        {
            EndText.text = "You Lose";
        }
    }
}
