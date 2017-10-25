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

namespace More_Borg
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D[] gunTurrets = new Texture2D[4];
        Texture2D torpedo;
        Texture2D borg;
        Rectangle[] gunTurRcts = new Rectangle[4];
        Rectangle torpedoRct;
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

            //torpedosRct = new Rectangle(375, 100, 25, 100);

            //borgRct = new Rectangle(0, 0, 100, 100);

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

            torpedo = this.Content.Load<Texture2D>("white");

            borg = this.Content.Load<Texture2D>("white");

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
            double seconds = time / 60.0;

            // Arrow Logic
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

            // Power Logic
            for (int i = 0; i < numPad.Length; i++)
            {
                if (kb.IsKeyDown(numPad[i]))
                {
                    mjChoice = i;
                }
            }

            // Torpedo Motion Logic
            if (kb.IsKeyDown(Keys.Space) &&
                !oldKb.IsKeyDown(Keys.Space) && delay == -1)
            {
                switch (index)
                {
                    case 0:
                        torpedoX = 385;
                        torpedoY = 115;
                        break;
                    case 1:
                        torpedoX = 430;
                        torpedoY = 235;
                        break;
                    case 2:
                        torpedoX = 385;
                        torpedoY = 340;
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
                            torpedoY -= 2;
                            torpedoRct = new Rectangle(torpedoX, torpedoY, 5, 5);
                            break;
                        case 1:
                            torpedoX += 3;
                            torpedoRct = new Rectangle(torpedoX, torpedoY, 5, 5);
                            break;
                        case 2:
                            torpedoY += 2;
                            torpedoRct = new Rectangle(torpedoX, torpedoY, 5, 5);
                            break;
                        case 3:
                            torpedoX -= 3;
                            torpedoRct = new Rectangle(torpedoX, torpedoY, 5, 5);
                            break;
                    }
                }
                else
                {
                    delay = shootingIndex = -1;
                    torpedoX = torpedoY = 0;
                }
            }

            // LSU Energy Logic
            if (lsuEnergy < 100 && (time % 60) == 0)
            {
                lsuEnergy += 3;
            }
            else if (lsuEnergy > 100)
                lsuEnergy = 100;

            // Borg Apperence Logic
            if ((seconds % 5.0) == 0 || seconds == 0.1)
            {
                Random rnd = new Random();
                int randNum = rnd.Next(4);
                switch (randNum)
                {
                    case 0:
                        borgRct = new Rectangle(363, 0, 50, 50);
                        break;
                    case 1:
                        borgRct = new Rectangle(700, 213, 50, 50);
                        break;
                    case 2:
                        borgRct = new Rectangle(363, 400, 50, 50);
                        break;
                    case 3:
                        borgRct = new Rectangle(0, 213, 50, 50);
                        break;
                    default:
                        this.Exit();
                        break;
                }
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
                spriteBatch.Draw(torpedo, torpedoRct, Color.White);

            for (int i = 0; i < 4; i++)
            {
                if (i == index && shootingIndex == -1)
                    spriteBatch.Draw(gunTurrets[i], gunTurRcts[i], Color.LightGreen);
                else
                    spriteBatch.Draw(gunTurrets[i], gunTurRcts[i], Color.Red);
            }

            spriteBatch.DrawString(lsuFont, lsuEnergy + "MJ", new Vector2(360, 220), Color.White);

            if (gameTime.TotalGameTime.Seconds >= 0.2)
                spriteBatch.Draw(borg, borgRct, Color.Red);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
