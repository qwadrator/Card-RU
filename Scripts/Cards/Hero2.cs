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
            this.Description = "Военачальник воодушивляет карты и переманивает карты противника. Вначале игры получает артефакт:"; ;
        }
        public override void HeroEvents()
        {
            EventManager.AddEvent(() => SelectedGameCharacter.Hero.DECKDRAW.CARDS.Last().SPchange(-50),"OnTurnStart", oneTime: false);
        }
    }
}