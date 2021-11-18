using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Iterex.Entity
{
    public class Hitbox
    {
        public Rectangle Box;
        public int Team;
        public int Damage;
        public int Hits;
        public int Direction;
        public float Knockback;
        public Entity Parent;

        public void Update(float deltaTime)
        {
            for (int i = 0; i < Common.Global.Entities.Count; i++)
            {
                if (Box.Intersects(Common.Global.Entities[i].CollisionBox))
                {
                    if (Common.Global.Entities[i].Attributes.Team != this.Team)
                        Common.Global.Entities[i].ReceiveDamage(Damage);
                }
            }
        }
    }
}
