using System;
using System.Collections.Generic;

namespace Scripts.Effects{
    public static class EventManager {
        public static List<Action> OnBattleStart = new();
        public static List<Action> OnBattleEnd = new();
        public static List<Action> OnTurnStart = new();
        public static List<Action> OnTurnEnd = new();
        public static List<Action> OnCardPlay = new();
        public static List<Action> OnDamageDeal = new();
        public static List<Action> OnDamageGet = new();
        public static List<Action> OnApplayDebaff = new();
        
    }
}