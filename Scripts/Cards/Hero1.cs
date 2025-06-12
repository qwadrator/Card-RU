
using System;
using System.Linq;
using System.ComponentModel;
using Scripts.Effects;
namespace Cards {
	public partial class Hero1 : AbstractGameCharacter
	{
		public Hero1() : base(
			"berserk",
			100,
			new HeroDeck1(),
			5)
		{
			this.Description = "Воин бьет дубинкой, постоянно изменяет SP карт";
		}
        public override void HeroEvents()
        {
            EventManager.AddEvent(() => SelectedGameCharacter.Hero.DECKDRAW.CARDS.Last().SPchange(50),"OnTurnStart", oneTime: false);
        }
	}
}

