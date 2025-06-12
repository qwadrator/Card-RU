using System;
using System.Collections.Generic;
using Scripts.Effects;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;
using Unity.VisualScripting;
using System.Collections;

namespace Cards {
	public abstract partial class AbstractGameCharacter{
		public string NAME{ get; protected set; }
		public int MAXHP{ get; protected set; }
		public int TEMPHP{ get; protected set; }
		public int BLOCK = 0;
		public AbstractDeck DECK { get; protected set; }
		public AbstractDeck HAND{ get; set; }
		public AbstractDeck DECKDRAW { get; set; } 
		public AbstractDeck DECKDISCARD { get; set; }
		public AbstractDeck DECKBURN { get;  set; }
		public int HANDDRAW  { get; protected set; }
		public List<Effects> ActiveEffects { get; set; }
		public string Description { get; set; }
		
		public AbstractGameCharacter(string name, int Hp, AbstractDeck deck, int HandDraw)
		{
			this.NAME = name;
			this.MAXHP = Hp;
			this.TEMPHP = Hp;
			this.HANDDRAW = HandDraw;
			this.DECK = new SomeDeck(Owner.Player);
			this.DECK.CARDS.AddRange(deck.CARDS); 
			this.DECKDRAW = new SomeDeck(Owner.Player);
			this.HAND = new SomeDeck(Owner.Player);
			this.DECKDISCARD = new SomeDeck(Owner.Player);
			this.DECKBURN = new SomeDeck(Owner.Player);
			
			ActiveEffects = new List<Effects>();
		}
		public void Damage(int D) {
			this.TEMPHP -= D;
		}
		public void ApplyEffect(Effects effect)
		{
			if (effect is ICharacterEffect characterEffect)
			{
				characterEffect.SetTarget(this);
				ActiveEffects.Add(effect);
				effect.ApplyEffect();
			}
			else
			{
				throw new ArgumentException("Effect is not compatible with characters");
			}
		}
		public void Draw()
		{
			for (int i = 0; i < HANDDRAW; i++)
			{
				if (this.DECK.OWN == Owner.Player || EventManager.OnCardDraw == null || EventManager.OnCardDraw.Count == 0)
				{
					for (int j = EventManager.OnCardDraw.Count - 1; j >= 0; j--)
					{
						EventManager.OnCardDraw[j]?.Invoke();
					}
				}
				Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
				if (DECKDRAW.IsNotNull() || DECKDRAW.CARDS.Count != 0)
				{
					HAND.AddCard(DECKDRAW.CARDS.Last());
					//Debug.Log("СП последней карты:" + DECKDRAW.CARDS.Last().SP);
					DECKDRAW.CARDS.Remove(DECKDRAW.CARDS.Last());
				}
				else
				{
					if (DECKDISCARD.IsNotNull())
					{
						DECKDRAW = DECKDISCARD;
						DECKDISCARD.RemoveAllCards();
						i--;
					}
					else
					{
						Debug.LogError("Ошибка добора обратитесь к создателям богам нашим");
					}
				}
				Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
			}
		}
		public abstract void HeroEvents();
		public void GainBlock(int count)
		{
			this.BLOCK += count;
		}
	}
}
