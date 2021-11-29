using System;
using System.Collections.Generic;
using System.Linq;
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

        UI.UIStatus playerStatus = new UI.UIStatus(new Rectangle(0,0,500,20),1,null);
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
            Global.UITextures = new Dictionary<string, ITextureAdapter>();
            Global.PlayerAnimations = new Dictionary<string, Dictionary<string, ITextureAdapter>>();
            Global.EnemyAnimations = new Dictionary<string, Dictionary<string, ITextureAdapter>>();
            Global.Entities = new List<Entity.Entity>();
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
            LoadUITexture();

            //MARK: Need to initialize them once we have the textures for the width of collision box
            Global.ActiveWorld = new World.World(300, 100);
            Global.ParralexBackground = new ParallaxBackground();

            Global.Player = new GraveRobber()
            {
                Position = new Vector2(10 * Global.TILE_SIZE, 0 * Global.TILE_SIZE),
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
            Global.Entities.Add(Global.Player);
        }

        private Dictionary<string, ITextureAdapter> GetPlayerAnimations(string playerName)
        {
            Dictionary<string, ITextureAdapter> animationList = new Dictionary<string, ITextureAdapter>();

            animationList.Add("Walk", Global.AnimatedEntityTextures[playerName + "Walk"]);
            animationList.Add("Run", Global.AnimatedEntityTextures[playerName + "Run"]);
            animationList.Add("Jump", Global.AnimatedEntityTextures[playerName + "Jump"]);
            animationList.Add("Idle", Global.AnimatedEntityTextures[playerName + "Idle"]);
            animationList.Add("Attack1", Global.AnimatedEntityTextures[playerName + "Attack1"]);
            animationList.Add("Attack2", Global.AnimatedEntityTextures[playerName + "Attack2"]);
            animationList.Add("Attack3", Global.AnimatedEntityTextures[playerName + "Attack3"]);
            animationList.Add("Climb", Global.AnimatedEntityTextures[playerName + "Climb"]);
            animationList.Add("Dead", Global.AnimatedEntityTextures[playerName + "Dead"]);
            animationList.Add("Hurt", Global.AnimatedEntityTextures[playerName + "Hurt"]);
            animationList.Add("Push", Global.AnimatedEntityTextures[playerName + "Push"]);

            return animationList;
        }


        private Dictionary<string, ITextureAdapter> GetEnemyAnimations(string enemyName)
        {
            Dictionary<string, ITextureAdapter> animationList = new Dictionary<string, ITextureAdapter>();

            animationList.Add("Run", Global.AnimatedEntityTextures[enemyName + "Run"]);
            animationList.Add("Idle", Global.AnimatedEntityTextures[enemyName + "Idle"]);
            animationList.Add("Attack1", Global.AnimatedEntityTextures[enemyName + "Attack1"]);
            animationList.Add("Attack2", Global.AnimatedEntityTextures[enemyName + "Attack2"]);
            animationList.Add("Attack3", Global.AnimatedEntityTextures[enemyName + "Attack3"]);
            animationList.Add("Attack4", Global.AnimatedEntityTextures[enemyName + "Attack4"]);
            animationList.Add("Dead", Global.AnimatedEntityTextures[enemyName + "Dead"]);
            animationList.Add("Hurt", Global.AnimatedEntityTextures[enemyName + "Hurt"]);
            animationList.Add("Sneer", Global.AnimatedEntityTextures[enemyName + "Sneer"]);

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

            Global.AnimatedEntityTextures.Add("WoodcutterWalk", GetAnimatedEntityTexture("Woodcutter_walk", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterRun", GetAnimatedEntityTexture("Woodcutter_run", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterJump", GetAnimatedEntityTexture("Woodcutter_jump", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterIdle", GetAnimatedEntityTexture("Woodcutter_idle", 4, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterAttack1", GetAnimatedEntityTexture("Woodcutter_attack1", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("WoodcutterAttack2", GetAnimatedEntityTexture("Woodcutter_attack2", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("WoodcutterAttack3", GetAnimatedEntityTexture("Woodcutter_attack3", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("WoodcutterClimb", GetAnimatedEntityTexture("Woodcutter_climb", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterCraft", GetAnimatedEntityTexture("Woodcutter_craft", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("WoodcutterDead", GetAnimatedEntityTexture("Woodcutter_death", 6, 0.1f, false));
            Global.AnimatedEntityTextures.Add("WoodcutterHurt", GetAnimatedEntityTexture("Woodcutter_hurt", 3, 0.1f, false));
            Global.AnimatedEntityTextures.Add("WoodcutterPush", GetAnimatedEntityTexture("Woodcutter_push", 6, 0.1f, true));

            Global.AnimatedEntityTextures.Add("GraveRobberWalk", GetAnimatedEntityTexture("GraveRobber_walk", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("GraveRobberRun", GetAnimatedEntityTexture("GraveRobber_run", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("GraveRobberJump", GetAnimatedEntityTexture("GraveRobber_jump", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("GraveRobberIdle", GetAnimatedEntityTexture("GraveRobber_idle", 4, 0.1f, true));
            Global.AnimatedEntityTextures.Add("GraveRobberAttack1", GetAnimatedEntityTexture("GraveRobber_attack1", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("GraveRobberAttack2", GetAnimatedEntityTexture("GraveRobber_attack2", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("GraveRobberAttack3", GetAnimatedEntityTexture("GraveRobber_attack3", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("GraveRobberClimb", GetAnimatedEntityTexture("GraveRobber_climb", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("GraveRobberCraft", GetAnimatedEntityTexture("GraveRobber_craft", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("GraveRobberDead", GetAnimatedEntityTexture("GraveRobber_death", 6, 0.1f, false));
            Global.AnimatedEntityTextures.Add("GraveRobberHurt", GetAnimatedEntityTexture("GraveRobber_hurt", 3, 0.1f, false));
            Global.AnimatedEntityTextures.Add("GraveRobberPush", GetAnimatedEntityTexture("GraveRobber_push", 6, 0.1f, true));

            Global.AnimatedEntityTextures.Add("SteamManWalk", GetAnimatedEntityTexture("SteamMan_walk", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("SteamManRun", GetAnimatedEntityTexture("SteamMan_run", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("SteamManJump", GetAnimatedEntityTexture("SteamMan_jump", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("SteamManIdle", GetAnimatedEntityTexture("SteamMan_idle", 4, 0.1f, true));
            Global.AnimatedEntityTextures.Add("SteamManAttack1", GetAnimatedEntityTexture("SteamMan_attack1", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("SteamManAttack2", GetAnimatedEntityTexture("SteamMan_attack2", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("SteamManAttack3", GetAnimatedEntityTexture("SteamMan_attack3", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("SteamManClimb", GetAnimatedEntityTexture("SteamMan_climb", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("SteamManCraft", GetAnimatedEntityTexture("SteamMan_craft", 6, 0.1f, true));
            Global.AnimatedEntityTextures.Add("SteamManDead", GetAnimatedEntityTexture("SteamMan_death", 6, 0.1f, false));
            Global.AnimatedEntityTextures.Add("SteamManHurt", GetAnimatedEntityTexture("SteamMan_hurt", 3, 0.1f, false));
            Global.AnimatedEntityTextures.Add("SteamManPush", GetAnimatedEntityTexture("SteamMan_push", 6, 0.1f, true));

            Global.AnimatedEntityTextures.Add("CentipedeAttack1", GetAnimatedEntityTexture("Centipede_attack1", 4, 0.07f, false));
            Global.AnimatedEntityTextures.Add("CentipedeAttack2", GetAnimatedEntityTexture("Centipede_attack2", 4, 0.07f, false));
            Global.AnimatedEntityTextures.Add("CentipedeAttack3", GetAnimatedEntityTexture("Centipede_attack3", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("CentipedeAttack4", GetAnimatedEntityTexture("Centipede_attack4", 4, 0.07f, false));
            Global.AnimatedEntityTextures.Add("CentipedeDead", GetAnimatedEntityTexture("Centipede_death", 4, 0.1f, false));
            Global.AnimatedEntityTextures.Add("CentipedeHurt", GetAnimatedEntityTexture("Centipede_hurt", 2, 0.1f, false));
            Global.AnimatedEntityTextures.Add("CentipedeIdle", GetAnimatedEntityTexture("Centipede_idle", 4, 0.1f, true));
            Global.AnimatedEntityTextures.Add("CentipedeSneer", GetAnimatedEntityTexture("Centipede_sneer", 6, 0.08f, true));
            Global.AnimatedEntityTextures.Add("CentipedeRun", GetAnimatedEntityTexture("Centipede_walk", 4, 0.07f, true));

            Global.AnimatedEntityTextures.Add("BattleTurtleAttack1", GetAnimatedEntityTexture("Battle_turtle_attack1", 4, 0.07f, false));
            Global.AnimatedEntityTextures.Add("BattleTurtleAttack2", GetAnimatedEntityTexture("Battle_turtle_attack2", 4, 0.07f, false));
            Global.AnimatedEntityTextures.Add("BattleTurtleAttack3", GetAnimatedEntityTexture("Battle_turtle_attack3", 4, 0.07f, false));
            Global.AnimatedEntityTextures.Add("BattleTurtleAttack4", GetAnimatedEntityTexture("Battle_turtle_attack4", 4, 0.07f, false));
            Global.AnimatedEntityTextures.Add("BattleTurtleDead", GetAnimatedEntityTexture("Battle_turtle_death", 4, 0.1f, false));
            Global.AnimatedEntityTextures.Add("BattleTurtleHurt", GetAnimatedEntityTexture("Battle_turtle_hurt", 2, 0.1f, false));
            Global.AnimatedEntityTextures.Add("BattleTurtleIdle", GetAnimatedEntityTexture("Battle_turtle_idle", 4, 0.1f, true));
            Global.AnimatedEntityTextures.Add("BattleTurtleSneer", GetAnimatedEntityTexture("Battle_turtle_sneer", 6, 0.08f, true));
            Global.AnimatedEntityTextures.Add("BattleTurtleRun", GetAnimatedEntityTexture("Battle_turtle_walk", 4, 0.07f, true));

            Global.AnimatedEntityTextures.Add("BigBloatedAttack1", GetAnimatedEntityTexture("Big_bloated_attack1", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("BigBloatedAttack2", GetAnimatedEntityTexture("Big_bloated_attack2", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("BigBloatedAttack3", GetAnimatedEntityTexture("Big_bloated_attack3", 4, 0.07f, false));
            Global.AnimatedEntityTextures.Add("BigBloatedAttack4", GetAnimatedEntityTexture("Big_bloated_attack4", 6, 0.07f, false));
            Global.AnimatedEntityTextures.Add("BigBloatedDead", GetAnimatedEntityTexture("Big_bloated_death", 4, 0.1f, false));
            Global.AnimatedEntityTextures.Add("BigBloatedHurt", GetAnimatedEntityTexture("Big_bloated_hurt", 2, 0.1f, false));
            Global.AnimatedEntityTextures.Add("BigBloatedIdle", GetAnimatedEntityTexture("Big_bloated_idle", 4, 0.1f, true));
            Global.AnimatedEntityTextures.Add("BigBloatedSneer", GetAnimatedEntityTexture("Big_bloated_sneer", 6, 0.08f, true));
            Global.AnimatedEntityTextures.Add("BigBloatedRun", GetAnimatedEntityTexture("Big_bloated_walk", 6, 0.07f, true));

            Global.PlayerAnimations.Add("Woodcutter", GetPlayerAnimations("Woodcutter"));
            Global.PlayerAnimations.Add("GraveRobber", GetPlayerAnimations("GraveRobber"));
            Global.PlayerAnimations.Add("SteamMan", GetPlayerAnimations("SteamMan"));

            Global.EnemyAnimations.Add("Centipede", GetEnemyAnimations("Centipede"));
            Global.EnemyAnimations.Add("BattleTurtle", GetEnemyAnimations("BattleTurtle"));
            Global.EnemyAnimations.Add("BigBloated", GetEnemyAnimations("BigBloated"));
        }

        private void LoadUITexture()
        {
            ITextureAdapter GetUITexture(string name)
            {
                return new SimpleTextureAdapter(Content.Load<Texture2D>("uiSprites/" + name), name);
            }

            Global.UITextures.Add("pixel", GetUITexture("pixel"));
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
            Global.Entities = Global.Entities.Where(entity => entity.Attributes.IsRemovable == false).ToList();
            foreach(Entity.Entity entity in Global.Entities)
            {
                entity.Update(gameTime, Global.Sprites);
            }

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

            #region DrawParallaxBackground
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp);

            Global.ParralexBackground.Draw(_spriteBatch);

            _spriteBatch.End();
            #endregion

            #region DrawGame
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _camera.TransformMatrix);

            Global.ActiveWorld.Draw(_spriteBatch);
            foreach (Entity.Entity entity in Global.Entities)
            {
                entity.Draw(_spriteBatch);
            }

            playerStatus.Draw(_spriteBatch, Common.Global.Player.Attributes.HP, Common.Global.Player.Attributes.MaxHP);

            //test
            /*Sprite treetop = new Sprite(new Dictionary<string, ITextureAdapter>() { { "tree3", Global.TileTextures["tree3"] } },
                                                  "tree3")
            {
                Position = new Vector2(10 * Global.TILE_SIZE, 20 * Global.TILE_SIZE),
                IsSolid = false
            };

            treetop.Draw(_spriteBatch);*/

            _spriteBatch.End();
            #endregion

            base.Draw(gameTime);
        }

    }
}