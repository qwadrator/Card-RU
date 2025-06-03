using System;
using System.Collections.Generic;
using Scripts.Effects;

namespace Cards {
	public abstract partial class AbstractGameCharacter{
		public string NAME{ get; protected set; }
		public int MAXHP{ get; protected set; }
		public int TEMPHP{ get; protected set; }
		public int BLOCK = 0;
		public string IMG{ get; protected set; }
		public AbstractDeck DECK { get; protected set; }
		public List<Effects> ActiveEffects { get; set; }
		
		public AbstractGameCharacter(string name, int Hp, string img, AbstractDeck deck){
			this.NAME = name;
			this.MAXHP = Hp;
			this.TEMPHP = Hp;
			this.IMG = img;
			this.DECK = deck;
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
		public void GainBlock(int count){
			this.BLOCK += count;
		}
	}
}
