using Iterex.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iterex.Entity.Player
{
    public class Woodcutter : Player
    {
        public Woodcutter() 
            : base(Global.PlayerAnimations["Woodcutter"], "Idle")
        {

        }
    }
}
