using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        int baseMove = 32;
        int screenHeight;
        int screenWidth;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHelper inputHelper;

        Snake player;
        Texture2D snakeTexture;
        Texture2D appleTexture;
        Rectangle appleRect;
        Vector2 movement;


        public Game1()
        {
            screenHeight = 20 * baseMove;
            screenWidth = 25 * baseMove;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 15f);

            graphics.PreferredBackBufferWidth = screenWidth;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = screenHeight;   // set this value to the desired height of your window
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            StartGame();
            inputHelper = new InputHelper();
        }

        public void StartGame()
        {
            base.Initialize();

            player = new Snake(snakeTexture, baseMove);
            appleRect = new Rectangle(64, 64, appleTexture.Width, appleTexture.Height);
            movement = new Vector2(1, 0);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            snakeTexture = Content.Load<Texture2D>("Snake");
            appleTexture = Content.Load<Texture2D>("apple");
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
            inputHelper.Update();

            if (inputHelper.IsNewKeyPress(Keys.Escape)) { Exit(); }

            if (inputHelper.IsNewKeyPress(Keys.Left) && movement.X == 0)
            {
                movement.X = -1;
                movement.Y = 0;
            }
            if (inputHelper.IsNewKeyPress(Keys.Right) && movement.X == 0)
            {
                movement.X = 1;
                movement.Y = 0;
            }
            if (inputHelper.IsNewKeyPress(Keys.Up) && movement.Y == 0)
            {
                movement.X = 0;
                movement.Y = -1;
            }
            if (inputHelper.IsNewKeyPress(Keys.Down) && movement.Y == 0)
            {
                movement.X = 0;
                movement.Y = 1;
            }
            
            player.Update(movement, graphics, appleRect);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.ForestGreen);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.Draw(appleTexture, appleRect, Color.White);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}