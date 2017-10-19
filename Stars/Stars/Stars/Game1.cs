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

namespace Stars
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont timerFont;
        Texture2D ball, court;
        Rectangle ballRect, courtRect;
        double x, y;
        int timer, seconds;
        double intXV, intYV;
        const float ANGLE = 45;
        const double VEL = 88;

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
            timer = 0;
            seconds = 0;
            x = 100.0;
            y = 200.0;
            ballRect = new Rectangle((int) x, (int) y, 50, 50);
            courtRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            intXV = VEL * Math.Cos(ANGLE);
            intYV = VEL * Math.Sin(ANGLE);

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
            timerFont = this.Content.Load<SpriteFont>("TimerFont");
            ball = this.Content.Load<Texture2D>("basketball");
            court = this.Content.Load<Texture2D>("court");
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
            double second = timer / 60.0;
            if (second < 5)
            {
                seconds = (int)(5 - second);
            }
            else
            {
                second -= 5.0;
                x = intXV * second;
                y = intYV * second - 9.8 * Math.Pow(second, 2)/2;
                Console.Write("Y: " + y + "\n");

                ballRect = new Rectangle((int) x, (int) (200 - y), 50, 50);
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
            spriteBatch.Draw(court, courtRect, Color.White);
            spriteBatch.Draw(ball, ballRect, Color.White);

            spriteBatch.DrawString(timerFont, seconds + "s", new Vector2(350, 270), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
