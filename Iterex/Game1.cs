using System;
using System.Collections.Generic;
using Iterex.Common;
using Iterex.Common.Animation;
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

        private Camera _camera;

        public static int ScreenWidth;
        public static int ScreenHeight;
        
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
            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            _camera = new Camera();
            _camera.ScaleRatio = 2.5f;

            Global.Sprites = new List<Sprite>();
            Global.EntityTextures = new Dictionary<string, Texture2D>();
            Global.TileTextures = new Dictionary<string, Texture2D>();
            Global.AnimatedEntityTextures = new Dictionary<string, Dictionary<string, Animation>>();

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
            Global.TileTextures.Add("empty", null);
            Global.TileTextures.Add("Tile_0000", Content.Load<Texture2D>("tiles/Tile_0000"));
            Global.TileTextures.Add("Tile_0001", Content.Load<Texture2D>("tiles/Tile_0001"));
            Global.TileTextures.Add("Tile_0010", Content.Load<Texture2D>("tiles/Tile_0010"));
            Global.TileTextures.Add("Tile_0011", Content.Load<Texture2D>("tiles/Tile_0011"));
            Global.TileTextures.Add("Tile_0100", Content.Load<Texture2D>("tiles/Tile_0100"));
            Global.TileTextures.Add("Tile_0101", Content.Load<Texture2D>("tiles/Tile_0101"));
            Global.TileTextures.Add("Tile_0110", Content.Load<Texture2D>("tiles/Tile_0110"));
            Global.TileTextures.Add("Tile_0111", Content.Load<Texture2D>("tiles/Tile_0111"));
            Global.TileTextures.Add("Tile_1000", Content.Load<Texture2D>("tiles/Tile_1000"));
            Global.TileTextures.Add("Tile_1001", Content.Load<Texture2D>("tiles/Tile_1001"));
            Global.TileTextures.Add("Tile_1010", Content.Load<Texture2D>("tiles/Tile_1010"));
            Global.TileTextures.Add("Tile_1011", Content.Load<Texture2D>("tiles/Tile_1011"));
            Global.TileTextures.Add("Tile_1100", Content.Load<Texture2D>("tiles/Tile_1100"));
            Global.TileTextures.Add("Tile_1101", Content.Load<Texture2D>("tiles/Tile_1101"));
            Global.TileTextures.Add("Tile_1110", Content.Load<Texture2D>("tiles/Tile_1110"));
            Global.TileTextures.Add("Tile_1111", Content.Load<Texture2D>("tiles/Tile_1111"));

            Global.EntityTextures.Add("Woodcutter", Content.Load<Texture2D>("entitysprites/Woodcutter"));

            Dictionary<string, Animation> woodCutterAnimations = new Dictionary<string, Animation>();
            woodCutterAnimations.Add("WalkRight", new Animation(Content.Load<Texture2D>("animatedEntities/Woodcutter_walk_right"), 6, true));
            woodCutterAnimations.Add("WalkLeft", new Animation(Content.Load<Texture2D>("animatedEntities/Woodcutter_walk_left"), 6, true));
            woodCutterAnimations.Add("RunRight", new Animation(Content.Load<Texture2D>("animatedEntities/Woodcutter_run_right"), 6, true));
            woodCutterAnimations.Add("RunLeft", new Animation(Content.Load<Texture2D>("animatedEntities/Woodcutter_run_left"), 6, true));
            woodCutterAnimations.Add("IdleRight", new Animation(Content.Load<Texture2D>("animatedEntities/Woodcutter_idle_right"), 4, true));
            woodCutterAnimations.Add("IdleLeft", new Animation(Content.Load<Texture2D>("animatedEntities/Woodcuttuer_idle_left"), 4, true));

            Global.AnimatedEntityTextures.Add("Woodcutter", woodCutterAnimations);

            //MARK: Need to initialize them once we have the textures for the width of collision box
            Global.ActiveWorld = new World.World();
            Global.Player = new Player(Global.EntityTextures["Woodcutter"], Global.AnimatedEntityTextures["Woodcutter"])
            {
                Position = new Vector2(0, 3 * Global.TILE_SIZE),
                Velocity = new Vector2(0, 0),
                OnGround = false,
                Direction = 1,
                Speed = 70f
            };

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
            Global.KeyboardState = Keyboard.GetState();
            Global.MouseState = Mouse.GetState();

            Global.Player.Update(gameTime, Global.Sprites);

            //MARK: Centering camera on player
            _camera.Follow(Global.Player);

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

            spriteBatch.Begin(transformMatrix: _camera.TransformMatrix);

            Global.ActiveWorld.Draw(spriteBatch);
            Global.Player.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
