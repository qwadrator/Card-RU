namespace Cards {
	public class Monter1Deck:AbstractDeck{
		public Monter1Deck() : base(Owner.Enemy)
		{
			this.CARDS.Add(new MonsterCard());
			this.CARDS.Add(new MonsterCard());
			this.CARDS.Add(new MonsterCard2());
		}
		public void Print(){
			foreach(var card in this.CARDS){
				card.Print();
			}
		}
	}
}
