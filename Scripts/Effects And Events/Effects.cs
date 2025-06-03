using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scripts.Effects
{
    public abstract class Effects
    {
        public string Id { get; protected set; }
        public int Count { get; protected set; }
        public int Potency { get; protected set; }
        public Action ApplyEffect { get; protected set; } 
        public Action RemoveEffect { get; protected set; } 
        public bool IsTemporary { get; protected set; }

        protected Effects(string id, int potency, bool isTemporary){
            Id = id;
            Potency = potency;
            IsTemporary = isTemporary;
        }
    }
}