using Iterex.Common.Animation;
using Iterex.Common.TestingUltilities;
using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Entity
{
    public class AnimatedEntity : Entity
    {
        protected int _preDirection;
        public int Direction { get; protected set; }
        public bool OnGround { get; protected set; }

        public UI.UIStatus HpBar;

        public AnimatedEntity() : base() { }

        public AnimatedEntity(ITextureAdapter texture) 
            : base(texture)
        {

        }

        public AnimatedEntity(Dictionary<string, ITextureAdapter> textures, string firstTexture)
            : base(textures, firstTexture)
        {
            HpBar = new UI.UIStatus(new Rectangle((int)Position.X, (int)Position.Y - 20, 100, 20),1,null);
        }


        protected bool IsDone()
        {
            return _animationManager.IsDone();
        }

        public override void Draw(SpriteBatch spriteBatch, float depth)
        {
            if (_textures != null)
            {
                _animationManager.Draw(spriteBatch, Position, depth, IsFlip);
            }
        }

        public override void DrawBlack(SpriteBatch spriteBatch, float depth)
        {
            if (_textures != null)
            {
                _animationManager.DrawBlack(spriteBatch, Position, depth, IsFlip);
            }
        }

        public override void DrawHp(SpriteBatch spriteBatch)
        {
            HpBar.Area = new Rectangle((int)Position.X, (int)Position.Y - 10, 40, 10);
            HpBar.Draw(spriteBatch, Attributes.HP, Attributes.MaxHP);
        }

    }
}
