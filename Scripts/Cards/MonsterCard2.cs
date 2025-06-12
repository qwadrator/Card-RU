using System.IO;
using Cards.ICardInterfaces;
namespace Cards {
	public class MonsterCard2 : AbstractCard, ISelf
	{
		public int Block = 5;
		public MonsterCard2() : base("MonterBlockCardID", "MonterBlockCardID", "SingingMachineCrop", 0, "Наносит 5 урона", CardType.SKILL, CardTarget.SELF)
		{
		}

		public void Use(AbstractGameCharacter Monster)
		{
		}
		public override AbstractCard Copy()
		{
			return new MonsterCard2() { Block = this.Block };
		}
		public override void SpMax()
		{
			this.Block += 3;
		}
		public override void SpMin()
		{
			this.Block += 5;
		}
		public override void Use()
        {
        }
	}
}
