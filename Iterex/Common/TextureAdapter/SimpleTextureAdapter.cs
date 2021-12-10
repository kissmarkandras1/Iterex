using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Common.TextureAdapter
{
    public class SimpleTextureAdapter : ITextureAdapter
    {
        private Texture2D _adaptee;
        private Color[] _textureData;

        public int Width
        {
            get
            {
                return _adaptee.Width;
            }
        }

        public int Height
        {
            get
            {
                return _adaptee.Height;
            }
        }

        public int FrameCount
        {
            get
            {
                return 1;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return _adaptee;
            }
        }

        public int FrameWidth
        {
            get
            {
                return _adaptee.Width;
            }
        }

        public int FrameHeight
        {
            get
            {
                return _adaptee.Height;
            }
        }

        public int CurrentFrame
        {
            get; set;
        }

        public float FrameSpeed
        {
            get; private set;
        }

        public bool IsLooping
        {
            get; private set;
        }

        public Color Colour
        {
            get
            {
                return Color.White;
            }
        }

        public Rectangle ImageBox
        {
            get; private set;
        }

        public string Name
        {
            get; private set;
        }

        public SimpleTextureAdapter() { }

        public SimpleTextureAdapter(Texture2D texture, string name)
        {
            _adaptee = texture;
            Name = name;

            #region FindImageBoundary
            _textureData = new Color[Width * Height];
            Texture.GetData(_textureData);

            int xMin = Width, xMax = 0, yMin = Height, yMax = 0;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y < Height; y++)
                {
                    if (_textureData[y * Width + x].A != 0)
                    {
                        xMin = Math.Min(xMin, x);
                        xMax = Math.Max(xMax, x);
                        yMin = Math.Min(yMin, y);
                        yMax = Math.Max(yMax, y);
                    }
                }
            ImageBox = new Rectangle(xMin, yMin, xMax - xMin + 1, yMax - yMin + 1);
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, bool flip = false)
        {
            SpriteEffects effect = (flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            spriteBatch.Draw(Texture,
                             position,
                             null,
                             Colour,
                             0,
                             new Vector2(0, 0),
                             1f,
                             effect,
                             0f);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float depth, bool flip = false)
        {
            SpriteEffects effect = (flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            spriteBatch.Draw(Texture, 
                             position, 
                             null, 
                             Colour, 
                             0,
                             new Vector2(0, 0),
                             1f, 
                             effect,
                             depth);
        }

        public void DrawBlack(SpriteBatch spriteBatch, Vector2 position, bool flip = false)
        {
            SpriteEffects effect = (flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            spriteBatch.Draw(Texture,
                             position,
                             null,
                             Color.Black,
                             0,
                             new Vector2(0, 0),
                             1f,
                             effect,
                             0f);
        }

        public void DrawBlack(SpriteBatch spriteBatch, Vector2 position, float depth, bool flip = false)
        {
            SpriteEffects effect = (flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            spriteBatch.Draw(Texture,
                             position,
                             null,
                             Color.Black,
                             0,
                             new Vector2(0, 0),
                             1f,
                             effect,
                             depth);
        }

        public void DrawLight(SpriteBatch spriteBatch, Vector2 position, bool flip = false)
        {
            SpriteEffects effect = (flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
            spriteBatch.Draw(Global.glow,
                             position - new Vector2(100,100),
                             null,
                             Global.ambientcolor,
                             0,
                             new Vector2(0, 0),
                             1f,
                             effect,
                             0f);
        }
    }
}
