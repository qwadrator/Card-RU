using System;
using System.Collections.Generic;

namespace Scripts.Effects
{
    public static class EventManager
    {
        public static List<Action> OnBattleStart = new();
        public static List<Action> OnBattleEnd = new();
        public static List<Action> OnTurnStart = new();
        public static List<Action> OnTurnEnd = new();
        public static List<Action> OnCardPlay = new();
        public static List<Action> OnCardDraw = new();
        public static List<Action> OnDamageDeal = new();
        public static List<Action> OnDamageGet = new();
        public static List<Action> OnApplayDebaff = new();

        public static void AddEvent(Action action, string eventType, bool oneTime = false)
        {
            var eventList = GetEventList(eventType);
            if (eventList == null) return;

            if (oneTime)
            {
                Action wrappedAction = null;
                wrappedAction = () =>
                {
                    action();
                    eventList.Remove(wrappedAction);
                };
                eventList.Add(wrappedAction);
            }
            else
            {
                eventList.Add(action);
            }
        }

        private static List<Action> GetEventList(string eventType)
        {
            return eventType switch
            {
                "OnBattleStart" => OnBattleStart,
                "OnBattleEnd" => OnBattleEnd,
                "OnTurnStart" => OnTurnStart,
                "OnTurnEnd" => OnTurnEnd,
                "OnCardPlay" => OnCardPlay,
                "OnCardDraw" => OnCardDraw,
                "OnDamageDeal" => OnDamageDeal,
                "OnDamageGet" => OnDamageGet,
                "OnApplayDebaff" => OnApplayDebaff,
                _ => null
            };
        }
    }
} 