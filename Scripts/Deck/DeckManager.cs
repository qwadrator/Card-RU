using UnityEngine;
using UnityEngine.UI;
using Cards;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DeckManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform parentTransform;
    public GameObject DECK;
    public List<GameObject> ObjectsForHide;

    private void Start()
    {
        if (gameObject.name == "Deck" && gameObject.scene.name != null)
        {
            Debug.Log("DeckStart");
            if (SelectedGameCharacter.Hero != null)
            {
                Debug.Log("Создаем колоду!");
                AbstractDeck Deck = SelectedGameCharacter.Hero.DECK;
                DisplayDeck(Deck);

            }
            else
            {
                Debug.LogError("Герой не найден!");
                DisplayDeck(new HeroDeck1());
            }
        }
    }
    public void DisplayDeck(AbstractDeck Deck)
    {
        if (Deck == null || Deck.CARDS == null)
        {
            Debug.LogError("Колода пуста или не задана!");
            return;
        }

        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }
        foreach (AbstractCard Card in Deck.CARDS)
        {
            GameObject cardInstance = Instantiate(cardPrefab, parentTransform);
            Transform cardFull = cardInstance.transform;

            int cardIndex = cardFull.GetSiblingIndex();
            //Debug.Log(cardIndex);
            cardFull.localPosition += new Vector3(cardIndex % 6 * 290f, (int)(cardIndex / 6) * -360f, 0);


            Text Description = cardFull.Find("Description")?.GetComponent<Text>();
            Image cardImage = cardFull.Find("CardPicture")?.GetComponent<Image>();

            if (Description == null || cardImage == null)
            {
                Debug.LogError("Не найдены компоненты в префабе!");
                Destroy(cardInstance);
                continue;
            }
            Description.text = Card.RAWDESCRIPTION;
            Sprite loadedSprite = Resources.Load<Sprite>(Card.IMG);
            if (loadedSprite != null)
            {
                cardImage.sprite = loadedSprite;
            }
            else
            {
                Debug.LogError($"Не удалось загрузить спрайт: {Card.IMG}");
                cardImage.color = Color.red;
            }
        }
    }
    public void ToggleDeckView(AbstractDeck cards)
    {
        if (DECK == null) Debug.Log("Null");
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
