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

namespace Sidekick
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D big, station, small;
        Rectangle bigRct, staRct, smallRct;

        int xSmall, xBig;
        bool smallR = true, bigR = true;
        bool redS = false, redB = true;

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
            xBig = 0;
            xSmall = 0;

            bigRct = new Rectangle(xBig, 20, 150, 100);
            staRct = new Rectangle(350, 200, 100, 100);
            smallRct = new Rectangle(xSmall, (GraphicsDevice.Viewport.Height / 2) + 100, 50, 50);

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
            big = this.Content.Load<Texture2D>("BigMovingObj");
            small = this.Content.Load<Texture2D>("SmallMovingObj");
            station = this.Content.Load<Texture2D>("StationaryObj");
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
            if (smallR)
                xSmall += 5;
            else if (!smallR)
                xSmall -= 5;

            if (bigR)
                xBig += 1;
            else if (!bigR)
                xBig -= 1;

            smallRct = new Rectangle(xSmall, (GraphicsDevice.Viewport.Height / 2) + 100, 50, 50);
            bigRct = new Rectangle(xBig, 20, 150, 100);

            if ((xSmall + 50) == GraphicsDevice.Viewport.Width)
                smallR = false;
            else if (xSmall == 0)
                smallR = true;

            if ((xBig + 150) == GraphicsDevice.Viewport.Width)
                bigR = false;
            else if (xBig == 0)
                bigR = true;
            
            redB = xBig < 200 || xBig > 450;
            redS = xSmall < 300 || xSmall > 450;

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
            if (!redB)
                spriteBatch.Draw(big, bigRct, Color.Red);
            else
                spriteBatch.Draw(big, bigRct, Color.White);
            
            if (!redS)
                spriteBatch.Draw(small, smallRct, Color.Red);
            else
                spriteBatch.Draw(small, smallRct, Color.White);

            spriteBatch.Draw(station, staRct, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
