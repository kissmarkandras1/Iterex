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

namespace Iterex.Common
{
    public class Sprite
    {
        protected Dictionary<string, ITextureAdapter> _textures;
        protected AnimationManager _animationManager;
        protected string _currentTexture;

        public Vector2 Position;
        public bool IsSolid;
        public bool IsFlip = false;

        public virtual Rectangle TextureBox
        {
            get
            {
                return new Rectangle((int)Position.X, 
                                     (int)Position.Y,
                                     _textures[_currentTexture].FrameWidth, 
                                     _textures[_currentTexture].FrameHeight);
            }
        }
        public virtual Rectangle CollisionBox
        {
            get
            {
                int marginLeft = _textures[_currentTexture].ImageBox.X;
                int marginRight = _textures[_currentTexture].FrameWidth - _textures[_currentTexture].ImageBox.Right;

                if (IsFlip)
                    return new Rectangle((int)Position.X + marginRight,
                                     (int)Position.Y + _textures[_currentTexture].ImageBox.Y,
                                     _textures[_currentTexture].ImageBox.Width,
                                     _textures[_currentTexture].ImageBox.Height);

                return new Rectangle((int)Position.X + marginLeft,
                                     (int)Position.Y + _textures[_currentTexture].ImageBox.Y,
                                     _textures[_currentTexture].ImageBox.Width,
                                     _textures[_currentTexture].ImageBox.Height);
            }
        }

        public Sprite() { }

        public Sprite(ITextureAdapter texture)
        {
            _textures = new Dictionary<string, ITextureAdapter>()
            {
                { texture.Name, texture }
            };
            _currentTexture = texture.Name;
            _animationManager = new AnimationManager(_textures[_currentTexture]);
        }
        public Sprite(Dictionary<string, ITextureAdapter> textures, string firstTexture)
        {
            _textures = textures;
            _currentTexture = firstTexture;
            _animationManager = new AnimationManager(_textures[_currentTexture]);
        }

        public virtual void Update(GameTime time, List<Sprite> sprites)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_textures != null)
            {
                //BorderDrawer.DrawRectangle(spriteBatch, TextureBox, Color.Black, 1);
                //BorderDrawer.DrawRectangle(spriteBatch, CollisionBox, Color.Red, 1);
                _animationManager.Draw(spriteBatch, Position, IsFlip);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, float depth)
        {
            if (_textures != null)
            {
                _animationManager.Draw(spriteBatch, Position, depth, IsFlip);
            }
        }

        public void SwitchTexture(string textureName, bool flip = false)
        {
            _currentTexture = textureName;
            IsFlip = flip;
            _animationManager.PlayAnimation(_textures[_currentTexture]);
        }

    }
}
