using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex
{
    public class World
    {
        public Tile[,] map;

        public World()
        {
            //MARK: We create an empty map
            map = new Tile[100, 50];

            //MARK: We fill up the map with random blocks, 5% chance of a solid block
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (Global.random.Next(0, 20) < 1)
                        map[i, j] = new Tile(1);
                    else
                        map[i, j] = new Tile(0);
                }
            }

            //MARK: We fill the bottom row with tiles of id 1
            for (int i = 0; i < map.GetLength(0); i++)
            {
                map[i, 49] = new Tile(1);
            }
        }
        public bool GetSolid(Vector2 mapPosition)
        {
            //MARK: We request if a particular tile has id 0 (air) or not, false means it is passable
            if (mapPosition.X > 0 && mapPosition.X < map.GetLength(0) && mapPosition.Y > 0 && mapPosition.Y < map.GetLength(1))
                return map[(int)mapPosition.X, (int)mapPosition.Y].id != 0;
            else
                return true;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i,j].id>0)
                    {
                        spriteBatch.Draw(Global.tileTextures["grass"],new Vector2(i*40,j*40)-Global.cameraPosition,Color.White);
                    }
                }
            }
        }
    }
}