using Iterex.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iterex.Entity.Enemy
{
    class Centipede : MeleeEnemy
    {
        public Centipede()
            : base(Global.EnemyAnimations["Centipede"], "Idle")
        {

        }
    }
}
