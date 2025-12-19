
using System;
using System.Linq;
using System.ComponentModel;
using Scripts.Effects;
using UnityEngine;
namespace Cards {
	public partial class Hero1 : AbstractGameCharacter
	{
		public Hero1() : base(
			"berserk",
			100,
			new HeroDeck1(),
			5)
		{
			this.Description = "Воин бьет дубинкой, постоянно изменяет SP карт.В начале каждого хода повышает SP первой вытянутой карты до максимума.";
		}
		public override void HeroEvents()
		{
			BaseEvents();
			EventManager.AddEvent(() =>
			{
				if (SelectedGameCharacter.Hero.DECKDRAW.Count > 0)
				{
					SelectedGameCharacter.Hero.DECKDRAW.Last().SPchange(50);
				}
				else
				{
					Debug.LogError("0 Карт в стопки добора ТРЕБУЕТСЯ ФИКС");
				}
			}
			, "OnTurnStart", oneTime: false);
        }
	}
}

