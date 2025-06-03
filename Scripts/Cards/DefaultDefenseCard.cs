using System.IO;
using Cards.ICardInterfaces;
namespace Cards{
	public class DefaultDefenseCard : AbstractCards, ISelf
	{   
		public int BLOCK = 5;
		public DefaultDefenseCard(): base("DefaultDefenseCardID", "DefaultDefenseCard", "res://Pic/original.jpg", 0, "Защищает пока что на 5 хп", CardType.SKILL, CardTarget.SELF)
		{   
		}
		
		public void Use(AbstractGameCharacter   h)
		{
		}
	}
}
