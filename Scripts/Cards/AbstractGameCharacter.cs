using System;
using System.Collections.Generic;
using Scripts.Effects;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;
using Unity.VisualScripting;
using System.Collections;

namespace Cards {
	public abstract partial class AbstractGameCharacter
	{
		public string NAME { get; protected set; }
		public int MAXHP { get; protected set; }
		public int TEMPHP { get; protected set; }
		public int BLOCK = 0;
		public AbstractDeck DECK { get; protected set; }
		public List<AbstractCard> HAND { get; set; }
		public List<AbstractCard> DECKDRAW { get; set; }
		public List<AbstractCard> DECKDISCARD { get; set; }
		public List<AbstractCard> DECKBURN { get; set; }
		public int HANDDRAW { get; protected set; }
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
			this.DECKDRAW = new List<AbstractCard>();
			this.HAND = new List<AbstractCard>();
			this.DECKDISCARD = new List<AbstractCard>();
			this.DECKBURN = new List<AbstractCard>();

			ActiveEffects = new List<Effects>();
		}
		public void HPchange(int Count)
		{
			this.TEMPHP += Count;
			if (Count < 0)
			{
				if (EventManager.OnDamageGet != null && EventManager.OnDamageGet.Count > 0)
				{
					Debug.LogError("ТУТ ДОЛЖЕН БЫТЬ 0");
					for (int j = EventManager.OnDamageGet.Count - 1; j >= 0; j--)
					{
						EventManager.OnDamageGet[j]?.Invoke();
					}
				}
			}
			EventManager.TriggerEvent("ShowDMG", this, Count);
		}
		public void Damage(int D)
		{
			if (this.BLOCK - D < 0)
			{
				this.TEMPHP += this.BLOCK - D;
				if (EventManager.OnDamageGet != null && EventManager.OnDamageGet.Count > 0)
				{
					Debug.LogError("ТУТ ДОЛЖЕН БЫТЬ 0");
					for (int j = EventManager.OnDamageGet.Count - 1; j >= 0; j--)
					{
						EventManager.OnDamageGet[j]?.Invoke();
					}
				}
				this.BLOCK = 0;
			}
			else
			{
				this.BLOCK -= D;
			}
			EventManager.TriggerEvent("ShowBlock");
			EventManager.TriggerEvent("ShowDMG", this, D);
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
				//Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
				if (DECKDRAW != null && DECKDRAW.Count != 0)
				{
					HAND.Add(DECKDRAW.Last());
					//Debug.Log("СП последней карты:" + DECKDRAW.CARDS.Last().SP);
					DECKDRAW.Remove(DECKDRAW.Last());
				}
				else
				{
					if (DECKDISCARD != null && DECKDISCARD.Count > 0)
					{
						AddCardDrawFromDiscard();
						DECKDISCARD.Clear();
						ShuffleDeckDraw();
						i--;
					}
					else
					{
						Debug.LogError("Ошибка добора обратитесь к создателям богам нашим");
					}
				}
				//Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
			}
		}
		public void Discard()
		{
			for (int i = HAND.Count(); i > 0; i--)
			{
				DECKDISCARD.Add(HAND.Last());
				HAND.Remove(HAND.Last());
			}
		}
		public void AddCardDrawFromDiscard()
		{
			for (int i = DECKDISCARD.Count(); i > 0; i--)
			{
				DECKDRAW.Add(DECKDISCARD.Last());
				DECKDISCARD.Remove(DECKDISCARD.Last());
			}
		}

		public void ShuffleDeckDraw()
		{
			System.Random rng = new System.Random();
			int n = DECKDRAW.Count;
			while (n > 1)
			{
				n--;
				int k = rng.Next(n + 1);
				AbstractCard value = DECKDRAW[k];
				DECKDRAW[k] = DECKDRAW[n];
				DECKDRAW[n] = value;
			}
		}
		public bool CheckLIfe()
		{
			if (TEMPHP > 0)
			{
				return true;
			}
			else return false;
		}
		public abstract void HeroEvents();
		public void GainBlock(int count)
		{
			this.BLOCK += count;
			if (EventManager.OnBlockGain == null || EventManager.OnBlockGain.Count == 0)
			{
				for (int j = EventManager.OnBlockGain.Count - 1; j >= 0; j--)
				{
					EventManager.OnBlockGain[j]?.Invoke();
				}
			}
			EventManager.TriggerEvent("ShowBlock");
		}
		public void BaseEvents()
		{

			Debug.Log("BASE EVENTS TRIGGERED");
			EventManager.AddEvent(() => 
			{
				if (SelectedGameCharacter.Hero.DECKDRAW != null)
				{
					SelectedGameCharacter.Hero.ShuffleDeckDraw();
				}
			}, "OnBattleStart", oneTime: false);
			EventManager.AddEvent(() =>
			{
				Debug.Log("Добор героя " + SelectedGameCharacter.Hero.HANDDRAW);
				//Debug.Log("Количесво карт в колоде " + SelectedGameCharacter.Hero.DECK.Count());
				SelectedGameCharacter.Hero.Draw();
				Debug.Log("Добор Врага " + Enemies.Enemy.HANDDRAW);
				Enemies.Enemy.Draw();
			}
			, "OnTurnStart", oneTime: false);
			EventManager.AddEvent(() =>
			{
				SelectedGameCharacter.Hero.BLOCK = 0;
			}
			, "OnTurnStart", oneTime: false);
			EventManager.AddEvent(() =>
			{
				Enemies.Enemy.BLOCK = 0;
			}
			, "OnTurnEnd", oneTime: false);
		}
	}
}
