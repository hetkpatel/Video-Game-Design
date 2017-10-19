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

namespace Museum
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D museum, armsUp1, armsUp2, armsDown1, armsDown2, art;
        Rectangle museumRect, armsUpRect1, armsUpRect2, armsDownRect1, armsDownRect2, artRect;
        int armsUp1X, armsUp2X, armsDown1X, armsDown2X, artX;
        int timer;
        bool showHandsDown;

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
            showHandsDown = false;
            timer = armsUp1X = artX = 0;
            armsUp2X = 100;
            armsDown1X = 300;
            armsDown2X = 400;

            museumRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            armsUpRect1 = new Rectangle(armsUp1X, GraphicsDevice.Viewport.Height-175, 88, 175);
            armsUpRect2 = new Rectangle(armsUp2X, GraphicsDevice.Viewport.Height-175, 88, 175);
            armsDownRect1 = new Rectangle(armsDown1X, GraphicsDevice.Viewport.Height - 175, 80, 175);
            armsDownRect2 = new Rectangle(armsDown2X, GraphicsDevice.Viewport.Height - 175, 80, 175);
            artRect = new Rectangle(artX, 173, 186, 130);// (300, 173)
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
            museum = this.Content.Load<Texture2D>("museum");
            armsUp1 = this.Content.Load<Texture2D>("arms_up");
            armsUp2 = this.Content.Load<Texture2D>("arms_up");
            armsDown1 = this.Content.Load<Texture2D>("arms_down");
            armsDown2 = this.Content.Load<Texture2D>("arms_down");
            art = this.Content.Load<Texture2D>("art");
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
            timer++;
            int seconds = timer / 60;
            if (seconds < 5)
            {
                armsUp1X++;
                armsUp2X++;
                artX++;

                armsUpRect1 = new Rectangle(armsUp1X, GraphicsDevice.Viewport.Height - 175, 88, 175);
                armsUpRect2 = new Rectangle(armsUp2X, GraphicsDevice.Viewport.Height - 175, 88, 175);
                artRect = new Rectangle(artX, 173, 186, 130);
            } else if (seconds < 7)
            {
                showHandsDown = true;
            } else
            {
                armsDown1X-=2;
                armsDown2X+=2;

                armsDownRect1 = new Rectangle(armsDown1X, GraphicsDevice.Viewport.Height - 175, 80, 175);
                armsDownRect2 = new Rectangle(armsDown2X, GraphicsDevice.Viewport.Height - 175, 80, 175);
            }

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
            spriteBatch.Draw(museum, museumRect, Color.White);
            if (!showHandsDown)
            {
                spriteBatch.Draw(armsUp1, armsUpRect1, Color.Tan);
                spriteBatch.Draw(armsUp2, armsUpRect2, Color.Tan);
                spriteBatch.Draw(armsDown1, armsDownRect1, Color.Transparent);
                spriteBatch.Draw(armsDown2, armsDownRect2, Color.Transparent);
            } else
            {
                spriteBatch.Draw(armsUp1, armsUpRect1, Color.Transparent);
                spriteBatch.Draw(armsUp2, armsUpRect2, Color.Transparent);
                spriteBatch.Draw(armsDown1, armsDownRect1, Color.Tan);
                spriteBatch.Draw(armsDown2, armsDownRect2, Color.Tan);
            }
            spriteBatch.Draw(art, artRect, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
