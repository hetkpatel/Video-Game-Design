using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Space_Invaders
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //Rectangle[,] alienRects = new Rectangle[8, 8];
        //Texture2D[] alienTexts = new Texture2D[8];
        Alien[,] aliens = new Alien[8, 8];
        enum GameState { Left, Right, Down, Up }
        GameState gameState = GameState.Right;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1000;
            graphics.PreferredBackBufferWidth = 1000;
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
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    aliens[i, j] = new Alien();
                    aliens[i, j].Rect = new Rectangle(i * 40, j * 20, 20, 20);
                }
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
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    aliens[i, j].Text = this.Content.Load<Texture2D>("SpaceInvaders" + j);
                }
            }
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
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    if (gameState == GameState.Right && aliens[i, j].Rect.X < 900)
                    {
                        //aliens[i, j].Rect.X += 5;
                        aliens[i, j].setPosition(5, true);
                    }
                    else if (gameState == GameState.Right && aliens[i, j].Rect.X == 900)
                    {
                        gameState = GameState.Down;
                        i = 0;
                        j = 0;
                    }
                    if (gameState == GameState.Down && aliens[i, j].Rect.Y < 900)
                    {
                        //aliens[i, j].Rect.Y += 5;
                        aliens[i, j].setPosition(5, false);
                    }
                    else if (gameState == GameState.Down && aliens[i, j].Rect.Y == 900)
                    {
                        gameState = GameState.Left;
                        i = 0;
                        j = 0;
                    }
                    if (gameState == GameState.Left && aliens[i, j].Rect.X > 100)
                    {
                        //aliens[i, j].Rect.X -= 5;
                        aliens[i, j].setPosition(-5, true);
                    }
                    else if (gameState == GameState.Left && aliens[i, j].Rect.X == 100)
                    {
                        gameState = GameState.Up;
                        i = 0;
                        j = 0;
                    }
                    if (gameState == GameState.Up && aliens[i, j].Rect.Y > 100)
                    {
                        //aliens[i, j].Rect.Y -= 5;
                        aliens[i, j].setPosition(-5, false);
                    }
                    else if (gameState == GameState.Up && aliens[i, j].Rect.Y == 100)
                    {
                        gameState = GameState.Right;
                        i = 0;
                        j = 0;
                    }
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    switch (gameState)
                    {
                        case GameState.Right:
                            spriteBatch.Draw(aliens[i, j].Text, aliens[i, j].Rect, Color.White);
                            break;
                        case GameState.Down:
                            spriteBatch.Draw(aliens[i, j].Text, aliens[i, j].Rect, Color.White);
                            break;
                        case GameState.Left:
                            spriteBatch.Draw(aliens[i, j].Text, aliens[i, j].Rect, Color.White);
                            break;
                        case GameState.Up:
                            spriteBatch.Draw(aliens[i, j].Text, aliens[i, j].Rect, Color.White);
                            break;
                    }
                }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}