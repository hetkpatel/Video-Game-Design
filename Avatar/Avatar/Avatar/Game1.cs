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

namespace Avatar
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D[] avatars = new Texture2D[5];
        Texture2D backgorund;
        Rectangle[] avatarRect = new Rectangle[5];
        Rectangle backRect;

        GamePadState oldPad = GamePad.GetState(PlayerIndex.One);

        int currentSelection = 0;
        bool inSelectionMode = false;

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
            avatarRect[0] = new Rectangle(150, 100, 100, 100);
            avatarRect[1] = new Rectangle(350, 100, 100, 100);
            avatarRect[2] = new Rectangle(550, 100, 100, 100);
            avatarRect[3] = new Rectangle(250, 250, 100, 100);
            avatarRect[4] = new Rectangle(450, 250, 100, 100);
            backRect = new Rectangle(140, 90, 120, 120);

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

            avatars[0] = this.Content.Load<Texture2D>("elf");
            avatars[1] = this.Content.Load<Texture2D>("girl");
            avatars[2] = this.Content.Load<Texture2D>("glasses");
            avatars[3] = this.Content.Load<Texture2D>("tennis guy");
            avatars[4] = this.Content.Load<Texture2D>("woman");
            backgorund = this.Content.Load<Texture2D>("background");


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
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);
            // Allows the game to exit
            if (pad1.Buttons.B == ButtonState.Pressed)
                this.Exit();
            if (pad1.Buttons.Back == ButtonState.Pressed && inSelectionMode)
                inSelectionMode = false;

            // TODO: Add your update logic here
            if (!inSelectionMode)
            {
                if (pad1.DPad.Left == ButtonState.Pressed &&
                    oldPad.DPad.Left != ButtonState.Pressed)
                    currentSelection--;
                else if (pad1.DPad.Right == ButtonState.Pressed &&
                        oldPad.DPad.Right != ButtonState.Pressed)
                    currentSelection++;
            }

            if (pad1.Buttons.Start == ButtonState.Pressed)
                inSelectionMode = true;

            if (currentSelection == -1)
                currentSelection = 4;
            else if (currentSelection == 5)
                currentSelection = 0;

            oldPad = pad1;

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

            switch (currentSelection)
            {
                case 0:
                    backRect = new Rectangle(140, 90, 120, 120);
                    break;
                case 1:
                    backRect = new Rectangle(340, 90, 120, 120);
                    break;
                case 2:
                    backRect = new Rectangle(540, 90, 120, 120);
                    break;
                case 3:
                    backRect = new Rectangle(240, 240, 120, 120);
                    break;
                case 4:
                    backRect = new Rectangle(440, 240, 120, 120);
                    break;
            }
            if (!inSelectionMode)
            {
                avatarRect[0] = new Rectangle(150, 100, 100, 100);
                avatarRect[1] = new Rectangle(350, 100, 100, 100);
                avatarRect[2] = new Rectangle(550, 100, 100, 100);
                avatarRect[3] = new Rectangle(250, 250, 100, 100);
                avatarRect[4] = new Rectangle(450, 250, 100, 100);
                spriteBatch.Draw(backgorund, backRect, Color.Red);
            }
            for (int i = 0; i < avatars.Length;i++)
            {
                spriteBatch.Draw(avatars[i], avatarRect[i], Color.White);
            }
            if (inSelectionMode)
            {
                avatarRect[currentSelection] = new Rectangle(250, 100, 300, 300);
                backRect = new Rectangle(240, 90, 320, 320);
                spriteBatch.Draw(backgorund, backRect, Color.Red);
                spriteBatch.Draw(avatars[currentSelection], avatarRect[currentSelection], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
