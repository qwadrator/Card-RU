using System.Collections.Generic;
namespace Cards.ICardInterfaces {
    interface IActionOne{  //действие против 1 врага/союзника/героя
        void Use(AbstractGameCharacter Monster, AbstractGameCharacter Hero){}
    }
    interface IActionMany{  //Действие героя против множества противников
        void Use(List<AbstractGameCharacter> Monsters, AbstractGameCharacter Hero){}
    }
    interface ISelf{  //действие на самого себя(броня, бафф)
        void Use(AbstractGameCharacter Monster){}     
    }
    interface IEnemyEnemy{ //действие врага на врага
        void Use(AbstractGameCharacter MonsterAttacker, AbstractGameCharacter MonsterTakeDamage){}
    }
    interface IEnemyManyEnemy{ //действие врага на группу существ
        void Use(AbstractGameCharacter MonsterAttacker, List<AbstractGameCharacter> GameCharacters){}
    }
}