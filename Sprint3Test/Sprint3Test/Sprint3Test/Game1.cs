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

namespace Sprint3Test
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState oldKb = Keyboard.GetState();

        Texture2D car1, car2, raceTrack;
        Rectangle car1Rct, car2Rct, rtRct;
        SpriteFont titleFont, menuFont;
        SoundEffect blowOut;

        bool startGame = false;
        bool car1Won = false, car2Won = false;

        int timer = 0, seconds = 0, newSec = 0;
        int car1PO = 0, car2PO = 0;
        int car1X = 0, car2X = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
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
            car1Rct = new Rectangle(100, 500, 115, 100);
            car2Rct = new Rectangle(800, 500, 115, 100);

            rtRct = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

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

            car1 = this.Content.Load<Texture2D>("Race Car #1");
            car2 = this.Content.Load<Texture2D>("Race Car #2");
            raceTrack = this.Content.Load<Texture2D>("Race Track");
            titleFont = this.Content.Load<SpriteFont>("TitleFont");
            menuFont = this.Content.Load<SpriteFont>("MenuFont");
            blowOut = this.Content.Load<SoundEffect>("BlownEngineSound");

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
            KeyboardState kb = Keyboard.GetState();
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            if (kb.IsKeyDown(Keys.Space))
            {
                startGame = true;
            }

            if (startGame)
            {
                timer++;
                seconds = 5 - (timer / 60);

                if (seconds < 0)
                {
                    newSec = (timer / 60) - 5;
                    if (kb.IsKeyDown(Keys.Up) &&
                        !oldKb.IsKeyDown(Keys.Up))
                    {
                        car2PO++;
                    }
                    if (kb.IsKeyDown(Keys.W) &&
                        !oldKb.IsKeyDown(Keys.W))
                    {
                        car1PO++;
                    }
                    if (kb.IsKeyDown(Keys.Down) &&
                        !oldKb.IsKeyDown(Keys.Down))
                    {
                        car2PO--;
                    }
                    if (kb.IsKeyDown(Keys.S) &&
                        !oldKb.IsKeyDown(Keys.S))
                    {
                        car1PO--;
                    }

                    if (car1PO >= 11)
                        car1PO = 10;
                    else if (car1PO <= -1)
                        car1PO = 0;

                    if (car2PO >= 11)
                        car2PO = 10;
                    else if (car2PO <= -1)
                        car2PO = 0;

                    Random rnd = new Random();
                    int chance = rnd.Next(101);
                    switch (car1PO)
                    {
                        case 5:
                            if (chance <= 5)
                            {
                                car1PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 6:
                            if (chance <= 10)
                            {
                                car1PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 7:
                            if (chance <= 15)
                            {
                                car1PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 8:
                            if (chance <= 20)
                            {
                                car1PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 9:
                            if (chance <= 25)
                            {
                                car1PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 10:
                            if (chance <= 30)
                            {
                                car1PO = 0;
                                blowOut.Play();
                            }
                            break;
                        default:
                            break;
                    }

                    switch (car2PO)
                    {
                        case 5:
                            if (chance <= 5)
                            {
                                car2PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 6:
                            if (chance <= 10)
                            {
                                car2PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 7:
                            if (chance <= 15)
                            {
                                car2PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 8:
                            if (chance <= 20)
                            {
                                car2PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 9:
                            if (chance <= 25)
                            {
                                car2PO = 0;
                                blowOut.Play();
                            }
                            break;
                        case 10:
                            if (chance <= 30)
                            {
                                car2PO = 0;
                                blowOut.Play();
                            }
                            break;
                        default:
                            break;
                    }

                    car1X += rnd.Next(car1PO + 1);
                    car2X += rnd.Next(car2PO + 1);

                    if (car1X >= 1200)
                        car1Won = true;
                    else if (car2X >= 1200)
                        car2Won = true;
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
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            if (startGame)
            {
                GraphicsDevice.Clear(Color.Black);
                if (seconds >= 0)
                    spriteBatch.DrawString(titleFont, seconds + "", new Vector2(260, 300), Color.White);
                else if (!car1Won && !car2Won)
                {
                    spriteBatch.Draw(raceTrack, rtRct, Color.White);
                    car1Rct = new Rectangle(car1X, 275, 115, 100);
                    spriteBatch.Draw(car1, car1Rct, Color.White);
                    car2Rct = new Rectangle(car2X, 500, 115, 100);
                    spriteBatch.Draw(car2, car2Rct, Color.White);
                    spriteBatch.DrawString(titleFont, newSec + "sec", new Vector2(1100, 100), Color.White);
                    spriteBatch.DrawString(menuFont, "Car1: " + car1PO, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(menuFont, "Car2: " + car2PO, new Vector2(1000, 0), Color.White);
                } else
                {

                }
            } else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.DrawString(titleFont, "Welcome to the CTE Center Raceway", new Vector2(260, 300), Color.White);
                spriteBatch.DrawString(menuFont, "Hit the SPACE to begin", new Vector2(450, 340), Color.White);
                spriteBatch.Draw(car1, car1Rct, Color.White);
                spriteBatch.DrawString(menuFont, "W - increase speed\nS - decrease speed", new Vector2(70, 600), Color.White);
                spriteBatch.Draw(car2, car2Rct, Color.White);
                spriteBatch.DrawString(menuFont, "UP ARROW - increase speed\nDOWN ARROW - decrease speed", new Vector2(770, 600), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
