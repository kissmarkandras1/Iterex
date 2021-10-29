using System;
using System.Collections.Generic;
using Iterex.Common.TextureAdapter;
using Iterex.Entity.Player;
using Iterex.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.Common
{
    public static class Global
    {

        public static readonly int TILE_SIZE = 32;
        public static readonly int INF = 1000000000;

        public static KeyboardState KeyboardState;
        public static MouseState MouseState;
        public static World.World ActiveWorld;
        public static ParallaxBackground ParralexBackground;
        public static List<Sprite> Sprites;
        public static Dictionary<string, ITextureAdapter> BackgroundTextures;
        public static Dictionary<string, ITextureAdapter> TileTextures;
        public static Dictionary<string, ITextureAdapter> EntityTextures;
        public static Dictionary<string, ITextureAdapter> AnimatedEntityTextures;
        public static Player Player;
        public static Random Random = new Random();
        public static List<Entity.Entity> Entities;
    }
}
