using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Cards;
using Unity.VisualScripting;

public class DeckManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform parentTransform;

    void Start()
    {
        DisplayDeck(new HeroDeck());
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

        foreach (AbstractCards Card in Deck.CARDS)
        {
            GameObject cardInstance = Instantiate(cardPrefab, parentTransform);
            Transform cardFull = cardInstance.transform;

            int cardIndex = cardFull.GetSiblingIndex();
            if (cardIndex > 5)
            {
                cardFull.localPosition += new Vector3(cardIndex%6*290f, (int)(cardIndex/6) * -360f, 0);
            }
            else
            {
                cardFull.localPosition += new Vector3(cardIndex * 290f, 0, 0);
            }

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
}
