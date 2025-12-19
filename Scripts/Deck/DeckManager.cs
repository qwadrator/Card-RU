using UnityEngine;
using UnityEngine.UI;
using Cards;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

public class DeckManager : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();
    public GameObject cardPrefab;
    public Transform parentTransform;
    public GameObject DECK;
    public List<GameObject> ObjectsForHide;

    private List<GameObject> cardPool = new List<GameObject>();
    private int poolSize = 40;
     private Vector3[] gridPositions;
    private const int MAX_GRID_WIDTH = 6;
    private const float CARD_WIDTH = 290f;
    private const float CARD_HEIGHT = 360f;
     private Dictionary<string, Sprite> spriteCache = new Dictionary<string, Sprite>();

    private void Start()
    {
        PrecalculateGridPositions(poolSize);
        InitializeCardPool();

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
    private void PrecalculateGridPositions(int maxCards)
    {
        gridPositions = new Vector3[maxCards];
        for (int i = 0; i < maxCards; i++)
        {
            float x = (i % MAX_GRID_WIDTH) * CARD_WIDTH;
            float y = (int)(i / MAX_GRID_WIDTH) * -CARD_HEIGHT;
            gridPositions[i] = new Vector3(x, y, 0);
        }
    }

    private void InitializeCardPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject card = Instantiate(cardPrefab, parentTransform);
             card.name = $"Card_{i}";
            card.SetActive(false);
            InitializeCardComponents(card);
            cardPool.Add(card);
        }
    }
    private void InitializeCardComponents(GameObject cardObject)
    {
        CardUIComponents components = cardObject.GetComponent<CardUIComponents>();
        if (components == null)
        {
            components = cardObject.AddComponent<CardUIComponents>();
        }
        Transform cardTransform = cardObject.transform;
        components.description = cardTransform.Find("Description")?.GetComponent<Text>();
        components.sp = cardTransform.Find("Sp")?.GetComponent<Text>();
        components.cardImage = cardTransform.Find("CardPicture")?.GetComponent<Image>();
    }
    public void DisplayDeck(List<AbstractCard> cards)
    {
        stopwatch.Start();
        if (cards == null)
        {
            UnityEngine.Debug.LogError("Колода пуста или не задана!");
            return;
        }
        if (cards.Count > cardPool.Count)
        {
            ExpandCardPool(cards.Count - cardPool.Count);
        }
        foreach (var card in cardPool)
        {
            card.SetActive(false);
        }
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject cardInstance = cardPool[i];
            if (i < gridPositions.Length)
            {
                cardInstance.transform.localPosition = gridPositions[i];
            }
            else
            {
                cardInstance.transform.localPosition = CalculateGridPosition(i);
            }
            SetupCard(cardInstance, cards[i]);
            cardInstance.SetActive(true);
        }
        stopwatch.Stop();
        UnityEngine.Debug.Log($"Время выполнения: {stopwatch.Elapsed.TotalMilliseconds} ms");
        UnityEngine.Debug.Log(cards.Count);
        stopwatch.Reset();
    }
    private void ExpandCardPool(int additionalCount)
    {
        int startIndex = cardPool.Count;
        
        if (startIndex + additionalCount > gridPositions.Length)
        {
            System.Array.Resize(ref gridPositions, startIndex + additionalCount + 10);
            for (int i = startIndex; i < gridPositions.Length; i++)
            {
                gridPositions[i] = CalculateGridPosition(i);
            }
        }
        
        for (int i = 0; i < additionalCount; i++)
        {
            GameObject card = Instantiate(cardPrefab, parentTransform);
            card.name = $"Card_{startIndex + i}";
            card.SetActive(false);
            
            InitializeCardComponents(card);
            cardPool.Add(card);
        }
    }
    private Vector3 CalculateGridPosition(int index)
    {
        float x = (index % MAX_GRID_WIDTH) * CARD_WIDTH;
        float y = (int)(index / MAX_GRID_WIDTH) * -CARD_HEIGHT;
        return new Vector3(x, y, 0);
    }

    private void SetupCard(GameObject cardObject, AbstractCard card)
    {
        CardUIComponents components = cardObject.GetComponent<CardUIComponents>();
        
        if (components == null)
        {
            InitializeCardComponents(cardObject);
            components = cardObject.GetComponent<CardUIComponents>();
        }
        
        if (components.description != null)
            components.description.text = card.RAWDESCRIPTION;
        
        if (components.sp != null)
            components.sp.text = card.SP.ToString();
        LoadSpriteCached(card.IMG, components);
    }
 private void LoadSpriteCached(string spritePath, CardUIComponents components)
    {
        if (string.IsNullOrEmpty(spritePath) || components.cardImage == null)
            return;
        if (spriteCache.TryGetValue(spritePath, out Sprite cachedSprite))
        {
            components.cardImage.sprite = cachedSprite;
            return;
        }
        StartCoroutine(LoadSpriteAsync(spritePath, components));
    }
    private IEnumerator LoadSpriteAsync(string spritePath, CardUIComponents components)
    {
        if (spriteCache.ContainsKey(spritePath))
        {
            components.cardImage.sprite = spriteCache[spritePath];
            yield break;
        }
        
        ResourceRequest request = Resources.LoadAsync<Sprite>(spritePath);
        yield return request;
        
        if (request.asset != null && components.cardImage != null)
        {
            Sprite sprite = request.asset as Sprite;
            spriteCache[spritePath] = sprite;
            components.cardImage.sprite = sprite;
        }
        else
        {
            UnityEngine.Debug.LogError($"Не удалось загрузить спрайт: {spritePath}");
            if (components.cardImage != null)
                components.cardImage.color = Color.red;
        }
    }
    public void ClearSpriteCache()
    {
        spriteCache.Clear();
    }
    public void UpdateCardDisplay(int poolIndex, AbstractCard card)
    {
        if (poolIndex < 0 || poolIndex >= cardPool.Count)
            return;
        
        GameObject cardObject = cardPool[poolIndex];
        CardUIComponents components = cardObject.GetComponent<CardUIComponents>();
        
        if (components != null)
        {
            if (components.description != null)
                components.description.text = card.RAWDESCRIPTION;
            
            if (components.sp != null)
                components.sp.text = card.SP.ToString();
            
            LoadSpriteCached(card.IMG, components);
        }
    }
    public void HideAllCardsFast()
    {
        foreach (var card in cardPool)
        {
            card.SetActive(false);
        }
    }
    public void ShowCardsFast(int count)
    {
        for (int i = 0; i < count && i < cardPool.Count; i++)
        {
            cardPool[i].SetActive(true);
        }
    }
    
    public void ToggleDeckView(List<AbstractCard> cards)
    {
        if (DECK == null) 
        {
            UnityEngine.Debug.Log("DECK is Null");
            return;
        }
        
        if (DECK.activeSelf)
        {
            DECK.SetActive(false);
            foreach (GameObject Obj in ObjectsForHide)
            {
                Obj.SetActive(true);
            }
            HideAllCardsFast();
        }
        else if (ObjectsForHide[1].activeSelf)
        {
            if (cards != null)
            {
                DisplayDeck(cards);
            }
            else
            {
                UnityEngine.Debug.LogWarning("Нет карт для отображения");
                return;
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

    public class CardUIComponents : MonoBehaviour
    {
        public Text description;
        public Text sp;
        public Image cardImage;
    }
    public void HeroDeckDrow() => ToggleDeckView(SelectedGameCharacter.Hero?.DECKDRAW);
    public void HeroDeckBurn() => ToggleDeckView(SelectedGameCharacter.Hero?.DECKBURN);
    public void HeroDeckDiscard() => ToggleDeckView(SelectedGameCharacter.Hero?.DECKDISCARD);
    public void EnemyDeckDrow() => ToggleDeckView(Enemies.Enemy?.DECKDRAW);
    public void EnemyDeckDiscard() => ToggleDeckView(Enemies.Enemy?.DECKDISCARD);
}
