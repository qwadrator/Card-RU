using System.IO;
using Cards.ICardInterfaces;
namespace Cards {
	public class MonsterCard2 : AbstractCards, ISelf
	{
		public int Block = 5;
		public MonsterCard2(): base("MonterBlockCardID", "MonterBlockCardID", "", 0,"", CardType.SKILL, CardTarget.SELF)
		{   
		}

		public void Use(AbstractGameCharacter Monster)
		{
		}
	}
}
