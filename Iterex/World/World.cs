using System;
using System.Collections.Generic;
using Iterex.Common;
using Iterex.Common.TextureAdapter;
using Iterex.Entity;
using Iterex.Entity.Enemy;
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

            heightMap[0] = Global.Random.Next(10, Math.Min(30, height));
            //MARK: Generates peaks
            for (int i = 10; i < width; i += 10)
            {
                heightMap[i] = heightMap[i-10] + Global.Random.Next(-5, 5);
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
                    tileMap[i, j] = (i < 10 || i > width - 10 || j == height - 1 || j > heightMap[i]) ? 1 : 0;

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

            WorldGenerateTrees();
            WorldGenerateEnemies();
            /*
            //Generate trees at tile coordinates
            GenerateTree(10,20);

            //Generate tree with specified height
            GenerateTree(15, 20, 3);
            */
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

        public void DrawBlack(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] != null)
                        Map[i, j].DrawBlack(spriteBatch);
                }
            }
        }

        public void DrawLight(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == null)
                        Map[i, j].DrawLight(spriteBatch);
                }
            }
        }

        //Finds the height of the first tile at a given width
        public int FindSurface(int width)
        {
            for (int i=0;i<Map.GetLength(1);i++)
            {
                if (Map[width,i] != null)
                {
                    //found something that is not air -> it's the surface block
                    return i;
                }
            }
            return -1;  //there is no surface (impossible?)
        }

        //logic for generating trees in the world
        public void WorldGenerateTrees(int spacing = 3)
        {
            int c = 0;
            for (int i = 0; i < Map.GetLength(0); i++)
            {

                if (c == spacing+1)
                {
                    c = 0;
                    int surfaceLevel = FindSurface(i);
                    int h = Global.Random.Next(3,9);
                    GenerateTree(i, surfaceLevel-1, h);    //one above the surface block
                }

                c++;
            }
        }

        public void WorldGenerateEnemies()
        {
            for (int pos = 0; pos < Map.GetLength(0); pos += Global.Random.Next(10, 15))
            {
                int surfaceLevel = FindSurface(pos);

                Global.Entities.Add(GenerateEnemy(new Vector2(pos * Global.TILE_SIZE, (surfaceLevel - 2) * Global.TILE_SIZE)));
            }

        }

        public Entity.Entity GenerateEnemy(Vector2 position)
        {
            Entity.Entity enemy = null;
            int type = Global.Random.Next(1, 3);
            int randomHP = Global.Random.Next(80, 500);
            int randomMP = Global.Random.Next(80, 500);
            int randomDamage = Global.Random.Next(10, 50);

            if (type == 1)
            {
                enemy = new Centipede()
                {
                    Position = position,
                    Velocity = new Vector2(0, 0),
                    Attributes = new EntityAttributes()
                    {
                        MaxHP = randomHP,
                        MaxMP = randomMP,
                        HP = randomHP,
                        MP = randomMP,
                        Damage = randomDamage,
                        DodgeChance = 20,
                        Team = 2,
                        Speed = 15f
                    }
                };
            }
            else if (type == 2)
            {
                enemy = new BigBloated()
                {
                    Position = position,
                    Velocity = new Vector2(0, 0),
                    Attributes = new EntityAttributes()
                    {
                        MaxHP = randomHP,
                        MaxMP = randomMP,
                        HP = randomHP,
                        MP = randomMP,
                        Damage = randomDamage,
                        DodgeChance = 20,
                        Team = 2,
                        Speed = 30f
                    }
                };
            }

            return enemy;
        }

        //generate one tree
        public void GenerateTree(int x, int y, int h = 5)
        {
            //tree doesn't generate if there isn't enough space
            if (y - h < 0) //index out of bounds
            {
                return;
            }

            //generate tree trunk
            Map[x, y] = new Tile.Tile(new Dictionary<string, ITextureAdapter>() { { "tree1", Global.TileTextures["tree1"] } },
                                                  "tree1")
            {
                Position = new Vector2(x * Global.TILE_SIZE, y * Global.TILE_SIZE),
                IsSolid = false
            };

            for (int i=1;i<h-1;i++)
            {
                //generate tree body
                Map[x, y-i] = new Tile.Tile(new Dictionary<string, ITextureAdapter>() { { "tree2", Global.TileTextures["tree2"] } },
                                                      "tree2")
                {
                    Position = new Vector2(x * Global.TILE_SIZE, (y-i) * Global.TILE_SIZE),
                    IsSolid = false
                };
            }

            //generate tree top (I'm not sure if this should be a tile, but if not, the tree has to be able
            //  to reference it when destroyed so the tree could be a different class?

            //the tree3_large image looked okay but is was off-center because it was treated as a tile.
             Map[x, y - h] = new Tile.Tile(new Dictionary<string, ITextureAdapter>() { { "tree3", Global.TileTextures["tree3"] } },
                                                   "tree3")
             {
                 Position = new Vector2(x * Global.TILE_SIZE - 16, (y - h) * Global.TILE_SIZE),
                 IsSolid = false
             };
        }
    }
}