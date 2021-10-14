using System;
using System.Collections.Generic;
using Iterex.Common;
using Iterex.World.Background;
using Iterex.World.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.World
{
    public class World
    {
        public Tile.Tile[,] Map;
        public List<BackgroundLayer> BackgroundLayers;

        public World()
        {
            //MARK: We create an empty Map based on tileMap's dimensions
            int HEIGHT = Global.TileMap.GetLength(0);
            int WIDTH = Global.TileMap.GetLength(1);
            Map = new Tile.Tile[HEIGHT, WIDTH];

            //Initial the tiles based on tileMap
            for (int i = 0; i < HEIGHT; ++i)
            {
                for (int j = 0; j < WIDTH; ++j)
                {
                    if (Global.TileMap[i, j] == 0)
                        Map[i, j] = new Tile.Tile(null);
                    if (Global.TileMap[i, j] != 0)
                    {
                        int hasUp = 1, hasRight = 1, hasDown = 1, hasLeft = 1;
                        if (i > 0)
                            hasLeft = (Global.TileMap[i - 1, j] == 1) ? 1 : 0;
                        if (i < Global.TileMap.GetLength(0) - 1)
                            hasRight = (Global.TileMap[i + 1, j] == 1) ? 1 : 0;
                        if (j > 0)
                            hasUp = (Global.TileMap[i, j - 1] == 1) ? 1 : 0;
                        if (j < Global.TileMap.GetLength(1) - 1)
                            hasDown = (Global.TileMap[i, j + 1] == 1) ? 1 : 0;
                        //Format Tile_hasUp?_hasRight?_hasDown?_hasLeft?
                        //hasUp/hasRight/hasDown/hasLeft specify whether there is another tile block in that direction
                        //There are corresponding images to each format
                        string textureName = "Tile_" + hasUp + hasRight + hasDown + hasLeft;
                        Map[i, j] = new Tile.Tile(Global.TileTextures[textureName])
                        {
                            Position = new Vector2(i * Global.TILE_SIZE, j * Global.TILE_SIZE),
                            Colour = Color.White,
                            IsSolid = true
                        };
                        Global.Sprites.Add(Map[i, j]);
                    }
                }
            }
        }
       
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                     Map[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}