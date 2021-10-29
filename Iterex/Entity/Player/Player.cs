using System;
using System.Collections.Generic;
using System.Linq;
using Iterex.Common;
using Iterex.Common.Animation;
using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.Entity.Player
{
    public class Player : AnimatedEntity
    {
        public Player(List<ITextureAdapter> textureAdapter)
            : base(textureAdapter)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Move();
            SetAnimation();
            _animationManager.Update(gameTime);

            //Check for collision
            foreach (Sprite sprite in sprites)
            {
                if (sprite == this || !sprite.IsSolid)
                    continue;

                if ((this.Velocity.X * deltaTime > 0 && this.IsTouchingLeft(sprite, deltaTime)) ||
                    (this.Velocity.X * deltaTime < 0 && this.IsTouchingRight(sprite, deltaTime)))
                {
                    this.Velocity.X = 0;
                }
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
                if (Velocity.X < 0)
                    Velocity.X = 0;
                Velocity += EntityConfiguration.AccelerationX * Speed;
                HorizontalMoveRequest = true;
            }
            if (Global.KeyboardState.IsKeyDown(Keys.A))
            {
                if (Velocity.X > 0)
                    Velocity.X = 0;
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

        private void SetAnimation()
        {
            int preDirection = Direction;
            if (Velocity.X > 0)
                Direction = 1;
            if (Velocity.X < 0)
                Direction = -1;

            //Adjust position after turning
            if (preDirection != Direction)
            {
                if (Direction == 1)
                    Position.X += TextureBox.Width / 2;
                else
                    Position.X -= TextureBox.Width / 2;
            }

            if (!OnGround)
            {
                if (Direction == 1)
                    _animationManager.PlayAnimation(_animations["JumpRight"]);
                else
                    _animationManager.PlayAnimation(_animations["JumpLeft"]);
            }
            else if (Velocity.X > 0)
                _animationManager.PlayAnimation(_animations["RunRight"]);
            else if (Velocity.X < 0)
                _animationManager.PlayAnimation(_animations["RunLeft"]);
            else
            {
                if (Direction > 0)
                    _animationManager.PlayAnimation(_animations["IdleRight"]);
                else
                    _animationManager.PlayAnimation(_animations["IdleLeft"]);
            }
            
        }
    }
}
