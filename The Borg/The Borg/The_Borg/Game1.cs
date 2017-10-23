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

        Texture2D[] gunTurrets = new Texture2D[4], torpedos = new Texture2D[4];
        Texture2D borg;
        Rectangle[] gunTurRcts = new Rectangle[4], torpedosRct = new Rectangle[4];
        Rectangle borgRct;
        SpriteFont lsuFont;

        KeyboardState oldKb = Keyboard.GetState();
        Keys[] numPad = {Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3,
                        Keys.NumPad4, Keys.NumPad5, Keys.NumPad6,
                        Keys.NumPad7, Keys.NumPad8, Keys.NumPad9};

        int delay = -1;
        int time = 0;

        int lsuEnergy = 100, mjChoice = 0;
        int index = 0, shootingIndex = -1;
        int torpedoX = 0, torpedoY = 0;

        bool[] showBorg = { false, false, false, false };

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

            borgRct = new Rectangle(0, 0, 100, 100);

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

            borg = this.Content.Load<Texture2D>("borg");

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
            time += 1;
            int seconds = time / 60;

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

            for (int i = 0; i < numPad.Length;i++)
            {
                if (kb.IsKeyDown(numPad[i]))
                {
                    mjChoice = i;
                }
            }

            // TODO: Add torpedo motion
            if (kb.IsKeyDown(Keys.Space) &&
                !oldKb.IsKeyDown(Keys.Space) && delay == -1)
            {
                switch (index)
                {
                    case 0:
                        torpedoX = 375;
                        torpedoY = 100;
                        break;
                    case 1:
                        torpedoX = 420;
                        torpedoY = 225;
                        break;
                    case 2:
                        torpedoX = 375;
                        torpedoY = 270;
                        break;
                    case 3:
                        torpedoX = 250;
                        torpedoY = 225;
                        break;
                }
                shootingIndex = index;
                delay = gameTime.TotalGameTime.Seconds;
                lsuEnergy -= mjChoice;
            }

            if (delay != -1)
            {
                if ((gameTime.TotalGameTime.Seconds - delay) < 3)
                {
                    switch (shootingIndex)
                    {
                        case 0:
                            torpedoY -= 5;
                            torpedosRct[shootingIndex] = new Rectangle(torpedoX, torpedoY, 25, 100);
                            break;
                        case 1:
                            torpedoX += 5;
                            torpedosRct[shootingIndex] = new Rectangle(torpedoX, torpedoY, 100, 25);
                            break;
                        case 2:
                            torpedoY += 5;
                            torpedosRct[shootingIndex] = new Rectangle(torpedoX, torpedoY, 25, 100);
                            break;
                        case 3:
                            torpedoX -= 5;
                            torpedosRct[shootingIndex] = new Rectangle(torpedoX, torpedoY, 100, 25);
                            break;
                    }
                }
                else
                {
                    delay = shootingIndex = -1;
                    torpedoX = torpedoY = 0;
                }
            }

            if (lsuEnergy < 100 && (time % 60) == 0)
            {
                lsuEnergy += 3;
            }
            else if (lsuEnergy > 100)
                lsuEnergy = 100;

            if (seconds == 5)
            {
                Random rnd = new Random();
                showBorg[rnd.Next(4)] = true;
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
                if (i == index && shootingIndex == -1)
                    spriteBatch.Draw(gunTurrets[i], gunTurRcts[i], Color.LightGreen);
                else
                    spriteBatch.Draw(gunTurrets[i], gunTurRcts[i], Color.Red);
            }

            spriteBatch.DrawString(lsuFont, lsuEnergy+"MJ", new Vector2(360, 220), Color.White);

            if (showBorg[0])
                borgRct = new Rectangle(350, 0, 75, 75);
            else if (showBorg[1])
                borgRct = new Rectangle(700, 200, 75, 75);
            else if (showBorg[2])
                borgRct = new Rectangle(350, 400, 75, 75);
            else if (showBorg[3])
                borgRct = new Rectangle(0, 200, 75, 75);

            if (showBorg[0] || showBorg[1] || showBorg[2] || showBorg[3])
                spriteBatch.Draw(borg, borgRct, Color.Red);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
