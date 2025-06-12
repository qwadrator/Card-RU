using System.IO;
using Cards.ICardInterfaces;
using Scripts.Effects;
namespace Cards{
	public class DefaultBuffCard : AbstractCard, ISelf
	{
		public DefaultBuffCard() : base("DefaultBuffCardID", "DefaultBuffCard", "OneSinCrop", 0, "дает 4 защиты в начале хода", CardType.ABILITY, CardTarget.SELF)
		{
		}

		public void Use(AbstractGameCharacter h)
		{
			h.ApplyEffect(new SteelDefense());
		}
		public override AbstractCard Copy()
		{
			return new DefaultBuffCard();
		}
		public override void SpMax()
		{
		}
		public override void Use()
        {
        }
        public override void SpMin()
		{
		}
	}
}
