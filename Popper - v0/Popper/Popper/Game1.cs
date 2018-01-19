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

namespace Popper
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle window;
        Texture2D unpoppedTex;
        Texture2D poppedTex;

        List<Rectangle> kernels;
        List<Vector2> velocities;
        List<Texture2D> images;
        List<int> timers;
        
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
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            window = new Rectangle(0, 0, screenWidth, screenHeight);

            kernels = new List<Rectangle>();
            velocities = new List<Vector2>();
            images = new List<Texture2D>();
            timers = new List<int>();

            kernels.Add(new Rectangle(70, 50, 15, 15));
            velocities.Add(new Vector2(2, 3));
            timers.Add(0);

            kernels.Add(new Rectangle(70, 110, 15, 15));
            velocities.Add(new Vector2(2, 3));
            timers.Add(0);


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
            unpoppedTex = Content.Load<Texture2D>("unpopped");
            poppedTex = Content.Load<Texture2D>("popped");

            images.Add(unpoppedTex);
            images.Add(unpoppedTex);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            for (int i=0; i < kernels.Count; i++)
            {
                int x = kernels[i].X + (int)velocities[i].X;
                int y = kernels[i].Y + (int)velocities[i].Y;
                kernels[i] = new Rectangle(x, y, kernels[i].Width, kernels[i].Height);
                if (kernels[i].Y + kernels[i].Height >= window.Bottom ||
                    kernels[i].Y <= window.Top)
                {
                    velocities[i] = new Vector2(velocities[i].X, velocities[i].Y * -1);
                }
                if (kernels[i].X + kernels[i].Width>= window.Right ||
                    kernels[i].X <= window.Left)
                {
                    velocities[i] = new Vector2(velocities[i].X * -1, velocities[i].Y);
                }
                for (int j = 0;j < kernels.Count;j++)
                {
                    if (j != i)
                    {
                        if (kernels[i].Top == kernels[j].Bottom ||
                            kernels[i].Bottom == kernels[j].Top)
                        {
                            velocities[i] = new Vector2(velocities[i].X, velocities[i].Y * -1);
                            velocities[j] = new Vector2(velocities[j].X, velocities[j].Y * -1);
                            if (timers[i] == 0)
                                timers[i] = 45;
                            if (timers[j] == 0)
                                timers[j] = 45;
                            images[i] = images[j] = poppedTex;
                        }
                        if (kernels[i].Left == kernels[j].Right ||
                            kernels[i].Right == kernels[j].Left)
                        {
                            velocities[i] = new Vector2(velocities[i].X * -1, velocities[i].Y);
                            velocities[j] = new Vector2(velocities[j].X * -1, velocities[j].Y);
                            if (timers[i] == 0)
                                timers[i] = 45;
                            if (timers[j] == 0)
                                timers[j] = 45;
                            images[i] = images[j] = poppedTex;
                        }
                    }
                }
            }
            for (int i = 0; i < kernels.Count; i++)
            {
                timers[i]--;
                if (timers[i] == 0 && images[i] == poppedTex)
                {
                    kernels.RemoveAt(i);
                    velocities.RemoveAt(i);
                    images.RemoveAt(i);
                    timers.RemoveAt(i);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int i = 0; i < kernels.Count; i++)
            {
                spriteBatch.Draw(images[i], kernels[i], Color.White);
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
