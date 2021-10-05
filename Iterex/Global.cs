using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex
{
    public static class Global
    {
        public static KeyboardState keyboardState;
        public static MouseState mouseState;
        public static World activeWorld;
        public static Vector2 cameraPosition;
        public static Dictionary<string, Texture2D> tileTextures;
        public static Dictionary<string, Texture2D> entityTextures;
        public static Player player;
        public static Random random = new Random();
    }
}
