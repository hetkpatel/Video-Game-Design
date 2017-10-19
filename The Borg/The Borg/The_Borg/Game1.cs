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

namespace The_Borg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D[] gunTurrets = new Texture2D[4];
        Texture2D[] torpedos = new Texture2D[4];
        Rectangle[] gunTurRcts = new Rectangle[4];
        Rectangle[] torpedosRct = new Rectangle[4];
        SpriteFont lsuFont;

        KeyboardState oldKb = Keyboard.GetState();

        int delay = -1;

        int lsuEnergy = 100;
        int index = 0, shootingIndex = -1;
        int torpedoX = 0, torpedoY = 0;

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
            gunTurRcts[0] = new Rectangle(350, 100, 78, 100);
            gunTurRcts[1] = new Rectangle(420, 200, 100, 78);
            gunTurRcts[2] = new Rectangle(350, 270, 78, 100);
            gunTurRcts[3] = new Rectangle(250, 200, 100, 78);
            
            torpedosRct[0] = new Rectangle(375, 100, 25, 100);
            torpedosRct[1] = new Rectangle(420, 225, 100, 25);
            torpedosRct[2] = new Rectangle(375, 270, 25, 100);
            torpedosRct[3] = new Rectangle(250, 225, 100, 25);

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
            gunTurrets[0] = this.Content.Load<Texture2D>("gun_turret_up");
            gunTurrets[1] = this.Content.Load<Texture2D>("gun_turret_right");
            gunTurrets[2] = this.Content.Load<Texture2D>("gun_turret_down");
            gunTurrets[3] = this.Content.Load<Texture2D>("gun_turret_left");

            torpedos[0] = this.Content.Load<Texture2D>("torpedo_up");
            torpedos[1] = this.Content.Load<Texture2D>("torpedo_right");
            torpedos[2] = this.Content.Load<Texture2D>("torpedo_down");
            torpedos[3] = this.Content.Load<Texture2D>("torpedo_left");

            lsuFont = this.Content.Load<SpriteFont>("LSUFont");
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
            if (kb.IsKeyDown(Keys.Up) &&
                !oldKb.IsKeyDown(Keys.Up))
                index = 0;
            else if (kb.IsKeyDown(Keys.Right) &&
                !oldKb.IsKeyDown(Keys.Right))
                index = 1;
            else if (kb.IsKeyDown(Keys.Down) &&
                !oldKb.IsKeyDown(Keys.Down))
                index = 2;
            else if (kb.IsKeyDown(Keys.Left) &&
                !oldKb.IsKeyDown(Keys.Left))
                index = 3;

            // TODO: Add torpedo motion
            if (kb.IsKeyDown(Keys.Space) &&
                !oldKb.IsKeyDown(Keys.Space))
            {
                //switch (index)
                //{
                //    case 0:
                //        // UP MOTION
                //        break;
                //    case 1:
                //        // RIGHT MOTION
                //        break;
                //    case 2:
                //        // DOWN MOTION
                //        break;
                //    case 3:
                //        // LEFT MOTION
                //        break;
                //}
                shootingIndex = index;

                delay = gameTime.TotalGameTime.Seconds;
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
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            if (shootingIndex != -1)
                spriteBatch.Draw(torpedos[shootingIndex], torpedosRct[shootingIndex], Color.White);

            for (int i = 0; i < 4; i++) {
                if (i == index)
                    spriteBatch.Draw(gunTurrets[i], gunTurRcts[i], Color.LightGreen);
                else
                    spriteBatch.Draw(gunTurrets[i], gunTurRcts[i], Color.Red);
            }
            spriteBatch.DrawString(lsuFont, lsuEnergy+"MJ", new Vector2(360, 220), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
