using Iterex.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iterex.Entity.Player
{
    public class SteamMan : Player
    {
        public SteamMan() 
            : base(Global.PlayerAnimations["SteamMan"], "Idle")
        {

        }
    }
}
