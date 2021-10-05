using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex
{
    public class Entity
    {
        public Vector2 position;
        public Vector2 mapPosition;
        public Vector2 velocity;
        public string texture;
        public Rectangle collisionBox;
        public bool onGround;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Global.entityTextures[texture], position-Global.cameraPosition, Color.White);
        }
    }
}
