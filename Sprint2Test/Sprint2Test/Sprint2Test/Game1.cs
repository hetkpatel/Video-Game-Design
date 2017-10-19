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

namespace Sprint2Test
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D[] cardArr = new Texture2D[12];
        Rectangle[] cardRectArr = new Rectangle[12];

        SpriteFont timerFont;
        Color textColor;

        int timer;
        double card1x, card2x;
        String textTimer;
        bool foward;
        bool step1, step2, step3, step5;

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
            card1x = 0.0; card2x = GraphicsDevice.Viewport.Width-55;
            textTimer = "";
            step1 = true;
            step2 = false;
            step3 = false;
            step5 = false;

            foward = true;

            for (int i = 0; i < cardRectArr.Length; i++)
            {
                cardRectArr[i] = new Rectangle(i*67, (GraphicsDevice.Viewport.Height/2)-45, 55, 90);
            }

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
            for(int i = 1;i <= 2;i++)
            {
                cardArr[i-1] = this.Content.Load<Texture2D>("Card#" + i);
            }
            for (int i = 2;i < cardArr.Length;i++)
            {
                if (i == cardArr.Length - 1)
                    cardArr[i] = this.Content.Load<Texture2D>("Uno Card Back");
                else
                    cardArr[i] = this.Content.Load<Texture2D>("Uno Card #" + (i-1));
            }

            timerFont = this.Content.Load<SpriteFont>("TimerFont");
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
            int seconds = timer / 60;
            if (seconds < 10) { }
            else if (seconds <= 15)
            {
                step1 = false; step2 = true;
                switch(seconds)
                {
                    case 10:
                        textColor = Color.Red;
                        break;
                    case 11:
                        textColor = Color.Blue;
                        break;
                    case 12:
                        textColor = Color.Green;
                        break;
                    case 13:
                        textColor = Color.Gold;
                        break;
                    case 14:
                        textColor = Color.Purple;
                        break;
                    default:
                        textColor = Color.White;
                        break;
                }
                textTimer = 5-(seconds - 10) + "";
            } else if (seconds < 140)
            {
                step2 = false; step3 = true;
                if (seconds < 40)
                {
                    card1x += 0.5;
                    card2x -= 0.5;
                } else if (seconds < 60)
                {
                    card1x -= 0.5;
                    card2x += 0.5;
                }
                else if (seconds < 80)
                {
                    card1x += 0.5;
                    card2x -= 0.5;
                }
                else if (seconds < 100)
                {
                    card1x -= 0.5;
                    card2x += 0.5;
                }
                else if (seconds < 120)
                {
                    card1x += 0.5;
                    card2x -= 0.5;
                }
                else if (seconds < 140)
                {
                    card1x -= 0.5;
                    card2x += 0.5;
                }

                cardRectArr[0] = new Rectangle((int)card1x, 0, 55, 90);
                cardRectArr[1] = new Rectangle((int)card2x, 0, 55, 90);
            }
            else if (seconds <= 145)
            {
                step3 = false; step2 = true;
                switch (seconds)
                {
                    case 140:
                        textColor = Color.White;
                        textTimer = "Five";
                        break;
                    case 141:
                        textColor = Color.Purple;
                        textTimer = "Four";
                        break;
                    case 142:
                        textColor = Color.Gold;
                        textTimer = "Three";
                        break;
                    case 143:
                        textColor = Color.Green;
                        textTimer = "Two";
                        break;
                    case 144:
                        textColor = Color.Blue;
                        textTimer = "One";
                        break;
                    default:
                        textColor = Color.Red;
                        textTimer = "Zero";
                        break;
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
            if (step5)
            {
                GraphicsDevice.Clear(Color.Blue);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
            }
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            if (step1)
            {
                for (int i = 0; i < cardArr.Length; i++)
                {
                    spriteBatch.Draw(cardArr[i], cardRectArr[i], Color.White);
                }
            }
            else if (step2)
            {
                spriteBatch.DrawString(timerFont, textTimer, new Vector2((GraphicsDevice.Viewport.Width / 2) - 50, (GraphicsDevice.Viewport.Height / 2) - 100), textColor);
            }
            else if (step3)
            {
                spriteBatch.Draw(cardArr[0], cardRectArr[0], Color.White);
                spriteBatch.Draw(cardArr[1], cardRectArr[1], Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
