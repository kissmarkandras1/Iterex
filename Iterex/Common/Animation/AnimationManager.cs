using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Common.Animation
{
    public class AnimationManager
    {
        private AnimationTextureAdapter _animation;
        private float _timer;
        public AnimationManager()
        {
            _animation = null;
            _timer = 0.0f;
        }
        public void PlayAnimation(AnimationTextureAdapter animation)
        {
            //Already play this animation
            if (_animation == animation)
                return;
            _animation = animation;
            _timer = 0.0f;
        }

        public void StopPlaying()
        {
            _animation.CurrentFrame = 0;
            _timer = 0.0f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animation.FrameSpeed)
            {
                _timer = 0;
                _animation.CurrentFrame++;
                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            _animation.Draw(spriteBatch, position);
        }

    } 
}
