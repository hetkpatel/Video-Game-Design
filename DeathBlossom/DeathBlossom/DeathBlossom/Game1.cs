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

namespace DeathBlossom
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spaceTex, missileTex;
        Rectangle screenRect;
        Rectangle gunstarRect;
        Gunstar ship;
        KeyboardState oldKB;

        List<Missile> missileList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 950;
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

            // Get current game window dimensions
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            screenRect = new Rectangle(0, 0, screenWidth, screenHeight);
            oldKB = Keyboard.GetState();

            gunstarRect = new Rectangle(500, 300, 70, 50);
            missileList = new List<Missile>();
            
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
            // Starter code
            spaceTex = Content.Load<Texture2D>("space");
            Texture2D gunstarTex = Content.Load<Texture2D>("gunstar");
            
            ship = new Gunstar(gunstarTex, gunstarRect);
            missileTex = Content.Load<Texture2D>("missile2");

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
        bool shoot = true;
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || kb.IsKeyDown(Keys.Escape))
                this.Exit();
            ship.Update(gameTime);

            // TODO: Add your update logic here
            if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space))
                ship.fire();

            //Console.WriteLine(ship.Heading);

            if (ship.IsFiring)
            {
                if (gameTime.TotalGameTime.Seconds % 0 == 0 && shoot)
                {
                    missileList.Add(new Missile(missileTex, ship.Location, ship.Heading, gunstarRect));
                    shoot = false;
                }
                if (gameTime.TotalGameTime.Seconds % 0 != 0)
                {
                    shoot = true;
                }
                Console.WriteLine(gameTime.TotalGameTime.Seconds % 2 == 0);
            }

            foreach (Missile missile in missileList)
            {
                missile.Update(gameTime);
            }

            for (int i = 0;i < missileList.Count;i++)
            {
                if (missileList[i].Location.X > screenRect.Width ||
                    missileList[i].Location.X < 0 ||
                    missileList[i].Location.Y > screenRect.Height ||
                    missileList[i].Location.Y < 0)
                {
                    missileList.RemoveAt(i);
                }
            }

            oldKB = kb;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Starter code
            spriteBatch.Begin();
            spriteBatch.Draw(spaceTex, screenRect, Color.White);
            
            // TODO: Add your drawing code here
            foreach (Missile m in missileList)
            {
                m.Draw(spriteBatch, gameTime);
            }

            ship.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
