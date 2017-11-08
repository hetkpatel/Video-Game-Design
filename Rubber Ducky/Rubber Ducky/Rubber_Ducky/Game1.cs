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

namespace Rubber_Ducky
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteEffects sfx;

        Texture2D duckies;

        float[] rotations = new float[6];
        bool[] events = new bool[5];

        int rotateDuck = -1;
        int moveDuckS = -1, moveDuckB = -1;
        int duckSX = -1, duckBX = -1;
        bool motionDucks = false;

        bool hitB, hitS;

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
            for (int i = 0; i < rotations.Length; i++)
            {
                if (i == 4)
                    rotations[i] = 180;
                else
                    rotations[i] = 0;
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
            duckies = this.Content.Load<Texture2D>("Duckies");
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
            if (kb.IsKeyDown(Keys.A))
            {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[0] = true;
            }
            else if (kb.IsKeyDown(Keys.E))
            {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[1] = true;
            }
            else if (kb.IsKeyDown(Keys.O))
            {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[2] = true;
            }
            else if (kb.IsKeyDown(Keys.NumPad1)) {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[3] = true;
                rotateDuck = 1;
            }
            else if (kb.IsKeyDown(Keys.NumPad2))
            {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[3] = true;
                rotateDuck = 2;
            }
            else if (kb.IsKeyDown(Keys.NumPad3))
            {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[3] = true;
                rotateDuck = 3;
            }
            else if (kb.IsKeyDown(Keys.NumPad4))
            {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[3] = true;
                rotateDuck = 4;
            }
            else if (kb.IsKeyDown(Keys.NumPad5))
            {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[3] = true;
                rotateDuck = 5;
            }
            else if (kb.IsKeyDown(Keys.NumPad6))
            {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[3] = true;
                rotateDuck = 6;
            }
            else if (kb.IsKeyDown(Keys.D1) || kb.IsKeyDown(Keys.D6)) {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[4] = true;
                duckSX = 0;
                duckBX = (GraphicsDevice.Viewport.Width / 2) - 50;
                moveDuckS = 1;
                moveDuckB = 6;
            }
            else if (kb.IsKeyDown(Keys.D2) || kb.IsKeyDown(Keys.D5)) {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[4] = true;
                duckSX = 0;
                duckBX = (GraphicsDevice.Viewport.Width / 2) - 50;
                moveDuckS = 2;
                moveDuckB = 5;
            }
            else if (kb.IsKeyDown(Keys.D3) || kb.IsKeyDown(Keys.D4)) {
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                events[4] = true;
                duckSX = 0;
                duckBX = (GraphicsDevice.Viewport.Width / 2) - 50;
                moveDuckS = 3;
                moveDuckB = 4;
            }

            if (events[0])
            {
                for (int i = 0; i < rotations.Length; i++)
                    rotations[i] -= 10;
                motionDucks = false;
            }
            else if (events[1])
            {
                for (int i = 0; i < rotations.Length; i++)
                {
                    if (i % 2 != 0)
                        rotations[i] += 30;
                }
                motionDucks = false;
            }
            else if (events[2])
            {
                for (int i = 0; i < rotations.Length; i++)
                {
                    if (i % 2 == 0)
                        rotations[i] -= 30;
                }
                motionDucks = false;
            }
            else if (events[3])
            {
                rotations[rotateDuck - 1] += 30;
                motionDucks = false;
            }
            else if (events[4])
            {
                motionDucks = true;
                if (hitS)
                    duckSX -= 4;
                else
                    duckSX += 4;
                if (hitB)
                    duckBX -= 2;
                else
                    duckBX += 2;
            }

            if (duckSX == GraphicsDevice.Viewport.Width - 100)
                hitS = true;
            else if (duckSX == 0)
                hitS = false;

            if (duckBX == GraphicsDevice.Viewport.Width - 100)
                hitB = true;
            else if (duckBX == 0)
                hitB = false;

            if (kb.IsKeyDown(Keys.Space))
            {
                for (int i = 0; i < rotations.Length; i++)
                {
                    if (i == 4)
                        rotations[i] = 180;
                    else
                        rotations[i] = 0;
                }
                for (int i = 0; i < events.Length; i++)
                    events[i] = false;
                rotateDuck = moveDuckS = moveDuckB = duckBX = duckSX = -1;
                motionDucks = false;
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
            if (!motionDucks)
            {
                // 1
                spriteBatch.Draw(duckies, new Rectangle(100, 100, 100, 100), new Rectangle(0, 0, 200, 177), Color.White, (float)((Math.PI / 180) * rotations[0]), new Vector2(100), sfx, 0);
                // 2
                spriteBatch.Draw(duckies, new Rectangle(310, 100, 100, 100), new Rectangle(200, 0, 200, 177), Color.White, (float)((Math.PI / 180) * rotations[1]), new Vector2(100), sfx, 0);
                // 3
                spriteBatch.Draw(duckies, new Rectangle(510, 100, 100, 100), new Rectangle(400, 0, 200, 177), Color.White, (float)((Math.PI / 180) * rotations[2]), new Vector2(100), sfx, 0);
                // 4
                spriteBatch.Draw(duckies, new Rectangle(100, 230, 100, 100), new Rectangle(600, 0, 200, 177), Color.White, (float)((Math.PI / 180) * rotations[3]), new Vector2(100), sfx, 0);
                // 5
                spriteBatch.Draw(duckies, new Rectangle(310, 230, 100, 100), new Rectangle(800, 0, 200, 177), Color.White, (float)((Math.PI / 180) * rotations[4]), new Vector2(100), sfx, 0);
                // 6
                spriteBatch.Draw(duckies, new Rectangle(510, 230, 100, 100), new Rectangle(1000, 0, 200, 177), Color.White, (float)((Math.PI / 180) * rotations[5]), new Vector2(100), sfx, 0);
            } else
            {
                Rectangle smallRct = new Rectangle((moveDuckS * 200) - 200, 0, 200, 177);
                Rectangle bigRct = new Rectangle((moveDuckB * 200) - 200, 0, 200, 177);
                spriteBatch.Draw(duckies, new Rectangle(duckSX, (GraphicsDevice.Viewport.Height / 2) - 50, 100, 100), smallRct, Color.White);
                spriteBatch.Draw(duckies, new Rectangle(duckBX, 0, 100, 100), bigRct, Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
