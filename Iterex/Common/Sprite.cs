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
        protected List<ITextureAdapter> _textures;

        public Vector2 Position;
        public bool IsSolid;
        public int CurrentTexture;

        public virtual Rectangle TextureBox
        {
            get
            {
                return new Rectangle((int)Position.X, 
                                     (int)Position.Y,
                                     _textures[CurrentTexture].Width, 
                                     _textures[CurrentTexture].Height);
            }
        }
        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X + _textures[CurrentTexture].ImageBox.X - 1,
                                     (int)Position.Y + _textures[CurrentTexture].ImageBox.Y - 1,
                                     _textures[CurrentTexture].ImageBox.Width,
                                     _textures[CurrentTexture].ImageBox.Height);
            }
        }

        public Sprite(List<ITextureAdapter> textures)
        {
            _textures = textures;
        }

        public virtual void Update(GameTime time, List<Sprite> sprites)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_textures != null)
            {
                //BorderDrawer.DrawRectangle(spriteBatch, CollisionBox, Color.Red, 1);
                _textures[CurrentTexture].Draw(spriteBatch, Position);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, float depth)
        {
            if (_textures != null)
            {
                _textures[CurrentTexture].Draw(spriteBatch, Position, depth);
            }
        }

    }
}
