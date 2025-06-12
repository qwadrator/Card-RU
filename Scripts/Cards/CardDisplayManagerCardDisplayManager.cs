using UnityEngine;
using UnityEngine.UI;
using Cards;

public class CardDisplayManager : MonoBehaviour
{
    public GameObject cardPrefab;
    
    public void DisplayCards(AbstractCard[] cards, Transform parentTransform, bool needClear)
    {
        if (cards == null || parentTransform == null)
        {
            Debug.LogError("Карты или родительский трансформ не заданы!");
            return;
        }
        if (needClear)
        {
            foreach (Transform child in parentTransform)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < cards.Length; i++)
        {
            AbstractCard card = cards[i];
            GameObject cardInstance = Instantiate(cardPrefab, parentTransform);
            Transform cardFull = cardInstance.transform;

            cardFull.localPosition += new Vector3(i % 6 * 290f, (int)(i / 6) * -360f, 0);

            SetupCardUI(cardFull, card);
        }
    }

    private void SetupCardUI(Transform cardTransform, AbstractCard card)
    {
        Text description = cardTransform.Find("Description")?.GetComponent<Text>();
        Image cardImage = cardTransform.Find("CardPicture")?.GetComponent<Image>();

        if (description == null || cardImage == null)
        {
            Debug.LogError("Не найдены компоненты в префабе!");
            Destroy(cardTransform.gameObject);
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
}