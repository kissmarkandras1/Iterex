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

        public Player() : base() { }

        public Player(ITextureAdapter texture)
            : base(texture)
        {

        }

        public Player(Dictionary<string, ITextureAdapter> textureAdapter, string firstTexture)
            : base(textureAdapter, firstTexture)
        {
            OnGround = false;
            Direction = 1;
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
                Velocity += EntityConfiguration.AccelerationX * Attributes.Speed;
                HorizontalMoveRequest = true;
            }
            if (Global.KeyboardState.IsKeyDown(Keys.A))
            {
                if (Velocity.X > 0)
                    Velocity.X = 0;
                Velocity -= EntityConfiguration.AccelerationX * Attributes.Speed;
                HorizontalMoveRequest = true;
            }
            //MARK: Negative Y is up
            if (Global.KeyboardState.IsKeyDown(Keys.W) && OnGround)
            {
                Velocity += -EntityConfiguration.InitialJumpSpeed * Attributes.Speed;
                OnGround = false;
            }

            //Apply gravity force
            Velocity += EntityConfiguration.Gravity;

            //Limit the speed
            if (Velocity.X > EntityConfiguration.MaxSpeed.X * Attributes.Speed)
                Velocity.X = EntityConfiguration.MaxSpeed.X * Attributes.Speed;
            if (Velocity.X < -EntityConfiguration.MaxSpeed.X * Attributes.Speed)
                Velocity.X = -EntityConfiguration.MaxSpeed.X * Attributes.Speed;
            if (Velocity.Y > EntityConfiguration.MaxSpeed.Y * Attributes.Speed)
                Velocity.Y = EntityConfiguration.MaxSpeed.Y * Attributes.Speed;

            //Decelerate the horizontal speed when release A/D buttons
            if (!HorizontalMoveRequest)
            {
                if (Velocity.X > 0)
                    Velocity.X -= Math.Min(EntityConfiguration.DecelerationX.X * Attributes.Speed, Velocity.X);
                if (Velocity.X < 0)
                    Velocity.X += Math.Min(EntityConfiguration.DecelerationX.X * Attributes.Speed, -Velocity.X);
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
                    SwitchTexture("JumpRight");
                else
                    SwitchTexture("JumpLeft");
            }
            else if (Velocity.X > 0) 
                SwitchTexture("RunRight");
            else if (Velocity.X < 0)
                SwitchTexture("RunLeft");
            else
            {
                if (Direction > 0)
                    SwitchTexture("IdleRight");
                else
                    SwitchTexture("IdleLeft");
            }
            
        }
    }
}
