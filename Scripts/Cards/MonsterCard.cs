
using System.IO;
using Cards.ICardInterfaces;
using UnityEngine;
namespace Cards {
	public class MonsterCard : AbstractCard, IActionOne
	{
		public int DAMAGE = 9;
		public MonsterCard() : base("DeffaultAttackCardID", "DeffaultAttackCard", "Red Button", 0, "Наносит 9 урона", CardType.ATTACK, CardTarget.ENEMY)
		{
		}
		public override AbstractCard Copy()
		{
			return new MonsterCard() { DAMAGE = this.DAMAGE };
		}
		public override void SpMax()
		{
			this.DAMAGE += 3;
		}
		public override void SpMin()
		{
			this.DAMAGE += 5;
		}
		public override void Use(AbstractGameCharacter Hero, AbstractGameCharacter Monster)
		{
			//new AttackAction(h,m,DAMAGE);
			Debug.Log("DAMAGE DEALT TO HERO " + DAMAGE);
			Hero.Damage(DAMAGE);
			Debug.Log(Hero.TEMPHP);
		}
	}
}
