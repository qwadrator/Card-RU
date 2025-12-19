using System;
using System.IO;
using System.Linq.Expressions;
using Cards.ICardInterfaces;
using UnityEngine;
namespace Cards{
	public class DefaultDefenseCard : AbstractCard, ISelf
	{
		public int BASEBLOCK = 5;
		public int BLOCK = 5;
		public DefaultDefenseCard() : base("DefaultDefenseCardID", "DefaultDefenseCard", "OneSinCrop", 0, "Дает пока что 5 защиты", CardType.SKILL, CardTarget.SELF)
		{
		}
		public override AbstractCard Copy()
		{
			return new DefaultDefenseCard() { BLOCK = this.BLOCK };
		}
		public override void SpMax()
		{
			BLOCK = 7;
			DescriptionChange("Дает: " + BLOCK + " защиты");
		}
		public override void SpMin()
		{
			this.BLOCK = 12;
			DescriptionChange("Дает: " + BLOCK + " защиты \n Отнимает 2 хп");
		}
		public void SPZero()
		{
			this.SP = 0;
			BLOCK = BASEBLOCK;
			DescriptionChange("пока что" + BLOCK + "защиты");
		}
		public override void Use(AbstractGameCharacter Hero, AbstractGameCharacter Monster)
		{
			Hero.GainBlock(BLOCK);
			if (BLOCK > BASEBLOCK)
			{
				if (BLOCK == 12)
				{
					Hero.HPchange(-2);
				}
				SPZero();
			}
			Debug.Log(BLOCK + "   Полученно ");
			BaseUse();
		}
		public void DescriptionChange(String str)
		{
			rawDescriptionChange(str);
		}
	}
}
