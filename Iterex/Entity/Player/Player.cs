using System;
using System.Collections.Generic;
using System.Linq;
using Iterex.Common;
using Iterex.Common.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.Entity.Player
{
    public class Player : AnimatedEntity
    {
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _animations.First().Value.FrameWidth, _animations.First().Value.FrameHeight);
            }
        }
        public Player(Texture2D texture, Dictionary<string, Animation> animations)
            : base(texture, animations)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Move();
            SetAnimation();
            _animationManager.Update(gameTime);

            Console.WriteLine(CollisionBox);
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

        private void SetAnimation()
        {
            if (Velocity.X > 0)
                Direction = 1;
            if (Velocity.X < 0)
                Direction = -1;

            if (Math.Abs(Velocity.X) <= Speed)
            {
                if (Velocity.X > 0)
                    _animationManager.PlayAnimation(_animations["WalkRight"]);
                else if (Velocity.X < 0)
                    _animationManager.PlayAnimation(_animations["WalkLeft"]);
                else
                {
                    if (Direction > 0)
                        _animationManager.PlayAnimation(_animations["IdleRight"]);
                    else
                        _animationManager.PlayAnimation(_animations["IdleLeft"]);
                }
            }
            else
            {
                if (Velocity.X > 0)
                    _animationManager.PlayAnimation(_animations["RunRight"]);
                else
                    _animationManager.PlayAnimation(_animations["RunLeft"]);
            }
        }
    }
}
