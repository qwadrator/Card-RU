using System;
using System.Linq;
using Scripts.Effects;

namespace Cards
{
    public class Hero2 : AbstractGameCharacter
    {
        public Hero2() : base(
            "Warlord",
            60,
            new HeroDeck2(),
            4)
        {
            this.Description = "Военачальник воодушивляет карты и переманивает карты противника. В начале каждого хода понижает SP первой вытянутой карты до минимума."; ;
        }
        public override void HeroEvents()
        {
            BaseEvents();
            EventManager.AddEvent(() => SelectedGameCharacter.Hero.DECKDRAW.CARDS.Last().SPchange(-50),"OnTurnStart", oneTime: false);
        }
    }
}