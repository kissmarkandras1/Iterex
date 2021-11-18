using Iterex.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iterex.Entity.Player
{
    class GraveRobber : Player 
    {
        public GraveRobber()
            : base(Global.PlayerAnimations["GraveRobber"], "Idle")
        {

        }
    }
}
