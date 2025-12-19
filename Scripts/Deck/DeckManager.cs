using UnityEngine;
using UnityEngine.UI;
using Cards;
using System.Collections.Generic;
using System.Diagnostics;

public class DeckManager : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();
    public GameObject cardPrefab;
    public Transform parentTransform;
    public GameObject DECK;
    public List<GameObject> ObjectsForHide;

    private void Start()
    {
        if (gameObject.name == "Deck" && gameObject.scene.name != null)
        {
            UnityEngine.Debug.Log("DeckStart");
            if (SelectedGameCharacter.Hero != null)
            {
                UnityEngine.Debug.Log("Создаем колоду!");
                List<AbstractCard> cards = SelectedGameCharacter.Hero.DECK.CARDS;
                DisplayDeck(cards);

            }
            else
            {
                UnityEngine.Debug.LogError("Герой не найден!");
                DisplayDeck(new HeroDeck1().CARDS);
            }
        }
    }
    public void DisplayDeck(List<AbstractCard> cards)
    {
        stopwatch.Start();
        if (cards == null)
        {
            UnityEngine.Debug.LogError("Колода пуста или не задана!");
            return;
        }
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i<cards.Count;i++)
        {
            AbstractCard Card = cards[i];
            GameObject cardInstance = Instantiate(cardPrefab, parentTransform);
            Transform cardFull = cardInstance.transform;

            //Debug.Log(SelectedGameCharacter.Hero.DECK.Count() + "  " + i);
            cardFull.localPosition += new Vector3(i % 6 * 290f, (int)(i / 6) * -360f, 0);

            Text SP = cardFull.Find("Sp")?.GetComponent<Text>();
            Text Description = cardFull.Find("Description")?.GetComponent<Text>();
            Image cardImage = cardFull.Find("CardPicture")?.GetComponent<Image>();

            if (Description == null || cardImage == null || SP == null)
            {
                UnityEngine.Debug.LogError("Не найдены компоненты в префабе!");
                Destroy(cardInstance);
                continue;
            }
            Description.text = Card.RAWDESCRIPTION;
            SP.text = Card.SP.ToString();
            Sprite loadedSprite = Resources.Load<Sprite>(Card.IMG);
            if (loadedSprite != null)
            {
                cardImage.sprite = loadedSprite;
            }
            else
            {
                UnityEngine.Debug.LogError($"Не удалось загрузить спрайт: {Card.IMG}");
                cardImage.color = Color.red;
            }
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log($"Время выполнения: {stopwatch.Elapsed.TotalMilliseconds} ms");
        UnityEngine.Debug.Log(cards.Count);
        stopwatch.Reset();
    }
    public void ToggleDeckView(List<AbstractCard> cards)
    {
        if (DECK == null) UnityEngine.Debug.Log("Null");
        if (DECK.activeSelf)
        {
            DECK.SetActive(false);
            foreach (GameObject Obj in ObjectsForHide)
            {
                Obj.SetActive(true);
            }
            ClearDeck();
        }
        else if (ObjectsForHide[1].activeSelf)
        {
            if (SelectedGameCharacter.Hero != null)
            {
                DisplayDeck(cards);
            }
            foreach (GameObject Obj in ObjectsForHide)
            {
                Obj.SetActive(false);
            }
            DECK.SetActive(true);
        }
    }
    public void ClearDeck()
    {
        for (int i = DECK.transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(DECK.transform.GetChild(i).gameObject);
        }
    }
    public void HeroDeckDrow() => ToggleDeckView(SelectedGameCharacter.Hero?.DECKDRAW);
    public void HeroDeckBurn() => ToggleDeckView(SelectedGameCharacter.Hero?.DECKBURN);
    public void HeroDeckDiscard() => ToggleDeckView(SelectedGameCharacter.Hero?.DECKDISCARD);
    public void EnemyDeckDrow() => ToggleDeckView(Enemies.Enemy?.DECKDRAW);
    public void EnemyDeckDiscard() => ToggleDeckView(Enemies.Enemy?.DECKDISCARD);
}
