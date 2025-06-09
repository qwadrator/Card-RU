namespace Cards {
	public class HeroDeck2:AbstractDeck{

		public HeroDeck2() : base(Owner.Player)
		{
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultDefenseCard());
		}
	}
}
