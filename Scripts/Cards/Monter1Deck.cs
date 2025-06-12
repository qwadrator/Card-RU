namespace Cards {
	public class Monter1Deck:AbstractDeck{
		public Monter1Deck() : base(Owner.Enemy)
		{
			this.CARDS.Add(new MonsterCard());
			this.CARDS.Add(new MonsterCard());
			this.CARDS.Add(new MonsterCard());
			this.CARDS.Add(new MonsterCard2());
			this.CARDS.Add(new MonsterCard2());
		}
	}
}
