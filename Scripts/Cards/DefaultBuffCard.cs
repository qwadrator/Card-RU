using System.IO;
using Cards.ICardInterfaces;
using Scripts.Effects;
namespace Cards{
	public class DefaultBuffCard : AbstractCards, ISelf
	{   
		public DefaultBuffCard(): base("DefaultBuffCardID", "DefaultBuffCard", "res://Pic/original.jpg", 0, "дает 4 защиты в начале хода", CardType.ABILITY, CardTarget.SELF)
		{   
		}
		
		public void Use(AbstractGameCharacter  h)
		{
            h.ApplyEffect(new SteelDefense());
        }
	}
}
