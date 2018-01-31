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

namespace Bounce_House
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rnd = new Random();
        Rectangle window;

        Texture2D circle, diamond, heart, pentagon, square, star, triangle, tri2, line;
        List<Texture2D> shapeTextures = new List<Texture2D>();

        Rectangle shapeRct, lineRct;

        List<Rectangle> shapes;
        List<Texture2D> shapeTxts;
        List<Vector2> velocities;
        List<int> timers;
        List<int> hits;

        bool A, X, R, B, P, Y;
        double nextSpawn = 0.0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 800;

            graphics.ApplyChanges();
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
            A = X = R = B = P = Y = false;
            shapes = new List<Rectangle>();
            velocities = new List<Vector2>();
            shapeTxts = new List<Texture2D>();
            timers = new List<int>();
            hits = new List<int>();

            lineRct = new Rectangle(GraphicsDevice.Viewport.Width/2, 0, 1, GraphicsDevice.Viewport.Height);

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
            line = this.Content.Load<Texture2D>("White");

            circle = this.Content.Load<Texture2D>("Circle");
            diamond = this.Content.Load<Texture2D>("Diamond");
            heart = this.Content.Load<Texture2D>("Heart");
            pentagon = this.Content.Load<Texture2D>("Pentagon");
            square = this.Content.Load<Texture2D>("Square");
            star = this.Content.Load<Texture2D>("Star");
            triangle = this.Content.Load<Texture2D>("Triangle");
            tri2 = this.Content.Load<Texture2D>("Triangle 2");

            shapeTextures.Add(circle);
            shapeTextures.Add(diamond);
            shapeTextures.Add(heart);
            shapeTextures.Add(pentagon);
            shapeTextures.Add(square);
            shapeTextures.Add(star);
            shapeTextures.Add(triangle);
            shapeTextures.Add(tri2);
        }

        public void LoadShape(int texture)
        {
            shapes.Add(new Rectangle(rnd.Next(window.Width - 31), rnd.Next(window.Height - 31), 32, 32));
            velocities.Add(new Vector2(rnd.Next(1, 12), rnd.Next(1, 12)));
            shapeTxts.Add(shapeTextures[texture]);
            timers.Add(rnd.Next(2, 13) * 60);
            hits.Add(0);
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

            // TODO: Add your update logic here
            // TODO: Working on changing the height of the line randomly
            //if (gameTime.TotalGameTime.TotalSeconds % 3.0 <= (nextSpawn + 0.1) &&
            //    gameTime.TotalGameTime.TotalSeconds % 3.0 >= (nextSpawn - 0.1))
            //{
            //    Console.WriteLine
            //    lineRct = new Rectangle(GraphicsDevice.Viewport.Width / 2, 0, 1, GraphicsDevice.Viewport.Height * (rnd.Next(50, 101)*100));
            //    nextSpawn = rnd.NextDouble();
            //}
            if (shapes.Count < 3)
            {
                LoadShape(rnd.Next(0, 8));
            }
            for (int i = 0; i < shapes.Count; i++)
            {
                int x = shapes[i].X + (int)velocities[i].X;
                int y = shapes[i].Y + (int)velocities[i].Y;
                shapes[i] = new Rectangle(x, y, shapes[i].Width, shapes[i].Height);
                if (shapes[i].Y + shapes[i].Height >= window.Bottom ||
                    shapes[i].Y <= window.Top)
                {
                    velocities[i] = new Vector2(velocities[i].X, velocities[i].Y * -1);
                    hits[i]++;
                }
                if (shapes[i].X + shapes[i].Width >= window.Right ||
                    shapes[i].X <= window.Left)
                {
                    velocities[i] = new Vector2(velocities[i].X * -1, velocities[i].Y);
                    hits[i]++;
                }

                for (int j = 0; j < shapes.Count; j++)
                {
                    if (j != i)
                    {
                        if (shapes[i].Intersects(shapes[j]) && shapeTxts[i] == shapeTxts[j])
                        {
                            velocities[i] = new Vector2(velocities[i].X * -1, velocities[i].Y * -1);
                            velocities[j] = new Vector2(velocities[j].X * -1, velocities[j].Y * -1);
                            LoadShape(rnd.Next(0, 8));
                        }
                    }
                }
            }
            for (int i = 0; i < shapes.Count; i++)
            {
                timers[i]--;
                if (timers[i] == 0)
                {
                    shapes.RemoveAt(i);
                    velocities.RemoveAt(i);
                    shapeTxts.RemoveAt(i);
                    timers.RemoveAt(i);
                    hits.RemoveAt(i);
                    LoadShape(rnd.Next(0, 8));
                }
                if (hits[i] % 5 == 0 && hits[i] != 0)
                {
                    LoadShape(rnd.Next(0, 8));
                    hits[i]++;
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            spriteBatch.Draw(line, lineRct, Color.White);
            for (int i = 0;i < shapes.Count;i++)
            {
                spriteBatch.Draw(shapeTxts[i], shapes[i], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
