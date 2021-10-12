using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.World.Tile
{
    public class Tile
    {

        private Texture2D _texture;
        private string _type;

        public Tile(string type)
        {
            if (type != "empty")
                _texture = Global.tileTextures[type];
            else
                _texture = null;
            _type = type;
        }

        public Texture2D GetTexture()
        {
            return _texture;
        }

        public string GetType()
        {
            return _type;
        }

    }
}
