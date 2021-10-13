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
        private Animation _animation;
        private float _timer;

        public Color Colour = Color.White;

        public AnimationManager()
        {
            _animation = null;
            _timer = 0.0f;
        }
        public void PlayAnimation(Animation animation)
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
            spriteBatch.Draw(_animation.Texture,
                             position,
                             new Rectangle(_animation.CurrentFrame * _animation.FrameWidth,
                                           0,
                                           _animation.FrameWidth,
                                           _animation.FrameHeight),
                             Colour);
        }
    } 
}
