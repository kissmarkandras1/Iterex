using System.Collections.Generic;
using Iterex.Entity.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Iterex
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Global.entityTextures = new Dictionary<string, Texture2D>();
            Global.tileTextures = new Dictionary<string, Texture2D>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //MARK: We load the graphics for entities and tiles into a global storage, referencing them by key
            Global.tileTextures.Add("grass", Content.Load<Texture2D>("tiles/grassdirt"));
            Global.tileTextures.Add("Tile_0000", Content.Load<Texture2D>("tiles/Tile_0000"));
            Global.tileTextures.Add("Tile_0001", Content.Load<Texture2D>("tiles/Tile_0001"));
            Global.tileTextures.Add("Tile_0010", Content.Load<Texture2D>("tiles/Tile_0010"));
            Global.tileTextures.Add("Tile_0011", Content.Load<Texture2D>("tiles/Tile_0011"));
            Global.tileTextures.Add("Tile_0100", Content.Load<Texture2D>("tiles/Tile_0100"));
            Global.tileTextures.Add("Tile_0101", Content.Load<Texture2D>("tiles/Tile_0101"));
            Global.tileTextures.Add("Tile_0110", Content.Load<Texture2D>("tiles/Tile_0110"));
            Global.tileTextures.Add("Tile_0111", Content.Load<Texture2D>("tiles/Tile_0111"));
            Global.tileTextures.Add("Tile_1000", Content.Load<Texture2D>("tiles/Tile_1000"));
            Global.tileTextures.Add("Tile_1001", Content.Load<Texture2D>("tiles/Tile_1001"));
            Global.tileTextures.Add("Tile_1010", Content.Load<Texture2D>("tiles/Tile_1010"));
            Global.tileTextures.Add("Tile_1011", Content.Load<Texture2D>("tiles/Tile_1011"));
            Global.tileTextures.Add("Tile_1100", Content.Load<Texture2D>("tiles/Tile_1100"));
            Global.tileTextures.Add("Tile_1101", Content.Load<Texture2D>("tiles/Tile_1101"));
            Global.tileTextures.Add("Tile_1110", Content.Load<Texture2D>("tiles/Tile_1110"));
            Global.tileTextures.Add("Tile_1111", Content.Load<Texture2D>("tiles/Tile_1111"));
            Global.entityTextures.Add("player", Content.Load<Texture2D>("entitysprites/Woodcutter"));

            //MARK: Need to initialize them once we have the textures for the width of collision box
            Global.activeWorld = new World.World();
            Global.player = new Player();

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //MARK: Get the states of keyboard and mouse inputs and store them globally
            Global.keyboardState = Keyboard.GetState();
            Global.mouseState = Mouse.GetState();

            //MARK: Time elapsed since last frame in seconds
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Global.player.Update(deltaTime);

            //MARK: Centering camera on player
            Global.cameraPosition = new Vector2(Global.player.position.X + Global.player.collisionBox.Width / 2 - graphics.PreferredBackBufferWidth / 2,
                Global.player.position.Y + Global.player.collisionBox.Height / 2 - graphics.PreferredBackBufferHeight / 2);

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            Global.activeWorld.Draw(spriteBatch);
            Global.player.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
