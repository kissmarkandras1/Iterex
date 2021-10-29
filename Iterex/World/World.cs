using System;
using System.Collections.Generic;
using Iterex.Common;
using Iterex.Common.TextureAdapter;
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

            int[,] tileMap = GenerateTilemap(width, height);

            GenerateActualTiles(tileMap, width, height);
        }

        private int[,] GenerateTilemap(int width, int height)
        {
            //MARK: Tilemap generation
            int[] heightMap = new int[width];
            int[,] tileMap = new int[width, height];

            //MARK: Generates peaks
            for (int i = 0; i < width; i += 10)
            {
                heightMap[i] = Global.Random.Next(0, Math.Min(height, 30));
            }

            //MARK: And connects them
            for (int i = 0; i < width; i++)
            {
                int prevTen = (i / 10) * 10;
                int nextTen = ((i / 10) * 10 + 10) > (width - 1) ? (width - 1) : ((i / 10) * 10 + 10);
                int prevDist = i - prevTen;
                int nextDist = nextTen - i;
                heightMap[i] = (nextDist * heightMap[prevTen] + prevDist * heightMap[nextTen]) / (prevDist + nextDist);
            }

            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                    tileMap[i, j] = (i == 0 || i == width - 1 || j == height - 1 || j > heightMap[i]) ? 1 : 0;

            return tileMap;
        }
       
        private void GenerateActualTiles(int[,] tileMap, int width, int height)
        {
            //Initial the tiles based on tilemap
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
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

                        // Format Tile_hasUp?_hasRight?_hasDown?_hasLeft?
                        // hasUp/hasRight/hasDown/hasLeft specify whether there is another tile block in that direction
                        // There are corresponding images to each format

                        string textureName = "Tile_" + hasUp + hasRight + hasDown + hasLeft;
                        Map[i, j] = new Tile.Tile(new Dictionary<string, ITextureAdapter>() { { textureName, Global.TileTextures[textureName] } }, 
                                                  textureName)
                        {
                            Position = new Vector2(i * Global.TILE_SIZE, j * Global.TILE_SIZE),
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
                    if (Map[i, j] != null)
                        Map[i, j].Draw(spriteBatch);
                }
            }
        }
    }
}