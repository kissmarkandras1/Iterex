using Iterex.Common;
using Iterex.World.Background;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.World
{
    public class ParallaxBackground
    {

        public List<BackgroundLayer> BackgroundLayers;

        public ParallaxBackground()
        {

            BackgroundLayers = new List<BackgroundLayer>()
            {
                new BackgroundLayer(Global.BackgroundTextures["Layer0"], 10f, 0.1f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["Layer1"], 30f, 0.7f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["Layer2"], 35f, 0.75f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["Layer3"], 60f, 0.8f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["Layer4"], 35f, 0.9f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["Layer5"], 80f, 0.99f, false, 3)
            };
        }

        public void Update(GameTime gameTime)
        {
            foreach (BackgroundLayer backgroundLayer in BackgroundLayers)
            {
                backgroundLayer.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(BackgroundLayer backgroundLayer in BackgroundLayers)
            {
                backgroundLayer.Draw(spriteBatch);
            }
        }
    }
}
