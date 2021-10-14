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

        #region Pixel Collision
        protected bool IntersectsPixel(Sprite sprite, float deltaTime)
        {
            Vector2 nextPosition = Position + Velocity * deltaTime;
            Rectangle nextCollisionBox = new Rectangle((int)nextPosition.X, (int)nextPosition.Y, CollisionBox.Width, CollisionBox.Height);

            int top = Math.Max(nextCollisionBox.Top, sprite.CollisionBox.Top);
            int bottom = Math.Min(nextCollisionBox.Bottom, sprite.CollisionBox.Bottom);
            int left = Math.Max(nextCollisionBox.Left, sprite.CollisionBox.Left);
            int right = Math.Min(nextCollisionBox.Right, sprite.CollisionBox.Right);

            Color[] thisTextureData = this.GetTextureData();
            Color[] spriteTextureData = sprite.GetTextureData();

            for (int x = left; x < right; x++)
                for (int y = top; y < bottom; y++)
                {
                    Color pixelOfThis = thisTextureData[(x - nextCollisionBox.Left) + (y - nextCollisionBox.Top) * nextCollisionBox.Width];
                    Color pixelOfSprite = spriteTextureData[(x - sprite.CollisionBox.Left) + (y - sprite.CollisionBox.Top) * sprite.CollisionBox.Width];

                    if (pixelOfThis.A != 0 && pixelOfSprite.A != 0)
                        return true;
                }

            return false;
        }
        #endregion
    }
}
