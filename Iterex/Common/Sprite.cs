using Iterex.Common.TestingUltilities;
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
        protected Texture2D _texture;

        public Vector2 Position;
        public Color Colour = Color.White;
        public bool IsSolid;
        public readonly Color[] TextureData;
        public readonly Rectangle ImageBox;

        public virtual Rectangle TextureBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, GetWidth(), GetHeight());
            }
        }
        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X + GetImageBox().X - 1, (int)Position.Y + GetImageBox().Y - 1, GetImageBox().Width, GetImageBox().Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            if (texture == null)
                return;
            _texture = texture;
            TextureData = new Color[_texture.Width * _texture.Height];
            _texture.GetData(TextureData);

            int xMin = _texture.Width, xMax = 0, yMin = _texture.Height, yMax = 0;
            for (int x = 0; x < _texture.Width; x++)
                for (int y = 0; y < _texture.Height; y++)
                {
                    if (TextureData[y * _texture.Width + x].A != 0)
                    {
                        xMin = Math.Min(xMin, x);
                        xMax = Math.Max(xMax, x);
                        yMin = Math.Min(yMin, y);
                        yMax = Math.Max(yMax, y);
                    }
                }
            ImageBox = new Rectangle(xMin, yMin, xMax - xMin + 1, yMax - yMin + 1);
        }

        public virtual void Update(GameTime time, List<Sprite> sprites)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
            {
                //BorderDrawer.DrawRectangle(spriteBatch, CollisionBox, Color.Red, 1);
                spriteBatch.Draw(_texture, Position, Colour);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, float depth)
        {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, Position, null, Colour, 0, new Vector2(0, 0), 1f, SpriteEffects.None, depth);
            }
        }

        public virtual Color[] GetTextureData()
        {
            return TextureData;
        }

        public virtual Rectangle GetImageBox()
        {
            return ImageBox;
        }

        public virtual int GetWidth()
        {
            return _texture.Width;
        }

        public virtual int GetHeight()
        {
            return _texture.Height;
        }
    }
}
