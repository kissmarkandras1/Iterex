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
        public Random Random;
        public Vector2 Velocity;
        public EntityAttributes Attributes;
        public List<Hitbox> Hitboxes = new List<Hitbox>();

        public Entity() : base() { }

        public Entity(ITextureAdapter texture)
            : base (texture)
        {

        }

        public Entity(Dictionary<string, ITextureAdapter> textures, string firstTexture) 
            : base(textures, firstTexture)
        {

        }

        private bool IsDead()
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
            if (IsDead())
                Global.Entities.Remove(this);
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

        //Check collision with other sprite - AABB
        #region CollisionAABB
        protected bool IsTouchingLeft(Sprite sprite, float deltaTime)
        {
            return this.CollisionBox.Right + this.Velocity.X * deltaTime > sprite.CollisionBox.Left &&
                   this.CollisionBox.Left < sprite.CollisionBox.Left &&
                   this.CollisionBox.Top < sprite.CollisionBox.Bottom &&
                   this.CollisionBox.Bottom > sprite.CollisionBox.Top;
        }

        protected bool IsTouchingRight(Sprite sprite, float deltaTime)
        {
            return this.CollisionBox.Left + this.Velocity.X * deltaTime < sprite.CollisionBox.Right &&
                   this.CollisionBox.Right > sprite.CollisionBox.Right &&
                   this.CollisionBox.Top < sprite.CollisionBox.Bottom &&
                   this.CollisionBox.Bottom > sprite.CollisionBox.Top;
        }

        protected bool IsTouchingTop(Sprite sprite, float deltaTime)
        {
            return this.CollisionBox.Bottom + this.Velocity.Y * deltaTime > sprite.CollisionBox.Top &&
                   this.CollisionBox.Top < sprite.CollisionBox.Top &&
                    this.CollisionBox.Right > sprite.CollisionBox.Left &&
                   this.CollisionBox.Left < sprite.CollisionBox.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite, float deltaTime)
        {
            return this.CollisionBox.Top + this.Velocity.Y * deltaTime < sprite.CollisionBox.Bottom &&
                   this.CollisionBox.Bottom > sprite.CollisionBox.Bottom &&
                   this.CollisionBox.Right > sprite.CollisionBox.Left &&
                   this.CollisionBox.Left < sprite.CollisionBox.Right;
        }
        #endregion
    }
}
