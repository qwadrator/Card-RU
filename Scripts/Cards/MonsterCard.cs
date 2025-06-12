using System.IO;
using Cards.ICardInterfaces;
namespace Cards {
	public class MonsterCard : AbstractCard, IActionOne
	{
		public int DAMAGE = 5;
		public MonsterCard() : base("DeffaultAttackCardID", "DeffaultAttackCard", "res://Pic/backcard23.png", 0, "", CardType.ATTACK, CardTarget.ENEMY)
		{
		}

		public void Use(AbstractGameCharacter m, AbstractGameCharacter h)
		{
			//new AttackAction(h,m,DAMAGE);
		}
		public override AbstractCard Copy()
		{
			return new MonsterCard() { DAMAGE = this.DAMAGE };
		}
	}
}
