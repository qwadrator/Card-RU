using System;
using Cards;


namespace Scripts.Effects
{
    public class SteelDefense: Effects, ICharacterEffect
    {
        private AbstractGameCharacter _target;
        private Action _armorEffect;
        public SteelDefense(): base("EffectSteelDefense", potency: 4, isTemporary: true)
		{   
            if (_target == null){
                throw new ArgumentException("Target indefinite");
            }
            _armorEffect = () =>  _target.GainBlock(this.Potency);
            ApplyEffect = () => EventManager.OnTurnStart.Add(_armorEffect);
            RemoveEffect = () => EventManager.OnTurnStart.Remove(_armorEffect);
		}
        public void SetTarget(AbstractGameCharacter target)
        {
            _target = target;
        }
    }
}