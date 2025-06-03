namespace Cards {
	public class HeroDeck:AbstractDeck{

		public HeroDeck() : base(Owner.Player)
		{
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultAttackCard());
			this.CARDS.Add(new DefaultDefenseCard());
			this.CARDS.Add(new DefaultDefenseCard());
		}

		public void AddCard(AbstractCards card) {
			this.CARDS.Add(card);
		}

		public void RemoveCard(AbstractCards card) {
			int index = CARDS.FindIndex(c => c.NAME == card.NAME);
			if (index != -1) {
				this.CARDS.Remove(CARDS[index]);
			}
			else {
				System.Console.WriteLine("The card does not exist");
			}
			
		}

		public void Print(){
			foreach(var card in this.CARDS){
				card.Print();
			}
		}
	}
}
