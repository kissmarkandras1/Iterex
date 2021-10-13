using System;
using System.Collections.Generic;
using Iterex.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.Entity.Player
{
    public class Player : Entity
    {

        public Player(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime time, List<Sprite> sprites)
        {

            float deltaTime = (float)time.ElapsedGameTime.TotalSeconds;

            Move();

            //Check for collision
            foreach (Sprite sprite in sprites)
            {
                if (sprite == this || !sprite.IsSolid)
                    continue;

                if ((this.Velocity.X * deltaTime > 0 && this.IsTouchingLeft(sprite, deltaTime)) ||
                    (this.Velocity.X * deltaTime < 0 && this.IsTouchingRight(sprite, deltaTime)))
                    this.Velocity.X = 0;
                if ((this.Velocity.Y * deltaTime > 0 && this.IsTouchingTop(sprite, deltaTime)) ||
                    (this.Velocity.Y * deltaTime < 0 && this.IsTouchingBottom(sprite, deltaTime)))
                {
                    if (this.IsTouchingTop(sprite, deltaTime))
                        this.OnGround = true;
                    this.Velocity.Y = 0;
                }
            }

            Position += Velocity * deltaTime;
        }

        private void Move()
        {
            //MARK: Is pressing A or D or not
            bool HorizontalMoveRequest = false;

            if (Global.KeyboardState.IsKeyDown(Keys.D))
            {
                Velocity += EntityConfiguration.AccelerationX * Speed;
                HorizontalMoveRequest = true;
            }
            if (Global.KeyboardState.IsKeyDown(Keys.A))
            {
                Velocity -= EntityConfiguration.AccelerationX * Speed;
                HorizontalMoveRequest = true;
            }
            //MARK: Negative Y is up
            if (Global.KeyboardState.IsKeyDown(Keys.W) && OnGround)
            {
                Velocity += -EntityConfiguration.InitialJumpSpeed * Speed;
                OnGround = false;
            }

            //Apply gravity force
            Velocity += EntityConfiguration.Gravity;

            //Limit the speed
            if (Velocity.X > EntityConfiguration.MaxSpeed.X * Speed)
                Velocity.X = EntityConfiguration.MaxSpeed.X * Speed;
            if (Velocity.X < -EntityConfiguration.MaxSpeed.X * Speed)
                Velocity.X = -EntityConfiguration.MaxSpeed.X * Speed;
            if (Velocity.Y > EntityConfiguration.MaxSpeed.Y * Speed)
                Velocity.Y = EntityConfiguration.MaxSpeed.Y * Speed;

            //Decelerate the horizontal speed when release A/D buttons
            if (!HorizontalMoveRequest)
            {
                if (Velocity.X > 0)
                    Velocity.X -= Math.Min(EntityConfiguration.DecelerationX.X * Speed, Velocity.X);
                if (Velocity.X < 0)
                    Velocity.X += Math.Min(EntityConfiguration.DecelerationX.X * Speed, -Velocity.X);
            } 
        }
    }
}
