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

namespace Rock_and_Hard_Place
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D rock, hardPlc;
        Rectangle rckRct, hardRct;

        int x, y;
        bool red = false;

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
            x = 100;
            y = GraphicsDevice.Viewport.Height / 2 - 50;
            rckRct = new Rectangle(x, y, 100, 100);
            hardRct = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 125, GraphicsDevice.Viewport.Height / 2 - 125, 250, 250);

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
            rock = this.Content.Load<Texture2D>("Rock");
            hardPlc = this.Content.Load<Texture2D>("Hard Place");
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

            // TODO: Add your update logic here
            if (kb.IsKeyDown(Keys.Left)) x -= 10;
            if (kb.IsKeyDown(Keys.Right)) x += 10;
            if (kb.IsKeyDown(Keys.Up)) y -= 10;
            if (kb.IsKeyDown(Keys.Down)) y += 10;

            rckRct = new Rectangle(x, y, 100, 100);
            red = isOverlapping(rckRct, hardRct);

            base.Update(gameTime);
        }

        private Boolean isOverlapping(Rectangle rec1, Rectangle rec2)
        {
            if ((rec1.X + rec1.Width) > rec2.X && rec1.X < (rec2.X + rec2.Width) &&
                (rec1.Y + rec1.Height) > rec2.Y && rec1.Y < (rec2.Y + rec2.Height))
                return true;
            else
                return false;
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
            spriteBatch.Draw(hardPlc, hardRct, Color.White);
            if (red) spriteBatch.Draw(rock, rckRct, Color.Red);
            else spriteBatch.Draw(rock, rckRct, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
