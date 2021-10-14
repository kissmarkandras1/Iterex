using Microsoft.Xna.Framework;
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

        public readonly Color[] TextureData;
        public readonly Dictionary<int, Color[]> FramesTextureData;
        public Rectangle ImageBox;

        public Animation(Texture2D texture, int frameCount, bool isLooping)
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLooping = isLooping;
            FrameSpeed = 0.1f;

            TextureData = new Color[Texture.Width * Texture.Height];
            Texture.GetData(TextureData);

            FramesTextureData = new Dictionary<int, Color[]>();
            InitializeFramesTextureData();

            InitializeFramesImageBox();
        }

        private void InitializeFramesTextureData()
        {
            for (int frame = 0; frame < FrameCount; frame++)
            {
                FramesTextureData.Add(frame, new Color[FrameWidth * FrameHeight]);
                for (int x = 0; x < FrameWidth; x++)
                    for (int y = 0; y < FrameHeight; y++)
                    {
                        int actualX = frame * FrameWidth + x;
                        FramesTextureData[frame][y * FrameWidth + x] = TextureData[y * Texture.Width + actualX];
                    }
            }
        }

        private void InitializeFramesImageBox()
        {
            int xMin = FrameWidth, xMax = 0, yMin = FrameHeight, yMax = 0;
            for (int frame = 0; frame < FrameCount; frame++)
            {
                for (int x = 0; x < FrameWidth; x++) 
                    for (int y = 0; y < FrameHeight; y++)
                    {
                        if (FramesTextureData[frame][y * FrameWidth + x].A != 0)
                        {
                            xMin = Math.Min(xMin, x);
                            xMax = Math.Max(xMax, x);
                            yMin = Math.Min(yMin, y);
                            yMax = Math.Max(yMax, y);
                        }
                    }
            }
            ImageBox = new Rectangle(xMin, yMin, xMax - xMin + 1, yMax - yMin + 1);
        }

        public Color[] GetCurrentFrameTextureData()
        {
            return FramesTextureData[CurrentFrame];
        }

        public Rectangle GetCurrentFrameImageBox()
        {
            return ImageBox;
        }
    }
}
