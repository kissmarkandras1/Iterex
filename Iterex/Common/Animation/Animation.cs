using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Common.Animation
{
    public class Animation
    {
        public Texture2D Texture { get; private set; }
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public float FrameSpeed { get; set; }
        public bool IsLooping { get; private set; }

        public Animation(Texture2D texture, int frameCount, bool isLooping)
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLooping = isLooping;
            FrameSpeed = 0.1f;
        }

    }
}
