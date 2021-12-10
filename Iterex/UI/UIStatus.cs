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
            spriteBatch.Draw(Common.Global.UITextures["pixel"].Texture,Area,Color.White);
            spriteBatch.Draw(Common.Global.UITextures["pixel"].Texture, new Rectangle(Area.X+5,Area.Y+5,Area.Width-10-((Area.Width-10)* (1 - maxhp / hp)),Area.Height-10), new Color(255*(uint)(1- maxhp / hp), 255 * (uint)(maxhp / hp),0));
        }

        public override void Clicked() { }
    }
}
