
using System;
namespace Cards {
	public partial class Monster1 : AbstractGameCharacter
	{
		public Monster1() : base("EvilMaid", 30, new Monter1Deck(), 2)
		{
		}
		public override void HeroEvents()
		{
		}
	}
}
