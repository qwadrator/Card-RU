using System.Collections.Generic;
using System;
using System.Linq;

namespace Cards {
	public abstract class AbstractDeck
	{

		public Owner OWN { get; set; }

		public List<AbstractCard> CARDS { get; set; }
		protected AbstractDeck(Owner OWN)
		{
			this.CARDS = new List<AbstractCard>();
			this.SetValues(OWN);
		}

		public void Shuffle()
		{
			this.CARDS = CARDS.OrderBy(a => Guid.NewGuid()).ToList();
		}

		protected void SetValues(Owner Own)
		{
			this.OWN = Own;
		}
		public void AddCard(AbstractCard card)
		{
			this.CARDS.Add(card);
		}
		public int Count()
		{
			int count = 0;
			foreach (AbstractCard Card in CARDS)
			{
				count++;
			}
			return count;
		}
		public void RemoveAllCards()
		{
			if (CARDS != null)
			{
				CARDS.Clear();
			}
		}
		public bool IsNotNull()
		{
			if (CARDS == null || CARDS.Count == 0) return false;
			else return true; ;
		}
		public void RemoveCard(AbstractCard card)
		{
			int index = CARDS.FindIndex(c => c.NAME == card.NAME);
			if (index != -1)
			{
				this.CARDS.Remove(CARDS[index]);
			}
			else
			{
				System.Console.WriteLine("The card does not exist");
			}

		}

		public void Print()
		{
			foreach (var card in this.CARDS)
			{
				card.Print();
			}
		}
	}
}
