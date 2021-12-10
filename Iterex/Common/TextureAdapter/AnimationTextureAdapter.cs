using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Common.TextureAdapter
{
    public class AnimationTextureAdapter : ITextureAdapter
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
            get; private set;
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
                return _adaptee.Width / FrameCount;
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
            get;
            private set;
        }

        public AnimationTextureAdapter() { }

        public AnimationTextureAdapter(Texture2D texture, string name, int frameCount, float frameSpeed, bool isLooping)
        {
            _adaptee = texture;
            Name = name;
            FrameCount = frameCount;
            FrameSpeed = frameSpeed;
            IsLooping = isLooping;
            CurrentFrame = 0;

            #region FindImageBoundary
            _textureData = new Color[Width * Height];
            Texture.GetData(_textureData);

            int xMin = FrameWidth, xMax = 0, yMin = FrameHeight, yMax = 0;
            for (int frame = 0; frame < FrameCount; frame++)
            {
                for (int x = 0; x < FrameWidth; x++)
                    for (int y = 0; y < FrameHeight; y++)
                    {
                        int actualX = frame * FrameWidth + x;
                        if (_textureData[y * Width + actualX].A != 0)
                        {
                            xMin = Math.Min(xMin, x);
                            xMax = Math.Max(xMax, x);
                            yMin = Math.Min(yMin, y);
                            yMax = Math.Max(yMax, y);
                        }
                    }
            }
            ImageBox = new Rectangle(xMin, yMin, xMax - xMin, yMax - yMin);
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, bool flip = false)
        {
            SpriteEffects effect = flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(Texture,
                             position,
                             new Rectangle(CurrentFrame * FrameWidth,
                                           0,
                                           FrameWidth,
                                           FrameHeight),
                             Colour,
                             0,
                             new Vector2(0, 0),
                             1f,
                             effect,
                             0f);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float depth, bool flip = false)
        {
            SpriteEffects effect = flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(Texture,
                             position,
                             new Rectangle(CurrentFrame * FrameWidth,
                                           0,
                                           FrameWidth,
                                           FrameHeight),
                             Colour,
                             0,
                             new Vector2(0, 0),
                             1f,
                             effect,
                             depth);
        }
    }
}
