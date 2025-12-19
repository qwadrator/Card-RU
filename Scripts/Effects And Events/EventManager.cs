using System;
using System.Collections.Generic;
using UnityEngine;

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
        public static List<Action> OnBlockGain = new();
        public static List<Action> OnApplayDebaff = new();

        public static List<Action> ShowBlock = new();
        public static List<Action<object[]>> ShowDMG = new();

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

        public static void AddEvent(Action<object[]> action, string eventType, bool oneTime = false)
        {
            Debug.Log("Add event to:" + eventType);
            var eventList = GetEventListWithParams(eventType);
            if (eventList == null) return;

            if (oneTime)
            {
                Action<object[]> wrappedAction = null;
                wrappedAction = (args) =>
                {
                    action(args);
                    eventList.Remove(wrappedAction);
                };
                eventList.Add(wrappedAction);
            }
            else
            {
                eventList.Add(action);
            }
        }

        public static void TriggerEvent(string eventType, params object[] args)
        {
            var eventList = GetEventList(eventType);
            if (eventList != null && eventList.Count > 0)
            {
                for (int j = eventList.Count - 1; j >= 0; j--)
                {
                    eventList[j]?.Invoke();
                }
            }

            var paramEventList = GetEventListWithParams(eventType);
            if (paramEventList != null && paramEventList.Count > 0)
            {
                for (int j = paramEventList.Count - 1; j >= 0; j--)
                {
                    paramEventList[j]?.Invoke(args);
                }
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
                "ShowBlock" => ShowBlock,
                _ => null
            };
        }

        private static List<Action<object[]>> GetEventListWithParams(string eventType)
        {
            return eventType switch
            {
                "ShowDMG" => ShowDMG,
                _ => null
            };
        }
    }
} 