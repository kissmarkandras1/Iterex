using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Iterex.Entity
{
    public class Hitbox
    {
        public float Duration;
        public Rectangle Box;
        public int Team;
        public int Damage;
        public int Hits;
        public float Knockback;
        public Entity Parent;

        public void Update(float deltaTime)
        {
            Duration -= deltaTime;
            if (Duration < 0)
            {
                Parent.Hitboxes.Remove(this);
            }
            for (int i = 0; i < Common.Global.Entities.Count; i++)
            {
                if (Box.Intersects(Common.Global.Entities[i].CollisionBox))
                {
                    if (Common.Global.Entities[i].Team != this.Team)
                        Common.Global.Entities[i].Damage(Damage);
                }
            }
        }
    }
}
