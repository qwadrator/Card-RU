using System.IO;
using Cards.ICardInterfaces;
namespace Cards {
	public class MonsterCard : AbstractCard, IActionOne
	{
		public int DAMAGE = 5;
		public MonsterCard() : base("DeffaultAttackCardID", "DeffaultAttackCard", "Red Button", 0, "Дает 5 защиты", CardType.ATTACK, CardTarget.ENEMY)
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
		public override void SpMax()
		{
			this.DAMAGE += 3;
		}
		public override void SpMin()
		{
			this.DAMAGE += 5;
		}
		public override void Use()
        {
        }
	}
}
