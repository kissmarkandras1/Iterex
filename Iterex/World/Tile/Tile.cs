using Iterex.Common;
using Iterex.Common.TextureAdapter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.World.Tile
{
    public class Tile : Sprite
    {
        public int Id;
        public int BackgroundId;
        public int Light;
        public Tile(Dictionary<string, ITextureAdapter> textures, string firstTexture)
            : base(textures, firstTexture)
        {

        }
    }
}
