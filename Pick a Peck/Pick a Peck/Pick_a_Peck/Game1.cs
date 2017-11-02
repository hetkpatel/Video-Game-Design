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

namespace Pick_a_Peck
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D sprite;
        Rectangle sprRct;

        SpriteFont font;

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
            sprite = this.Content.Load<Texture2D>("SpriteSheet");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
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
            // Rct 1
            sprRct = new Rectangle(20, 20, 50, 50);
            Rectangle imgRct = new Rectangle(278, 0, 50, 50);
            spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
            spriteBatch.DrawString(font, "1", new Vector2(35, 30), Color.White);

            // Rct 2
            sprRct = new Rectangle(100, 20, 150, 50);
            imgRct = new Rectangle(0, 201, 150, 50);
            spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);

            // Rct 3
            sprRct = new Rectangle(300, 20, 50, 150);
            imgRct = new Rectangle(227, 0, 50, 150);
            spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);

            // Rct 4
            sprRct = new Rectangle(400, 20, 75, 200);
            imgRct = new Rectangle(151, 0, 75, 200);
            spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);

            // Rct 5
            sprRct = new Rectangle(550, 20, 150, 200);
            imgRct = new Rectangle(0, 0, 150, 200);
            spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
