using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Entity
{
    class EntityConfiguration
    {
        //Gravity
        public static Vector2 Gravity = new Vector2(0.0f, 0.5f * 60f);
        //The amount of acceleration when press A or D
        public static Vector2 AccelerationX = new Vector2(0.1f, 0.0f);
        //The amount of deceleration when release A or D
        public static Vector2 DecelerationX = new Vector2(0.1f, 0.0f);
        //To limit the speed
        public static Vector2 MaxSpeed = new Vector2(1.5f, 8f);
        //Initial force when press jump
        public static Vector2 InitialJumpSpeed = new Vector2(0.0f, 8.0f);
    }
}
