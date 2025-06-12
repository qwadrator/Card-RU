using System.IO;
using Cards.ICardInterfaces;
namespace Cards {
	public class MonsterCard2 : AbstractCard, ISelf
	{
		public int Block = 5;
		public MonsterCard2() : base("MonterBlockCardID", "MonterBlockCardID", "", 0, "", CardType.SKILL, CardTarget.SELF)
		{
		}

		public void Use(AbstractGameCharacter Monster)
		{
		}
		public override AbstractCard Copy()
		{
			return new MonsterCard2() { Block = this.Block };
		}
	}
}
