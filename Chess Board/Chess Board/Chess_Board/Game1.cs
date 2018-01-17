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

namespace Chess_Board
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle[] blackRtc = new Rectangle[6];
        Rectangle[] whiteRtc = new Rectangle[6];
        Rectangle boardRtc;

        Texture2D[] blackPcs = new Texture2D[6];
        Texture2D[] whitePcs = new Texture2D[6];
        Texture2D board;

        int multiple = 60;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 543;
            graphics.PreferredBackBufferWidth = 500;
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
            boardRtc = new Rectangle(0, 0, 500, 543);

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
            for (int i = 0; i < 6; i++)
            {
                blackPcs[i] = this.Content.Load<Texture2D>("B" + (i + 1));
            }
            for (int i = 0; i < 6; i++)
            {
                whitePcs[i] = this.Content.Load<Texture2D>("W" + (i + 1));
            }
            board = this.Content.Load<Texture2D>("Board");
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.U))
                multiple++;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                multiple--;

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
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            spriteBatch.Draw(board, boardRtc, Color.White);

            for (int i = 0; i < 3; i++)
            {
                blackRtc[i] = new Rectangle((i * 61) + 10, 14, 50, 50);
                spriteBatch.Draw(blackPcs[i], blackRtc[i], Color.White);
            }
            blackRtc[3] = new Rectangle(193, 14, 50, 50);
            spriteBatch.Draw(blackPcs[3], blackRtc[3], Color.White);
            blackRtc[4] = new Rectangle(255, 14, 50, 50);
            spriteBatch.Draw(blackPcs[4], blackRtc[4], Color.White);
            for (int i = 0; i < 3; i++)
            {
                blackRtc[i] = new Rectangle((i * 61) + 318, 14, 50, 50);
                spriteBatch.Draw(blackPcs[2-i], blackRtc[i], Color.White);
            }
            for (int i = 0; i < 8; i++)
            {
                blackRtc[5] = new Rectangle((i * 61) + 10, 79, 50, 50);
                spriteBatch.Draw(blackPcs[5], blackRtc[5], Color.White);
            }

            for (int i = 0; i < 3; i++)
            {
                whiteRtc[i] = new Rectangle((i * 61) + 10, 477, 50, 50);
                spriteBatch.Draw(whitePcs[i], whiteRtc[i], Color.White);
            }
            whiteRtc[3] = new Rectangle(193, 477, 50, 50);
            spriteBatch.Draw(whitePcs[3], whiteRtc[3], Color.White);
            whiteRtc[4] = new Rectangle(255, 477, 50, 50);
            spriteBatch.Draw(whitePcs[4], whiteRtc[4], Color.White);
            for (int i = 0; i < 3; i++)
            {
                whiteRtc[i] = new Rectangle((i * 61) + 318, 477, 50, 50);
                spriteBatch.Draw(whitePcs[2 - i], whiteRtc[i], Color.White);
            }
            for (int i = 0; i < 8; i++)
            {
                whiteRtc[5] = new Rectangle((i * 61) + 10, 410, 50, 50);
                spriteBatch.Draw(whitePcs[5], whiteRtc[5], Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
