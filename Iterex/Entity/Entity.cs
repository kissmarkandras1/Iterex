using System;
using System.Collections.Generic;
using Iterex.Common;
using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.Entity
{
    public class Entity : Sprite
    {
        public Random Random = new Random();
        public Vector2 Velocity;
        public EntityAttributes Attributes;
        public List<Hitbox> Hitboxes = new List<Hitbox>();

        public Entity() : base() 
        {
            IsSolid = true;
        }

        public Entity(ITextureAdapter texture)
            : base (texture)
        {
            IsSolid = true;
        }

        public Entity(Dictionary<string, ITextureAdapter> textures, string firstTexture) 
            : base(textures, firstTexture)
        {
            IsSolid = true;
        }

        public bool IsDead()
        {
            return Attributes.HP <= 0;
        }

        public bool Dodge()
        {
            if (Random.Next(1, 100) <= Attributes.DodgeChance)
                return true;
            return false;
        }
        public void ReceiveDamage(int damage)
        {
            if (Dodge())
                return;

            Attributes.HP -= damage;
            this.Attributes.StartReceivingDamage = true;
            if (IsDead())
            {
                this.Attributes.StartDeadAnimation = true;
            }
        }

        public void DealDamage(Entity target)
        {
            target.ReceiveDamage(Attributes.Damage);
        }

        public void ReceiveHeal(int heal)
        {
            Attributes.HP += heal;
            Attributes.HP = Math.Min(Attributes.HP, Attributes.MaxMP);
        }

        protected List<Sprite> GetSurroundingTiles()
        {
            List<Sprite> sprites = new List<Sprite>();

            int x = (int)(CollisionBox.X / Global.TILE_SIZE);
            int y = (int)(CollisionBox.Y / Global.TILE_SIZE);

            for (int i = -10; i < 10; ++i) 
                for (int j = -10; j < 10; ++j)
                {
                    int newX = x + i;
                    int newY = y + j;

                    if (newX >= 0 && 
                        newX < Global.ActiveWorld.Map.GetLength(0) &&
                        newY >= 0 &&
                        newY < Global.ActiveWorld.Map.GetLength(1) &&
                        Global.ActiveWorld.Map[newX, newY] != null)
                    {
                        sprites.Add(Global.ActiveWorld.Map[newX, newY]);
                    }
                }

            return sprites;
        }

        //Check collision with other sprite - AABB
        #region CollisionAABB
        protected bool IsTouchingLeft(Sprite sprite, float range)
        {
            return this.CollisionBox.Right + range > sprite.CollisionBox.Left &&
                   this.CollisionBox.Left < sprite.CollisionBox.Left &&
                   this.CollisionBox.Top < sprite.CollisionBox.Bottom &&
                   this.CollisionBox.Bottom > sprite.CollisionBox.Top;
        }

        protected bool IsTouchingRight(Sprite sprite, float range)
        {
            return this.CollisionBox.Left + range < sprite.CollisionBox.Right &&
                   this.CollisionBox.Right > sprite.CollisionBox.Right &&
                   this.CollisionBox.Top < sprite.CollisionBox.Bottom &&
                   this.CollisionBox.Bottom > sprite.CollisionBox.Top;
        }

        protected bool IsTouchingTop(Sprite sprite, float range)
        {
            return this.CollisionBox.Bottom + range > sprite.CollisionBox.Top &&
                   this.CollisionBox.Top < sprite.CollisionBox.Top &&
                    this.CollisionBox.Right > sprite.CollisionBox.Left &&
                   this.CollisionBox.Left < sprite.CollisionBox.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite, float range)
        {
            return this.CollisionBox.Top + range < sprite.CollisionBox.Bottom &&
                   this.CollisionBox.Bottom > sprite.CollisionBox.Bottom &&
                   this.CollisionBox.Right > sprite.CollisionBox.Left &&
                   this.CollisionBox.Left < sprite.CollisionBox.Right;
        }
        #endregion
    }
}
