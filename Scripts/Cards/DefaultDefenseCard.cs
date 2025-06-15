using System.IO;
using Cards.ICardInterfaces;
namespace Cards{
	public class DefaultDefenseCard : AbstractCard, ISelf
	{
		public int BLOCK = 5;
		public DefaultDefenseCard() : base("DefaultDefenseCardID", "DefaultDefenseCard", "OneSinCrop", 0, "Защищает пока что на 5 хп", CardType.SKILL, CardTarget.SELF)
		{
		}

		public void Use(AbstractGameCharacter h)
		{
		}
		public override AbstractCard Copy()
		{
			return new DefaultDefenseCard() { BLOCK = this.BLOCK };
		}
		public override void SpMax()
		{
			this.BLOCK += 3;
			DescriptionChange();
		}
		public override void SpMin()
		{
			this.BLOCK += 5;
			DescriptionChange();
		}
		public override void Use(AbstractGameCharacter Hero, AbstractGameCharacter Monster)
		{
			//new AttackAction(h,m,DAMAGE);
			Hero.GainBlock(BLOCK);
		}
		public void DescriptionChange()
		{
			rawDescriptionChange("Защищает на: " + BLOCK + "хп");
		}
	}
}
