using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Iterex.UI
{
    public abstract class UIElement
    {
        public int Layer;
        public bool Clickable;
        public Rectangle Area;
        public UIElement Parent;
        public List<UIElement> Children;

        public UIElement()
        {

        }

        public UIElement(Rectangle Areain, bool Clickablein, int LayerIn, UIElement Parentin)
        {
            Area = Areain;
            Clickable = Clickablein;
            Layer = LayerIn;
            Parent = Parentin;
        }

        public void Click()
        {
            UIElement toClick = null;
            int maxlayer = 0;
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i].Layer >= maxlayer && Children[i].Clickable)
                {
                    maxlayer = Children[i].Layer;
                    toClick = Children[i];
                }
            }
            if (toClick == null)
                this.Clicked();
        }

        public abstract void Clicked();

        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
