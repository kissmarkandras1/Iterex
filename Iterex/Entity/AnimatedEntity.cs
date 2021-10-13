using Iterex.Common.Animation;
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

        public AnimatedEntity(Texture2D texture, Dictionary<string, Animation> animations)
            : base(texture)
        {
            _animationManager = new AnimationManager();
            _animations = animations;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _animationManager.Draw(spriteBatch, Position);
        }
    }
}
