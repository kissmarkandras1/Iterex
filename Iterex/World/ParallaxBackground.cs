using Iterex.Common;
using Iterex.Common.TextureAdapter;
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
                new BackgroundLayer(Global.BackgroundTextures["layer0"], 10f, 0.1f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["layer1"], 30f, 0.7f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["layer2"], 35f, 0.75f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["layer3"], 60f, 0.8f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["layer4"], 35f, 0.9f, false, 3),
                new BackgroundLayer(Global.BackgroundTextures["layer5"], 80f, 0.99f, false, 3)
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
