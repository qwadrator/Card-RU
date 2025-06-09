using System;
using System.Collections.Generic;
using Scripts.Effects;
using UnityEngine;
using System.Linq;
using UnityEngine.XR;

namespace Cards {
	public abstract partial class AbstractGameCharacter{
		public string NAME{ get; protected set; }
		public int MAXHP{ get; protected set; }
		public int TEMPHP{ get; protected set; }
		public int BLOCK = 0;
		public AbstractDeck DECK { get; protected set; }
		public AbstractDeck HAND{ get; set; } = new SomeDeck(Owner.Player);
		public AbstractDeck DECKDRAW { get; set; } = new SomeDeck(Owner.Player);
		public AbstractDeck DECKDISCARD { get; set; } = new SomeDeck(Owner.Player);
		public AbstractDeck DECKBURN { get;  set; } = new SomeDeck(Owner.Player);
		public int HANDDRAW  { get; protected set; }
		public List<Effects> ActiveEffects { get; set; }
		public string Description { get; set; }
		
		public AbstractGameCharacter(string name, int Hp, AbstractDeck deck, int HandDraw)
		{
			this.NAME = name;
			this.MAXHP = Hp;
			this.TEMPHP = Hp;
			this.DECK = deck;
			this.HANDDRAW = HandDraw;
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
		public void Draw(int count)
		{
			for (int i = 0; i < count; i++)
			{
				if (DECKDRAW.IsNotNull() || DECKDRAW.CARDS.Count !=0 )
				{
					Debug.Log(i);
					HAND.AddCard(DECKDRAW.CARDS.Last());
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
			}
		}
		public void GainBlock(int count)
		{
			this.BLOCK += count;
		}
	}
}
