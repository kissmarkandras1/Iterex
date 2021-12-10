using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iterex.UI
{
    public class UIStatus : UIElement
    {
        public UIStatus(Rectangle Areain, int LayerIn, UIElement Parentin)
        {
            Area = Areain;
            Clickable = false;
            Layer = LayerIn;
            Parent = Parentin;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Draw(SpriteBatch spriteBatch, int hp, int maxhp)
        {
            float hprate = (float)hp / (float)maxhp;
            float hprateinv = 1.0f - hprate;
            spriteBatch.Draw(Common.Global.UITextures["pixel"].Texture,Area,Color.Black);
            spriteBatch.Draw(Common.Global.UITextures["pixel"].Texture, new Rectangle(Area.X+2,Area.Y+2, (int)((Area.Width-4)*hprate), Area.Height-4), new Color((uint)(255.0*hprateinv), (uint)(255.0*hprate), 0));
        }

        public override void Clicked() { }
    }
}
