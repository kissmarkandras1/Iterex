using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex.Entity.Player
{
    public class Player : Entity
    {
        public Player()
        {
            position = new Vector2(0, 3 * Global.TILE_SIZE);
            mapPosition = new Vector2(0, 3);
            velocity = new Vector2(0, 0);
            texture = "player";
            collisionBox = new Rectangle((int)position.X, (int)position.Y, (int)Global.entityTextures[texture].Width, (int)Global.entityTextures[texture].Height);
            onGround = false;
        }
        public void Update(float deltaTime)
        {
            //MARK: Reading inputs
            velocity.X = 0;

            if (Global.keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X = 4 * Global.TILE_SIZE;
            }
            if (Global.keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X = -4 * Global.TILE_SIZE;
            }
            //MARK: Negative Y is up
            if (Global.keyboardState.IsKeyDown(Keys.W) && onGround)
            {
                velocity.Y = -4 * Global.TILE_SIZE;
                onGround = false;
            }

            //MARK: Seeing where the player will end up after applying the speed for this frame, probably going to make it general to other entities later
            Vector2 newPosition = position + velocity * deltaTime;
            
            //MARK: Corners to check for collision
            Vector2[] checkCorners = new Vector2[4];
            checkCorners[0] = newPosition / Global.TILE_SIZE;
            checkCorners[1] = new Vector2(newPosition.X + collisionBox.Width, newPosition.Y) / Global.TILE_SIZE;
            checkCorners[2] = new Vector2(newPosition.X, newPosition.Y + collisionBox.Height) / Global.TILE_SIZE;
            checkCorners[3] = new Vector2(newPosition.X + collisionBox.Width, newPosition.Y + collisionBox.Height) / Global.TILE_SIZE;

            //MARK: Checking collision in the blocks along the player's top side
            for (int i = (int)checkCorners[0].X; i < (int)checkCorners[1].X; i++)
            {
                Vector2 tileToCheck = new Vector2(i, checkCorners[0].Y);
                if (Global.activeWorld.GetSolid(tileToCheck))
                {
                    //MARK: If the player would overshoot and collide, the player ends up adjacent to the tile, later will be done with adjusting a vector between current and future position
                    velocity.Y = 0;
                    newPosition.Y = ((int)checkCorners[0].Y + 1) * Global.TILE_SIZE;
                    break;
                }
            }

            //MARK: Checking collision in the blocks along the player's bottom side
            bool bottomCollision = false;
            for (int i = (int)checkCorners[2].X; i < (int)checkCorners[3].X; i++)
            {
                Vector2 tileToCheck = new Vector2(i, checkCorners[2].Y);
                if (Global.activeWorld.GetSolid(tileToCheck))
                {
                    //MARK: If the player would overshoot and collide, the player ends up adjacent to the tile, later will be done with adjusting a vector between current and future position
                    velocity.Y = 0;
                    newPosition.Y = ((int)checkCorners[0].Y) * Global.TILE_SIZE;
                    onGround = true;
                    break;
                }
            }
            //MARK: Gravity
            if (!bottomCollision)
            {
                velocity.Y += 10 * Global.TILE_SIZE * deltaTime;
            }


            //MARK: Checking collision in the blocks along the player's left side
            for (int i = (int)checkCorners[0].Y; i < (int)checkCorners[2].Y; i++)
            {
                Vector2 tileToCheck = new Vector2(checkCorners[0].X, i);
                if (Global.activeWorld.GetSolid(tileToCheck))
                {
                    //MARK: If the player would overshoot and collide, the player ends up adjacent to the tile, later will be done with adjusting a vector between current and future position
                    velocity.X = 0;
                    newPosition.X = ((int)checkCorners[0].X + 1) * Global.TILE_SIZE;
                    break;
                }
            }


            //MARK: Checking collision in the blocks along the player's right side
            for (int i = (int)checkCorners[1].Y; i < (int)checkCorners[3].Y; i++)
            {
                Vector2 tileToCheck = new Vector2(checkCorners[1].X, i);
                if (Global.activeWorld.GetSolid(tileToCheck))
                {
                    //MARK: If the player would overshoot and collide, the player ends up adjacent to the tile, later will be done with adjusting a vector between current and future position
                    velocity.X = 0;
                    newPosition.X = ((int)checkCorners[0].X) * Global.TILE_SIZE;
                    break;
                }
            }

            position = newPosition;
            mapPosition = position / Global.TILE_SIZE;
            collisionBox.X = (int)position.X;
            collisionBox.Y = (int)position.Y;
        }
    }
}
