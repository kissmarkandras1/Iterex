using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Common
{
    class Camera
    {
        public float ScaleRatio { private get; set;  }
        public Matrix TransformMatrix { get; private set; }

        public void Follow (Sprite sprite)
        {
            Matrix translateToOrigin = Matrix.CreateTranslation((-sprite.CollisionBox.X - sprite.CollisionBox.Width / 2) * ScaleRatio ,
                                                                (-sprite.CollisionBox.Y - sprite.CollisionBox.Height / 2) * ScaleRatio,
                                                                0);

            Matrix translateToCenter = Matrix.CreateTranslation(Game1.ScreenWidth / 2,
                                                                Game1.ScreenHeight / 2,
                                                                0);

            Matrix scale = Matrix.CreateScale(ScaleRatio);

            TransformMatrix = scale * translateToOrigin * translateToCenter;
        }
    }
}
