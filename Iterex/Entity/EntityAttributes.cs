using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Entity
{
    public class EntityAttributes
    {
        public int MaxHP;
        public int MaxMP;
        public int HP;
        public int MP;
        public int Damage;
        public int DodgeChance;
        public int Team;
        public float Speed;
        public bool StartAttacking;
        public bool IsAttacking;
        public bool StartReceivingDamage;
        public bool IsReceivingDamage;
        public bool StartDeadAnimation;
        public bool IsPlayingDeadAnimation;
        public bool IsRemovable;

        public EntityAttributes() {
            StartAttacking = false;
            IsAttacking = false;
            StartReceivingDamage = false;
            IsReceivingDamage = false;
            StartDeadAnimation = false;
            IsPlayingDeadAnimation = false;
            IsRemovable = false;
        }
    }
}
