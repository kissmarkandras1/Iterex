using System;
using System.Collections.Generic;
using Iterex.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.Entity
{
    public class Entity : Sprite
    {

        public Vector2 Velocity;
        public float Speed;
        public bool OnGround;

        public Entity(Texture2D texture) 
            : base(texture)
        {

        }

        //Check collision with other sprite
        #region Collision
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
