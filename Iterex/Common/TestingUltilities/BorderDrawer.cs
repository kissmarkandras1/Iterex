using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Common.TestingUltilities
{
    public class BorderDrawer
    {
        static Texture2D _pointTexture;
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData<Color>(new Color[] { Color.White });
            }

            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth - 1), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth - 1, lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X + rectangle.Width - 1, rectangle.Y, lineWidth, rectangle.Height + lineWidth - 1), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - 1, rectangle.Width + lineWidth - 1, lineWidth), color);
        }
    }
}
