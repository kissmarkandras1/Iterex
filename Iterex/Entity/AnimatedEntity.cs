using Iterex.Common.Animation;
using Iterex.Common.TestingUltilities;
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
        protected AnimationManager _animationManager;
        protected Dictionary<string, Animation> _animations;
        
        public int Direction { get; set; }

        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X + GetImageBox().X - 1, 
                                     (int)Position.Y + GetImageBox().Y - 1, 
                                     GetImageBox().Width, 
                                     GetImageBox().Height);
            }
        }

        public AnimatedEntity(Texture2D texture, Dictionary<string, Animation> animations)
            : base(texture)
        {
            _animationManager = new AnimationManager();
            _animations = animations;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //BorderDrawer.DrawRectangle(spriteBatch, CollisionBox, Color.Black, 1);
            //BorderDrawer.DrawRectangle(spriteBatch, TextureBox, Color.White, 1);
            _animationManager.Draw(spriteBatch, Position);
        }

        public override Color[] GetTextureData()
        {
            return _animationManager.GetTextureData();
        }

        public override Rectangle GetImageBox()
        {
            return _animationManager.GetImageBox();
        }

        public override int GetWidth()
        {
            return _animationManager.GetWidth();
        }

        public override int GetHeight()
        {
            return _animationManager.GetHeight();
        }
    }
}
