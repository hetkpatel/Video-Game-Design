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
using System.IO;

namespace IIWGTI
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SubjectNames subjects;

        KeyboardState oldKb;
        Texture2D spriteSheet;

        Rectangle[] rects;
        string[] names;
        bool enterKeyPressed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferWidth = 1000;

            //graphics.ApplyChanges();
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
            subjects = SubjectNames.CompSci;
            rects = new Rectangle[16];
            names = new string[16];
            enterKeyPressed = false;
            oldKb = Keyboard.GetState();
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
            spriteSheet = this.Content.Load<Texture2D>("Test Image");

            ReadStringFile(@"Content/Test Image - Names.txt");
            ReadIntegerFile(@"Content/Test Image - Points.txt");
        }

        void ReadStringFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        names[i] = line;
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with file:");
                Console.WriteLine(e.Message);
            }
        }

        private void ReadIntegerFile(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(' ');
                        rects[i] = new Rectangle(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]),
                            Convert.ToInt32(parts[2]), Convert.ToInt32(parts[3]));
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with file:");
                Console.WriteLine(e.Message);
            }
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            if (kb.IsKeyDown(Keys.Space) &&
                !(oldKb.IsKeyDown(Keys.Space)))
            {
                switch (subjects)
                {
                    case SubjectNames.CompSci:
                        subjects = SubjectNames.English;
                        break;
                    case SubjectNames.English:
                        subjects = SubjectNames.Math;
                        break;
                    case SubjectNames.Math:
                        subjects = SubjectNames.PE;
                        break;
                    case SubjectNames.PE:
                        subjects = SubjectNames.CompSci;
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            switch (subjects)
            {
                case SubjectNames.CompSci:
                    if (!enterKeyPressed)
                    {
                        for (int i = 0;i < 4;i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(10, 10, rects[1].Width / 2, rects[1].Height / 2), rects[1], Color.White);
                                    break;
                                case 1:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[1].Width / 2 + 20, 10, rects[0].Width / 2, rects[0].Height / 2), rects[0], Color.White);
                                    break;
                                case 2:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[0].Width / 2 + 200, 10, rects[2].Width / 2, rects[2].Height / 2), rects[2], Color.White);
                                    break;
                                case 3:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[2].Width / 2 + 350, 10, rects[3].Width / 2, rects[3].Height / 2), rects[3], Color.White);
                                    break;
                            }
                        }
                    } else
                    {

                    }
                    break;
                case SubjectNames.English:
                    if (!enterKeyPressed)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(10, 10, rects[4].Width , rects[4].Height ), rects[4], Color.White);
                                    break;
                                case 1:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[4].Width  + 20, 10, rects[6].Width , rects[6].Height ), rects[6], Color.White);
                                    break;
                                case 2:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[6].Width + 450, 10, rects[5].Width, rects[5].Height), rects[5], Color.White);
                                    break;
                                case 3:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[5].Width + 200, 10, rects[7].Width, rects[7].Height), rects[7], Color.White);
                                    break;
                            }
                        }
                    }
                    else
                    {

                    }
                    break;
                case SubjectNames.Math:
                    if (!enterKeyPressed)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(10, 10, rects[8].Width, rects[8].Height), rects[8], Color.White);
                                    break;
                                case 1:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[8].Width + 20, 10, rects[9].Width, rects[9].Height), rects[9], Color.White);
                                    break;
                                case 2:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[9].Width + 20, 10, rects[10].Width, rects[10].Height), rects[9], Color.White);
                                    break;
                                case 3:
                                    spriteBatch.Draw(spriteSheet, new Rectangle(rects[10].Width + 100, 10, rects[11].Width, rects[11].Height), rects[11], Color.White);
                                    break;
                            }
                        }
                    }
                    else
                    {

                    }
                    break;
                case SubjectNames.PE:
                    if (!enterKeyPressed)
                    {

                    }
                    else
                    {

                    }
                    break;
                default:
                    Console.WriteLine("Error with SWITCH");
                    this.Exit();
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
