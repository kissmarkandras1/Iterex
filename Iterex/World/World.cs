using System;
using System.Collections.Generic;
using Iterex.World.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.World
{
    public class World
    {
        public Tile.Tile[,] map;

        public World()
        {
            //MARK: We create an empty map
            int HEIGHT = Global.tileMap.GetLength(0);
            int WIDTH = Global.tileMap.GetLength(1);
            map = new Tile.Tile[HEIGHT, WIDTH];

            for (int i = 0; i < HEIGHT; ++i)
            {
                for (int j = 0; j < WIDTH; ++j)
                {
                    if (Global.tileMap[i, j] == 0)
                        map[i, j] = new Tile.Tile("empty");
                    if (Global.tileMap[i, j] != 0)
                    {
                        int hasUp = 1, hasRight = 1, hasDown = 1, hasLeft = 1;
                        if (i > 0)
                            hasLeft = (Global.tileMap[i - 1, j] == 1) ? 1 : 0;
                        if (i < Global.tileMap.GetLength(0) - 1)
                            hasRight = (Global.tileMap[i + 1, j] == 1) ? 1 : 0;
                        if (j > 0)
                            hasUp = (Global.tileMap[i, j - 1] == 1) ? 1 : 0;
                        if (j < Global.tileMap.GetLength(1) - 1)
                            hasDown = (Global.tileMap[i, j + 1] == 1) ? 1 : 0;
                        string textureName = "Tile_" + hasUp + hasRight + hasDown + hasLeft;
                        map[i, j] = new Tile.Tile(textureName);
                    }
                }
            }
        }
        public bool GetSolid(Vector2 mapPosition)
        {
            //MARK: We request if a particular tile has id 0 (air) or not, false means it is passable
            if (mapPosition.X > 0 && mapPosition.X < map.GetLength(0) && mapPosition.Y > 0 && mapPosition.Y < map.GetLength(1))
                return map[(int)mapPosition.X, (int)mapPosition.Y].GetType() != "empty";
            else
                return true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j].GetType() != "empty")
                    {
                        spriteBatch.Draw(map[i, j].GetTexture(), new Vector2(i * Global.TILE_SIZE, j * Global.TILE_SIZE) - Global.cameraPosition, Color.White);
                    }
                }
            }
        }
    }
}