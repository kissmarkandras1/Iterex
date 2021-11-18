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
        private ITextureAdapter _texture;
        private float _timer;
        private bool _isDone;
        public AnimationManager(ITextureAdapter texture)
        {
            _texture = texture;
            _timer = 0.0f;
            _isDone = false;
        }
        public void PlayAnimation(ITextureAdapter texture)
        {
            //Already play this animation
            if (_texture == texture)
                return;
            _texture = texture;
            _timer = 0.0f;
            _isDone = false;
        }

        public void StopPlaying()
        {
            _texture.CurrentFrame = 0;
            _timer = 0.0f;
        }

        public void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_isDone)
                return;

            if (_timer > _texture.FrameSpeed)
            {
                _timer = 0;
                _texture.CurrentFrame++;
                if (_texture.CurrentFrame >= _texture.FrameCount)
                {
                    _texture.CurrentFrame = 0;
                    if (_texture.IsLooping == false)
                        _isDone = true;
                }
            }
        }

        public bool IsDone()
        {
            return _isDone;
        }

        public ITextureAdapter GetTexture()
        {
            return _texture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, bool flip = false)
        {
            _texture.Draw(spriteBatch, position, flip);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float depth, bool flip = false)
        {
            _texture.Draw(spriteBatch, position, depth, flip);
        }

    } 
}
