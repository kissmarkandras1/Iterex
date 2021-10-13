using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterex.Common
{
    public class Sprite
    {
        protected Texture2D _texture;

        public Vector2 Position;
        public Vector2 MapPosition;
        public Color Colour = Color.White;
        public bool IsSolid;

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        public virtual void Update(GameTime time, List<Sprite> sprites)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position - Global.CameraPosition, Colour);
        }

    }
}
