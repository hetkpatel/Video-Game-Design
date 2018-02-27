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

namespace Interacting_Booleans_Part_2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle heroineRect;
        Texture2D divingText, duckingText, jumpingText, standingText, heroineTexture;
        bool isJumping, isDucking;
        KeyboardState oldKb = Keyboard.GetState();
        int jumpVelocity = 10;
        int yVelocity;

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
            heroineRect = new Rectangle(350, 150, 100, 200);
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
            divingText = Content.Load<Texture2D>("diving");
            duckingText = Content.Load<Texture2D>("ducking");
            jumpingText = Content.Load<Texture2D>("jumping");
            standingText = Content.Load<Texture2D>("standing");
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
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.J))
            {
                if (!isJumping && !isDucking)
                {
                    isJumping = true;
                    yVelocity = jumpVelocity;
                    heroineTexture = jumpingText;
                    heroineRect.Y -= 100;
                }
            }
            else if (!(kb.IsKeyDown(Keys.J)))
            {
                isJumping = false;
                heroineTexture = standingText;
                heroineRect.Y = 200;
            }
            if (kb.IsKeyDown(Keys.Down))
            {
                if (!isJumping)
                {
                    isDucking = true;
                    heroineTexture = duckingText;
                }
                else
                {
                    isJumping = false;
                    heroineTexture = divingText;
                    heroineRect.Y = 200;
                }
            }

            else if (!(kb.IsKeyDown(Keys.Down)))
            {
                if (isDucking)
                {
                    isDucking = false;
                    heroineTexture = standingText;
                }

            }
            // TODO: Add your update logic here
            oldKb = kb;

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
            spriteBatch.Draw(heroineTexture, heroineRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
