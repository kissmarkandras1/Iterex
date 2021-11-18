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
            DetectAttacking(gameTime);
            SetAnimation();
            _animationManager.Update(gameTime);

            //Check for collision
            foreach (Sprite sprite in GetSurroundingTiles())
            {
                if (sprite == this || !sprite.IsSolid)
                    continue;

                if ((this.Velocity.X * deltaTime > 0 && this.IsTouchingLeft(sprite, this.Velocity.X * deltaTime)) ||
                    (this.Velocity.X * deltaTime < 0 && this.IsTouchingRight(sprite, this.Velocity.X * deltaTime)))
                {
                    this.Velocity.X = 0;
                }
                if ((this.Velocity.Y * deltaTime > 0 && this.IsTouchingTop(sprite, this.Velocity.Y * deltaTime)) ||
                    (this.Velocity.Y * deltaTime < 0 && this.IsTouchingBottom(sprite, this.Velocity.Y * deltaTime)))
                {
                    if (this.IsTouchingTop(sprite, this.Velocity.Y * deltaTime))
                        this.OnGround = true;
                    this.Velocity.Y = 0;
                }
                
            }

            Position += Velocity * deltaTime;
        }

        private void DetectAttacking(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Global.KeyboardState.IsKeyDown(Keys.Space))
            {
                if (!this.Attributes.IsAttacking )
                {
                    this.Attributes.StartAttacking = true;
                    Velocity.X += Direction * EntityConfiguration.MaxSpeed.X * Attributes.Speed;

                    foreach (Entity entity in Global.Entities)
                    {
                        if (entity is Enemy.Enemy)
                        {
                            if (this.CollisionBox.Intersects(entity.CollisionBox))
                            {
                                DealDamage(entity);
                            }
                        }
                    }
                }
            }
        }

        private void Move()
        {
            //MARK: Is pressing A or D or not
            _preDirection = Direction;
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

            if (Velocity.X > 0)
                Direction = 1;
            else if (Velocity.X < 0)
                Direction = -1;
        }

        protected void SetAnimation()
        {
            //Adjust position after turning
            if (_preDirection != Direction)
            {
                if (Direction == 1)
                    Position.X += TextureBox.Width / 2;
                else
                    Position.X -= TextureBox.Width / 2;
            }

            if (IsDead())
            {
                if (Direction == 1)
                    SwitchTexture("Dead");
                else
                    SwitchTexture("Dead", true);

                this.Attributes.IsAttacking = false;
                this.Attributes.StartAttacking = false;
            }
            else if (this.Attributes.StartReceivingDamage || this.Attributes.IsReceivingDamage)
            {
                if (this.Attributes.StartReceivingDamage)
                {
                    if (Direction == 1)
                        SwitchTexture("Hurt");
                    else
                        SwitchTexture("Hurt", true);

                    this.Attributes.StartReceivingDamage = false;
                    this.Attributes.IsReceivingDamage = true;
                }

                if (this.Attributes.IsReceivingDamage && IsDone())
                {
                    this.Attributes.IsReceivingDamage = false;
                }

                this.Attributes.IsAttacking = false;
                this.Attributes.StartAttacking = false;
            }
            else if (this.Attributes.StartAttacking || this.Attributes.IsAttacking)
            {
                if (this.Attributes.StartAttacking)
                {
                    Random random = new Random();
                    int attackType = random.Next(1, 4);

                    if (Direction == 1)
                        SwitchTexture("Attack" + attackType);
                    else
                        SwitchTexture("Attack" + attackType, true);

                    this.Attributes.StartAttacking = false;
                    this.Attributes.IsAttacking = true;
                }

                if (this.Attributes.IsAttacking && IsDone())
                    this.Attributes.IsAttacking = false;
            }
            else if (!OnGround)
            {
                if (Direction == 1)
                    SwitchTexture("Jump");
                else
                    SwitchTexture("Jump", true);
            }
            else if (Velocity.X > 0)
                SwitchTexture("Run");
            else if (Velocity.X < 0)
                SwitchTexture("Run", true);
            else
            {
                if (Direction > 0)
                    SwitchTexture("Idle");
                else
                    SwitchTexture("Idle", true);
            }

        }
    }
}
