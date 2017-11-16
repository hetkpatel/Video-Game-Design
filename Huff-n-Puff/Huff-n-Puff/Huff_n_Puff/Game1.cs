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

namespace Huff_n_Puff
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D sprite;
        Rectangle[] bobLRct = new Rectangle[5];
        Rectangle[] bobRRct = new Rectangle[5];
        Rectangle bobRct;
        Rectangle[] featherRct = new Rectangle[4];
        Rectangle fethrRct;

        Random fethrDirct = new Random(), fethrDist = new Random();

        bool gameInPlay = true;
        int playerState = 0, featherState = 0;
        int playerX, playerY;
        double featherX, featherY;
        bool playerDirctL = true;
        int time = 0;
        int dirct;
        double dist;

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
            for (int i = 0; i < 5; i++)
                bobLRct[i] = new Rectangle(i * 25, 0, 24, 34);
            for (int i = 0; i < 5; i++)
                bobRRct[i] = new Rectangle(i * 25, 35, 24, 34);
            for (int i = 0; i < 4; i++)
                featherRct[i] = new Rectangle(i * 25, 70, 24, 24);

            playerX = GraphicsDevice.Viewport.Width / 2 - 38;
            playerY = GraphicsDevice.Viewport.Height - 106;
            bobRct = new Rectangle(playerX, playerY, 75, 106);

            featherX = GraphicsDevice.Viewport.Width / 2 - 25;
            featherY = GraphicsDevice.Viewport.Height / 2 - 25;
            fethrRct = new Rectangle((int)featherX, (int)featherY, 50, 50);

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
            sprite = this.Content.Load<Texture2D>("HuffNPuff");
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
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            time++;
            double seconds = time / 60.0;
            bool changeSeconds = seconds % 0.25 == 0;
            if (gameInPlay)
            {
                // TODO: Add your update logic here
                if (changeSeconds)
                    featherState++;

                if (seconds % 3 == 0)
                    dirct = fethrDirct.Next(2);
                if (seconds % 2 == 0)
                    dist = fethrDist.NextDouble();

                if (dirct == 0)
                    featherX += dist;
                else
                    featherX -= dist;

                if (playerDirctL && kb.IsKeyDown(Keys.Right) && !(kb.IsKeyDown(Keys.Left)))
                    playerDirctL = false;
                else if (!playerDirctL && kb.IsKeyDown(Keys.Left) && !(kb.IsKeyDown(Keys.Right)))
                    playerDirctL = true;

                if (kb.IsKeyDown(Keys.Right) && !(kb.IsKeyDown(Keys.Left)) && !(kb.IsKeyDown(Keys.Space)))
                {
                    if (changeSeconds)
                        playerState++;
                    if (playerState >= 4)
                        playerState = 0;
                    playerX += 5;
                    playerY = GraphicsDevice.Viewport.Height - 106;
                    featherY += 0.5;
                }
                else if (kb.IsKeyDown(Keys.Left) && !(kb.IsKeyDown(Keys.Right)) && !(kb.IsKeyDown(Keys.Space)))
                {
                    if (changeSeconds)
                        playerState++;
                    if (playerState >= 4)
                        playerState = 0;
                    playerX -= 5;
                    playerY = GraphicsDevice.Viewport.Height - 106;
                    featherY += 0.5;
                }
                else if (kb.IsKeyDown(Keys.Space))
                {
                    playerState = 4;
                    playerY = GraphicsDevice.Viewport.Height - 117;
                    if (puffFeather())
                        featherY -= 1;
                    else
                        featherY += 0.5;
                }
                else
                {
                    playerState = 0;
                    playerY = GraphicsDevice.Viewport.Height - 106;
                    featherY += 0.5;
                }

                if (featherState == 4)
                    featherState = 0;

                if (playerX < -80)
                    playerX = GraphicsDevice.Viewport.Width + 5;
                else if (playerX > GraphicsDevice.Viewport.Width + 5)
                    playerX = -80;

                if (featherX < -55)
                    featherX = GraphicsDevice.Viewport.Width + 5;
                else if (featherX > GraphicsDevice.Viewport.Width + 5)
                    featherX = -55;
                if (featherY > (GraphicsDevice.Viewport.Height - 50))
                    gameInPlay = false;

                bobRct = new Rectangle(playerX, playerY, 75, 106);
                fethrRct = new Rectangle((int)featherX, (int)featherY, 50, 50);
            }

            base.Update(gameTime);
        }

        private Boolean puffFeather()
        {
            const int PADDING = 35;
            if (bobRct.X >= (fethrRct.X - PADDING) && (bobRct.X + 75) < (fethrRct.X + 50 + PADDING))
                return true;
            else return false;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            if (playerDirctL)
                spriteBatch.Draw(sprite, bobRct, bobLRct[playerState], Color.White);
            else
                spriteBatch.Draw(sprite, bobRct, bobRRct[playerState], Color.White);

            spriteBatch.Draw(sprite, fethrRct, featherRct[featherState], Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
