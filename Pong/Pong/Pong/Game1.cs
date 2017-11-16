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

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        Texture2D sprite;
        Rectangle backgroundRct, ballRect, whiteRct;
        int ballSpeedX;
        int ballSpeedY;

        Rectangle bottom, left, right, top;

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
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            bottom = new Rectangle(0, screenHeight, screenWidth, 20);
            top = new Rectangle(0, 0, screenWidth, 0);
            left = new Rectangle(0, 0, 0, screenHeight);
            right = new Rectangle(screenWidth, 0, 20, screenHeight);
            backgroundRct = new Rectangle(0, 0, screenWidth, screenHeight);
            ballRect = new Rectangle(50, 50, 20, 20);
            ballSpeedX = 2;
            ballSpeedY = 3;

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
            sprite = this.Content.Load<Texture2D>("Pong Sprite Sheet");
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

            // TODO: Add your update logic here
            ballRect.X += ballSpeedX;
            ballRect.Y += ballSpeedY;

            if (ballRect.Intersects(bottom) || ballRect.Intersects(top))
                ballSpeedY *= -1;
            if (ballRect.Intersects(right) || ballRect.Intersects(left))
                ballSpeedX *= -1;

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
            spriteBatch.Draw(sprite, backgroundRct, new Rectangle(0, 0, 800, 480), Color.White);
            for (int i = 0; i < GraphicsDevice.Viewport.Height; i++)
                spriteBatch.Draw(sprite, new Rectangle((GraphicsDevice.Viewport.Width / 2) - 8, i * 25, 16, 16), new Rectangle(867, 714, 16, 16), Color.White);
            spriteBatch.Draw(sprite, ballRect, new Rectangle(801, 0, 713, 713), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
