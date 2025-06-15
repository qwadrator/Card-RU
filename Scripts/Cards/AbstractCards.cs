using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System;
using JetBrains.Annotations;
namespace Cards {
	public abstract class AbstractCard
	{

		public string ID { get; set; }
		public string NAME { get; set; }
		public string IMG { get; set; }

		public int SP { get; set; }
		public CardType TYPE { get; set; }
		public string RAWDESCRIPTION { get; set; }
		//public CardColor Color { get; protected set; }
		//public CardRarity Rarity { get; protected set; }
		public CardTarget TARGET { get; set; }

		public AbstractCard(
							string id,
							string name,
							string img,
							int sp,
							string rawDescription,
							CardType type,
							//CardColor color,
							//CardRarity rarity,
							CardTarget target)
		{
			this.ID = id;
			this.NAME = name;
			this.IMG = img;
			this.SP = sp;
			this.RAWDESCRIPTION = rawDescription;
			this.TYPE = type;
			//Color = color;
			//Rarity = rarity;
			this.TARGET = target;
		}
		public void Print()
		{
			System.Console.WriteLine(this.NAME + " SP: " + this.SP + " Описание: " + this.RAWDESCRIPTION);
		}
		public void TargetSet(CardTarget target)
		{
			this.TARGET = target;
		}
		public void SPchange(int count)
		{
			this.SP += count;
			if (this.SP >= 50)
			{
				SpMax();
			}
			else if (this.SP <= 50)
			{
				SpMin();
			}
		}
		public void rawDescriptionChange(String Str)
		{
			this.RAWDESCRIPTION = Str;
		}
		public abstract void SpMax();
		public abstract void SpMin();
		public abstract void Use(AbstractGameCharacter Hero, AbstractGameCharacter Enemy);
	 	public abstract AbstractCard Copy();
	}
}
