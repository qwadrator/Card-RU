namespace Cards {
	public class HeroDeck1:AbstractDeck{

		public HeroDeck1() : base(Owner.Player)
		{
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultDefenseCard());
		}
	}
}
