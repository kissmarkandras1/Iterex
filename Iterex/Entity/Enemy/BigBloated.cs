using Iterex.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iterex.Entity.Enemy
{
    class BigBloated : MeleeEnemy
    {
        public BigBloated()
            : base(Global.EnemyAnimations["BigBloated"], "Idle")
        {

        }
    }
}
