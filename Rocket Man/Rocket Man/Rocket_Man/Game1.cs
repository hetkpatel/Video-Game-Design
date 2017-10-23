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

namespace Rocket_Man
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D rocketman;
        double velocity, degrees;
        double x;
        double y;
        SpriteFont font1;

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
            velocity = 1;
            x = 400;
            y = 100;
            degrees = 0;

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
            rocketman = this.Content.Load<Texture2D>("rocket");
            font1 = this.Content.Load<SpriteFont>("SpriteFont1");
            // TODO: use this.Content to load your game content here
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
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            velocity += pad1.ThumbSticks.Left.Y * 0.5;
            if (velocity > 0)
            {
                if (pad1.ThumbSticks.Right.X > 0)
                {
                    x += velocity;
                }
                if (pad1.ThumbSticks.Right.X < 0)
                {
                    x -= velocity;
                }
                if (pad1.ThumbSticks.Right.Y > 0)
                {
                    y -= velocity;
                }
                if (pad1.ThumbSticks.Right.Y < 0)
                {
                    y += velocity;
                }
                if (y < 0)
                {
                    y = GraphicsDevice.Viewport.Height - 20;
                }
                if (y > GraphicsDevice.Viewport.Height + 20)
                {
                    y = -10;
                }
                if (x < 0)
                {
                    x = GraphicsDevice.Viewport.Width - 20;
                }
                if (x > GraphicsDevice.Viewport.Width + 20)
                {
                    x = -10;
                }
            }
            if (velocity < 0)
                velocity = 0;
            if (velocity > 100)
                velocity = 100;
            double y1 = pad1.ThumbSticks.Right.Y;
            double x1 = pad1.ThumbSticks.Right.X;
            degrees = MathHelper.ToDegrees((float)Math.Atan2(y1, x1));
            degrees -= 90;
            degrees *= -1;
            if (degrees < 0)
            {
                degrees += 360;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(rocketman, new Rectangle((int)x, (int)y, 100, 100), Color.White);
            spriteBatch.DrawString(font1, "degrees:" + degrees, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font1, "velocity:" + velocity, new Vector2(0, 20), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
