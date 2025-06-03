using System.Collections.Generic;
using System;
using System.Linq;

namespace Cards {
	public abstract class AbstractDeck{
		
		protected Owner OWN { get; set; }
		
		public List<AbstractCards> CARDS{ get; set; }
		
		public List<AbstractCards> TEMPCARDS{ get;  set; }
		
		protected AbstractDeck(Owner OWN) {
			this.CARDS = new List<AbstractCards>();
			TEMPCARDS = CARDS;
			this.SetValues(OWN);
		}  
		public void DrawCard(AbstractCards card){
			if(CARDS != null){
				int index = TEMPCARDS.FindIndex(c => c.NAME == card.NAME);
				if (index != -1) {
					this.TEMPCARDS.Remove(TEMPCARDS[index]);
				}
				else {
					System.Console.WriteLine("The card does not exist");
				}
			}
		}
		public void Shuffle() {
			this.TEMPCARDS = CARDS.OrderBy(a => Guid.NewGuid()).ToList();
		}

		protected void SetValues(Owner Own){
			this.OWN = Own;
		}
	}
}
