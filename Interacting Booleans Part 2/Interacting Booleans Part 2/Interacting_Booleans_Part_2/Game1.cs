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
        State state;

        Rectangle heroineRect;
        Texture2D DIVING_TEXT, DUCKING_TEXT, JUMPING_TEXT, STANDING_TEXT, heroineTexture;
        bool isJumping, isDucking;
        KeyboardState oldKb = Keyboard.GetState();
        int JUMP_VELOCITY = 10;
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
            state = State.Standing;
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
            DIVING_TEXT = Content.Load<Texture2D>("diving");
            DUCKING_TEXT = Content.Load<Texture2D>("ducking");
            JUMPING_TEXT = Content.Load<Texture2D>("jumping");
            STANDING_TEXT = Content.Load<Texture2D>("standing");
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
            switch (state)
            {
                case State.Standing:
                    if (kb.IsKeyDown(Keys.J))
                    {
                        state = State.Jumping;
                        yVelocity = JUMP_VELOCITY;
                        heroineTexture = JUMPING_TEXT;
                    }
                    else if (kb.IsKeyDown(Keys.Down))
                    {
                        state = State.Ducking;
                        heroineTexture = DUCKING_TEXT;
                    }
                    break;
                case State.Jumping:

                    break;
                case State.Ducking:
                    break;
                case State.Diving:
                    break;
            }
            
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
