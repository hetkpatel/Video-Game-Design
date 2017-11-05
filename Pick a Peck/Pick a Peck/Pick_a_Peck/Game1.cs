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

namespace Pick_a_Peck
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D sprite;

        SpriteFont font;

        bool showSelected = false;
        int selectedBox = -1;

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
            sprite = this.Content.Load<Texture2D>("SpriteSheet");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
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
            if (!showSelected)
            {
                if (kb.IsKeyDown(Keys.D1) || kb.IsKeyDown(Keys.NumPad1))
                {
                    selectedBox = 1;
                    showSelected = true;
                }
                else if (kb.IsKeyDown(Keys.D2) || kb.IsKeyDown(Keys.NumPad2))
                {
                    selectedBox = 2;
                    showSelected = true;
                }
                else if (kb.IsKeyDown(Keys.D3) || kb.IsKeyDown(Keys.NumPad3))
                {
                    selectedBox = 3;
                    showSelected = true;
                }
                else if (kb.IsKeyDown(Keys.D4) || kb.IsKeyDown(Keys.NumPad4))
                {
                    selectedBox = 4;
                    showSelected = true;
                }
                else if (kb.IsKeyDown(Keys.D5) || kb.IsKeyDown(Keys.NumPad5))
                {
                    selectedBox = 5;
                    showSelected = true;
                }
            } else if (kb.IsKeyDown(Keys.Back))
            {
                showSelected = false;
                selectedBox = -1;
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
            if (!showSelected)
            {
                int textY = 130;
                // Rct 1
                Rectangle sprRct = new Rectangle(20, 20, 50, 50);
                Rectangle imgRct = new Rectangle(278, 0, 50, 50);
                spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                spriteBatch.DrawString(font, "1", new Vector2(40, textY), Color.Black);

                // Rct 2
                sprRct = new Rectangle(100, 20, 150, 50);
                imgRct = new Rectangle(0, 201, 150, 50);
                spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                spriteBatch.DrawString(font, "2", new Vector2(170, textY), Color.Black);

                // Rct 3
                sprRct = new Rectangle(300, 20, 25, 75);
                imgRct = new Rectangle(227, 0, 50, 150);
                spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                spriteBatch.DrawString(font, "3", new Vector2(307, textY), Color.Black);

                // Rct 4
                sprRct = new Rectangle(400, 20, 38, 100);
                imgRct = new Rectangle(151, 0, 75, 200);
                spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                spriteBatch.DrawString(font, "4", new Vector2(414, textY), Color.Black);

                // Rct 5
                sprRct = new Rectangle(550, 20, 75, 100);
                imgRct = new Rectangle(0, 0, 150, 200);
                spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                spriteBatch.DrawString(font, "5", new Vector2(582, textY), Color.Black);
            } else
            {
                Rectangle sprRct = new Rectangle();
                Rectangle imgRct = new Rectangle();
                switch (selectedBox)
                {
                    case 1:
                        sprRct = new Rectangle(GraphicsDevice.Viewport.Width/2 - 50, GraphicsDevice.Viewport.Height/2 - 50, 100, 100);
                        imgRct = new Rectangle(278, 0, 50, 50);
                        spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                        break;
                    case 2:
                        sprRct = new Rectangle(GraphicsDevice.Viewport.Width/2 - 150, GraphicsDevice.Viewport.Height/2 - 50, 300, 100);
                        imgRct = new Rectangle(0, 201, 150, 50);
                        spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                        break;
                    case 3:
                        sprRct = new Rectangle(GraphicsDevice.Viewport.Width/2 - 25, GraphicsDevice.Viewport.Height/2 - 75, 50, 150);
                        imgRct = new Rectangle(227, 0, 50, 150);
                        spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                        break;
                    case 4:
                        sprRct = new Rectangle(GraphicsDevice.Viewport.Width/2 - 38, GraphicsDevice.Viewport.Height/2 - 100, 75, 200);
                        imgRct = new Rectangle(151, 0, 75, 200);
                        spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                        break;
                    case 5:
                        sprRct = new Rectangle(GraphicsDevice.Viewport.Width/2 - 75, GraphicsDevice.Viewport.Height/2- 100, 150, 200);
                        imgRct = new Rectangle(0, 0, 150, 200);
                        spriteBatch.Draw(sprite, sprRct, imgRct, Color.White);
                        break;
                }
                spriteBatch.DrawString(font, "Press BACKSPACE to go back.", new Vector2(250, 400), Color.Black);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
