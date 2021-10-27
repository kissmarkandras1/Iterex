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
        public Tile.Tile[,] BackgroundMap;
        public List<BackgroundLayer> BackgroundLayers;

        public World(int width, int height)
        {
            //MARK: We create an empty Map based on tileMap's dimensions
            Map = new Tile.Tile[width, height];

            //MARK: Tilemap generation
            int[] heightMap = new int[width];
            int[,] tileMap = new int[width, height];

            //MARK: Generates peaks
            for (int i = 0; i < width; i += 10)
            {
                heightMap[i] = Global.Random.Next(0, Math.Min(height, 30));
            }

            //MARK: And connects them
            for (int i = 0; i < width; i ++)
            {
                int prevTen = (i/10)*10;
                int nextTen = ((i/10)*10+10)>(width-1)? (width-1):((i/10)*10+10);
                int prevDist = i-prevTen;
                int nextDist = nextTen-i;
                heightMap[i] = (nextDist * heightMap[prevTen] + prevDist * heightMap[nextTen])/(prevDist+nextDist);
            }

            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                    tileMap[i, j] = (i == 0 || i == width - 1 || j == height - 1 || j > heightMap[i]) ? 1 : 0;

            //Initial the tiles based on tilemap
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    if (tileMap[i, j] == 0)
                        Map[i, j] = new Tile.Tile(null);
                    if (tileMap[i, j] != 0)
                    {
                        int hasUp = 1, hasRight = 1, hasDown = 1, hasLeft = 1;
                        if (i > 0)
                            hasLeft = (tileMap[i - 1, j] == 1) ? 1 : 0;
                        if (i < tileMap.GetLength(0) - 1)
                            hasRight = (tileMap[i + 1, j] == 1) ? 1 : 0;
                        if (j > 0)
                            hasUp = (tileMap[i, j - 1] == 1) ? 1 : 0;
                        if (j < tileMap.GetLength(1) - 1)
                            hasDown = (tileMap[i, j + 1] == 1) ? 1 : 0;
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