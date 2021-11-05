using System;
using System.Collections.Generic;
using Iterex.Common;
using Iterex.Common.Animation;
using Iterex.Common.TextureAdapter;
using Iterex.Entity;
using Iterex.Entity.Player;
using Iterex.World;
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
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera _camera;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public Game1(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
            _graphics = new GraphicsDeviceManager(this);
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
            InitializeGraphic();
            InitializeCamera();
            InitializeGlobalProperties();

            base.Initialize();
        }

        private void InitializeGraphic()
        {
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.ApplyChanges();
        }

        private void InitializeCamera()
        {
            _camera = new Camera();
            _camera.ScaleRatio = 2.5f;
        }
 
        private void InitializeGlobalProperties()
        {
            Global.Sprites = new List<Sprite>();
            Global.BackgroundTextures = new Dictionary<string, ITextureAdapter>();
            Global.EntityTextures = new Dictionary<string, ITextureAdapter>();
            Global.TileTextures = new Dictionary<string, ITextureAdapter>();
            Global.AnimatedEntityTextures = new Dictionary<string, ITextureAdapter>();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //MARK: We load the graphics for entities and tiles into a global storage, referencing them by key
            LoadTiles();
            LoadBackgrounds();
            LoadAnimatedEntityTexture();

            //MARK: Need to initialize them once we have the textures for the width of collision box
            Global.ActiveWorld = new World.World(200, 100);
            Global.ParralexBackground = new ParallaxBackground();

            Global.Player = new Player(GetPlayerAnimations("Woodcutter"), "IdleRight")
            {
                Position = new Vector2(10 * Global.TILE_SIZE, 10 * Global.TILE_SIZE),
                Velocity = new Vector2(0, 0),
                Attributes = new EntityAttributes()
                {
                    MaxHP = 100,
                    MaxMP = 100,
                    HP = 100,
                    MP = 100,
                    Damage = 20,
                    DodgeChance = 20,
                    Team = 1,
                    Speed = 70f
                }
            };

        }

        private Dictionary<string, ITextureAdapter> GetPlayerAnimations(string playerName)
        {
            Dictionary<string, ITextureAdapter> animationList = new Dictionary<string, ITextureAdapter>();

            animationList.Add("WalkRight", Global.AnimatedEntityTextures[playerName + "WalkRight"]);
            animationList.Add("WalkLeft", Global.AnimatedEntityTextures[playerName + "WalkLeft"]);
            animationList.Add("RunRight", Global.AnimatedEntityTextures[playerName + "RunRight"]);
            animationList.Add("RunLeft", Global.AnimatedEntityTextures[playerName + "RunLeft"]);
            animationList.Add("JumpRight", Global.AnimatedEntityTextures[playerName + "JumpRight"]);
            animationList.Add("JumpLeft", Global.AnimatedEntityTextures[playerName + "JumpLeft"]);
            animationList.Add("IdleRight", Global.AnimatedEntityTextures[playerName + "IdleRight"]);
            animationList.Add("IdleLeft", Global.AnimatedEntityTextures[playerName + "IdleLeft"]);

            return animationList;
        }
        private void LoadTiles()
        {
            ITextureAdapter GetTileTexture(string name)
            {
                return new SimpleTextureAdapter(Content.Load<Texture2D>("tiles/" + name), name);
            }

            Global.TileTextures.Add("Tile_0000", GetTileTexture("Tile_0000"));
            Global.TileTextures.Add("Tile_0001", GetTileTexture("Tile_0001"));
            Global.TileTextures.Add("Tile_0010", GetTileTexture("Tile_0010"));
            Global.TileTextures.Add("Tile_0011", GetTileTexture("Tile_0011"));
            Global.TileTextures.Add("Tile_0100", GetTileTexture("Tile_0100"));
            Global.TileTextures.Add("Tile_0101", GetTileTexture("Tile_0101"));
            Global.TileTextures.Add("Tile_0110", GetTileTexture("Tile_0110"));
            Global.TileTextures.Add("Tile_0111", GetTileTexture("Tile_0111"));
            Global.TileTextures.Add("Tile_1000", GetTileTexture("Tile_1000"));
            Global.TileTextures.Add("Tile_1001", GetTileTexture("Tile_1001"));
            Global.TileTextures.Add("Tile_1010", GetTileTexture("Tile_1010"));
            Global.TileTextures.Add("Tile_1011", GetTileTexture("Tile_1011"));
            Global.TileTextures.Add("Tile_1100", GetTileTexture("Tile_1100"));
            Global.TileTextures.Add("Tile_1101", GetTileTexture("Tile_1101"));
            Global.TileTextures.Add("Tile_1110", GetTileTexture("Tile_1110"));
            Global.TileTextures.Add("Tile_1111", GetTileTexture("Tile_1111"));

            //load tree textures
            Global.TileTextures.Add("tree1", GetTileTexture("tree1"));
            Global.TileTextures.Add("tree2", GetTileTexture("tree2"));
            Global.TileTextures.Add("tree3", GetTileTexture("tree3"));

        }

        private void LoadBackgrounds()
        {
            ITextureAdapter GetBackgroundTexture(string name)
            {
                return new SimpleTextureAdapter(Content.Load<Texture2D>("background/" + name), name);
            }

            Global.BackgroundTextures.Add("layer0", GetBackgroundTexture("layer0"));
            Global.BackgroundTextures.Add("layer1", GetBackgroundTexture("layer1"));
            Global.BackgroundTextures.Add("layer2", GetBackgroundTexture("layer2"));
            Global.BackgroundTextures.Add("layer3", GetBackgroundTexture("layer3"));
            Global.BackgroundTextures.Add("layer4", GetBackgroundTexture("layer4"));
            Global.BackgroundTextures.Add("layer5", GetBackgroundTexture("layer5"));
        }

        private void LoadAnimatedEntityTexture()
        {
            ITextureAdapter GetAnimatedEntityTexture(string name, int frameCount, float frameSpeed, bool isLooping)
            {
                return new AnimationTextureAdapter(Content.Load<Texture2D>("animatedEntities/" + name), name, frameCount, frameSpeed, isLooping);
            }

            Global.AnimatedEntityTextures.Add("WoodcutterWalkRight", GetAnimatedEntityTexture("Woodcutter_walk_right", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterWalkLeft", GetAnimatedEntityTexture("Woodcutter_walk_left", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterRunRight", GetAnimatedEntityTexture("Woodcutter_run_right", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterRunLeft", GetAnimatedEntityTexture("Woodcutter_run_left", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterJumpRight", GetAnimatedEntityTexture("Woodcutter_jump_right", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterJumpLeft", GetAnimatedEntityTexture("Woodcutter_jump_left", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterIdleRight", GetAnimatedEntityTexture("Woodcutter_idle_right", 4, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterIdleLeft", GetAnimatedEntityTexture("Woodcuttuer_idle_left", 4, 0.1f, true));

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

            Global.ParralexBackground.Update(gameTime);
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

            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);

            Global.ParralexBackground.Draw(_spriteBatch);

            _spriteBatch.End();

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.TransformMatrix);

            Global.ActiveWorld.Draw(_spriteBatch);
            Global.Player.Draw(_spriteBatch);

            //test
            /*Sprite treetop = new Sprite(new Dictionary<string, ITextureAdapter>() { { "tree3", Global.TileTextures["tree3"] } },
                                                  "tree3")
            {
                Position = new Vector2(10 * Global.TILE_SIZE, 20 * Global.TILE_SIZE),
                IsSolid = false
            };

            treetop.Draw(_spriteBatch);*/

            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}