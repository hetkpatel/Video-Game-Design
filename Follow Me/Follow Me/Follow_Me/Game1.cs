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

namespace Follow_Me
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D walk, stand;
        Rectangle manRect;
        MouseState oldMouse;
        
        bool isKeyPressed;

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
            this.IsMouseVisible = true;
            isKeyPressed = false;
            manRect = new Rectangle((GraphicsDevice.Viewport.Width / 2) - 25, (GraphicsDevice.Viewport.Height / 2) - 90, 50, 180);
            oldMouse = Mouse.GetState();

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
            walk = this.Content.Load<Texture2D>("Walking Man");
            stand = this.Content.Load<Texture2D>("Standing Man");
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
            MouseState newMouse = Mouse.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            if (newMouse.LeftButton == ButtonState.Pressed &&
                oldMouse.LeftButton == ButtonState.Released ||
                newMouse.LeftButton == ButtonState.Pressed &&
                oldMouse.LeftButton == ButtonState.Pressed)
            {
                isKeyPressed = true;
                manRect = new Rectangle(newMouse.X, newMouse.Y, 50, 180);
            } else if (newMouse.LeftButton == ButtonState.Released &&
                       oldMouse.LeftButton == ButtonState.Released)
            {
                isKeyPressed = false;
            }

            oldMouse = newMouse;
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
            if (isKeyPressed)
            {
                spriteBatch.Draw(walk, manRect, Color.White);
            } else
            {
                spriteBatch.Draw(stand, manRect, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
