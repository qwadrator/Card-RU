using System;
using System.IO;
//using UnityEditor.Build.Player;
using Cards.ICardInterfaces;
namespace Cards {
	public class DefaultAttackCard : AbstractCard, IActionOne
	{
		public int BASEDAMAGE = 7;
		public int DAMAGE = 7;
		public DefaultAttackCard() : base("DeffaultAttackCardID", "DeffaultAttackCard", "Bear", 0, "Наносит пока что 7 урона", CardType.ATTACK, CardTarget.ENEMY)
		{
		}
		public override void Use(AbstractGameCharacter Hero, AbstractGameCharacter Monster)
		{
			Monster.Damage(DAMAGE);
			//new AttackAction(h,m,DAMAGE);
			if (DAMAGE > BASEDAMAGE)
			{
				if (this.TARGET == CardTarget.All)
				{
					Hero.Damage(DAMAGE / 4);
				}
				SPZero();
			}
		}
		public override AbstractCard Copy()
		{
			return new DefaultAttackCard() { DAMAGE = this.DAMAGE };
		}
		public override void SpMax()
		{
			this.DAMAGE = 10;
			DescriptionChange("Наносит " + DAMAGE + " урона");
		}
		public void SPZero()
		{
			this.SP = 0;
			DAMAGE = BASEDAMAGE;
			DescriptionChange("Наносит пока что " + DAMAGE + " урона");
		}
		public override void SpMin()
		{
			this.DAMAGE = 15;
			TargetSet(CardTarget.All);
			DescriptionChange("Наносит " + DAMAGE + " урона всем врагам \n Наносит " + DAMAGE / 4 + " урона герою");
		}
		public void DescriptionChange(String String)
		{
			rawDescriptionChange(String);
		}
    }
}
