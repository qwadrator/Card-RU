
using System;
namespace Cards {
	public partial class Hero1 : AbstractGameCharacter
	{
		public Hero1() : base(
			"berserk",
			100,
			new HeroDeck1(),
			10)
		{
			this.Description = "Воин бьет дубинкой, постоянно изменяет SP карт";
		}
	}
}

