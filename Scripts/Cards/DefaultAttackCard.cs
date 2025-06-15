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
		public override void Use(AbstractGameCharacter Hero, AbstractGameCharacter Monster)
		{
			//new AttackAction(h,m,DAMAGE);
			Monster.Damage(DAMAGE);
		}
		public override AbstractCard Copy()
		{
			return new DefaultAttackCard() { DAMAGE = this.DAMAGE };
		}
		public override void SpMax()
		{
			this.DAMAGE += 3;
			DescriptionChange();
		}
		public override void SpMin()
		{
			this.DAMAGE += 5;
			TargetSet(CardTarget.All);
			DescriptionChange();
		}
		public void DescriptionChange()
		{
			rawDescriptionChange("Наносит: " + DAMAGE + "урона");
		}
    }
}
