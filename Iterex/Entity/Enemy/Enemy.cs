using Iterex.Common;
using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Iterex.Entity.Enemy
{
    class Enemy : AnimatedEntity
    {
        private float _timer;

        public float ChangeDirectionTime = 3f;

        public Enemy() : base() { }

        public Enemy(ITextureAdapter texture) 
            : base(texture)
        {

        }

        public Enemy(Dictionary<string, ITextureAdapter> textures, string firstTexture)
            : base(textures, firstTexture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Move(gameTime);
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
                    if (this.IsTouchingTop(sprite, deltaTime))
                        this.OnGround = true;
                    this.Velocity.Y = 0;
                }

            }

            Position += Velocity * deltaTime;
        }

        protected void DetectAttacking(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!this.Attributes.IsAttacking)
            {
                if ((Direction == 1 && this.IsTouchingLeft(Global.Player, 10)) ||
                    (Direction == 0 && this.IsTouchingRight(Global.Player, 10)))
                {
                    DealDamage(Global.Player);
                    this.Attributes.StartAttacking = true;
                }
            }
            
        }

        protected void Move(GameTime gameTime)
        {
            _preDirection = Direction;
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_timer >= ChangeDirectionTime)
            {
                _timer = 0;
                this.Velocity.X = 0;
                Direction = Direction == 1 ? -1 : 1;
            }

            Velocity += EntityConfiguration.AccelerationX * Attributes.Speed * Direction;
            Velocity += EntityConfiguration.Gravity;

            //Limit the speed
            if (Velocity.X > EntityConfiguration.MaxSpeed.X * Attributes.Speed)
                Velocity.X = EntityConfiguration.MaxSpeed.X * Attributes.Speed;
            if (Velocity.X < -EntityConfiguration.MaxSpeed.X * Attributes.Speed)
                Velocity.X = -EntityConfiguration.MaxSpeed.X * Attributes.Speed;
            if (Velocity.Y > EntityConfiguration.MaxSpeed.Y * Attributes.Speed)
                Velocity.Y = EntityConfiguration.MaxSpeed.Y * Attributes.Speed;
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

            if (this.Attributes.StartDeadAnimation || this.Attributes.IsPlayingDeadAnimation)
            {
                if (this.Attributes.StartDeadAnimation)
                {
                    if (Direction == 1)
                        SwitchTexture("Dead", true);
                    else
                        SwitchTexture("Dead");

                    this.Attributes.StartDeadAnimation = false;
                    this.Attributes.IsPlayingDeadAnimation = true;
                }

                if (this.Attributes.IsPlayingDeadAnimation && IsDone())
                {
                    this.Attributes.IsPlayingDeadAnimation = false;
                    this.Attributes.IsRemovable = true; 
                }

                this.Attributes.IsAttacking = false;
                this.Attributes.StartAttacking = false;
            }
            else if (this.Attributes.StartReceivingDamage || this.Attributes.IsReceivingDamage)
            {
                if (this.Attributes.StartReceivingDamage)
                {
                    if (Direction == 1)
                        SwitchTexture("Hurt", true);
                    else
                        SwitchTexture("Hurt");

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
                    int attackType = random.Next(1, 5);

                    if (Direction == 1)
                        SwitchTexture("Attack" + attackType, true);
                    else
                        SwitchTexture("Attack" + attackType);

                    this.Attributes.StartAttacking = false;
                    this.Attributes.IsAttacking = true;
                }

                if (this.Attributes.IsAttacking && IsDone())
                    this.Attributes.IsAttacking = false;
            }
            else if (Velocity.X > 0)
                SwitchTexture("Run", true);
            else if (Velocity.X < 0)
                SwitchTexture("Run");
            else
            {
                if (Direction > 0)
                    SwitchTexture("Idle", true);
                else
                    SwitchTexture("Idle");
            }

        }
    }
}
