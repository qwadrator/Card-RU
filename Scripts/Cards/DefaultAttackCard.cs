using System.IO;
//using UnityEditor.Build.Player;
using Cards.ICardInterfaces;
namespace Cards {
	public class DefaultAttackCard : AbstractCard, IActionOne
	{
		public int DAMAGE = 7;
		public DefaultAttackCard() : base("DeffaultAttackCardID", "DeffaultAttackCard", "Bear", 0, "Наносит пока что 7 урона", CardType.ATTACK, CardTarget.ENEMY)
		{
		}
		/*
		public void Use(AbstractMonster m, AbstractHero h)
		{
			new AttackAction(h,m,DAMAGE);
		}
		*/
		public override AbstractCard Copy()
		{
			return new DefaultAttackCard() { DAMAGE = this.DAMAGE };
		}
    }
}
