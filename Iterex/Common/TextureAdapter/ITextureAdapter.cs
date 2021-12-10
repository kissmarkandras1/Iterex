using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Common.TextureAdapter
{
    public interface ITextureAdapter
    {
        int Width
        {
            get;
        }

        int Height
        {
            get;
        }

        int FrameWidth
        {
            get;
        }

        int FrameHeight
        {
            get;
        }

        int CurrentFrame
        {
            get; set;
        }
        int FrameCount
        {
            get;
        }

        float FrameSpeed
        {
            get;
        }

        bool IsLooping
        {
            get;
        }

        Texture2D Texture
        {
            get;
        }

        Color Colour
        {
            get;
        }

        Rectangle ImageBox
        {
            get;
        }

        string Name
        {
            get;
        }

        void Draw(SpriteBatch spriteBatch, Vector2 position, bool flip = false);

        void Draw(SpriteBatch spriteBatch, Vector2 position, float depth, bool flip = false);
    }
}
