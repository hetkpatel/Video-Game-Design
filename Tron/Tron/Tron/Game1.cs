using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tron
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont titleFont;
        SpriteFont subtextFont;

        Texture2D whiteBox;

        Rectangle player1;
        Rectangle player2;

        GameState gameState;
        Vector2 screen;

        int timer;

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
            this.IsMouseVisible = true;
            // TODO: Add your initialization logic here

            screen = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            gameState = GameState.MAIN_MENU;

            timer = 3;
            
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

            // TODO: use this.Content to load your game content here
            whiteBox = this.Content.Load<Texture2D>("White");

            titleFont = this.Content.Load<SpriteFont>("Title");
            subtextFont = this.Content.Load<SpriteFont>("Subtext");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        Stopwatch stopwtch = new Stopwatch();
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.MAIN_MENU:
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        stopwtch.Start();
                        gameState = GameState.GAME_PLAY;
                    }
                    break;
                case GameState.GAME_PLAY:
                    timer = 3 - stopwtch.Elapsed.Seconds;
                    if (timer == 0)
                    {
                        timer = -1;
                        stopwtch.Stop();

                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            switch (gameState)
            {
                case GameState.MAIN_MENU:
                    spriteBatch.DrawString(titleFont, "Tron", new Vector2(screen.X/2 - titleFont.MeasureString("Tron").X/2, 50), Color.White);
                    spriteBatch.DrawString(subtextFont, "Play(P)", new Vector2(screen.X / 2 - subtextFont.MeasureString("Play(P)").X / 2, 200), Color.White);
                    break;
                case GameState.GAME_PLAY:
                    String timerStr = "" + timer;
                    if (timerStr.Equals("-1"))
                        timerStr = "GO!";
                    spriteBatch.DrawString(titleFont, timerStr, new Vector2(screen.X / 2 - titleFont.MeasureString(timerStr).X / 2, screen.Y / 2 - titleFont.MeasureString(timerStr).Y / 2), Color.White);
                    break;
                default:
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
