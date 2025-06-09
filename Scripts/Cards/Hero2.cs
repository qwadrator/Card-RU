using System;

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
                this.Description = "Военачальник воодушивляет карты и переманивает карты противника. Вначале игры получает артефакт:";;
		}
    }
}