using System.IO;
using Cards.ICardInterfaces;
using UnityEngine;
namespace Cards {
	public class MonsterCard2 : AbstractCard, ISelf
	{
		public int BLOCK = 5;
		public MonsterCard2() : base("MonterBlockCardID", "MonterBlockCardID", "SingingMachineCrop", 0, "Дает 5 блока", CardType.SKILL, CardTarget.SELF)
		{
		}

		public void Use(AbstractGameCharacter Monster)
		{
		}
		public override AbstractCard Copy()
		{
			return new MonsterCard2() { BLOCK = this.BLOCK };
		}
		public override void SpMax()
		{
			this.BLOCK += 3;
		}
		public override void SpMin()
		{
			this.BLOCK += 5;
		}
		public override void Use(AbstractGameCharacter Hero, AbstractGameCharacter Monster)
		{
			//new AttackAction(h,m,DAMAGE);
			Debug.Log("MONSTER GAIN BLOCK " + BLOCK);
			Monster.GainBlock(BLOCK);
		}
	}
}
