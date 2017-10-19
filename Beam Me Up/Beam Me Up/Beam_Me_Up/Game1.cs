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

namespace Beam_Me_Up
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont titleFont;
        SoundEffect[] sounds = new SoundEffect[24];
        String[] soundNames = new String[24];
        KeyboardState oldKb = Keyboard.GetState();

        Texture2D backgorund;
        Rectangle backgrndRect;

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
            soundNames[0] = "alert10";
            soundNames[1] = "autodestructsequencearmed_ep";
            soundNames[2] = "borg_cut_clean";
            soundNames[3] = "commandcodesverified_ep";
            soundNames[4] = "ds9_door";
            soundNames[5] = "klingon_torpedo_clean";
            soundNames[6] = "tng_phaser3_clean";
            soundNames[7] = "tng_replicator";
            soundNames[8] = "tng_torpedo_clean";
            soundNames[9] = "tng_transporter1";
            soundNames[10] = "tng_tricorder10";
            soundNames[11] = "tos_chirp_2";
            soundNames[12] = "tos_dilithium_burnout";
            soundNames[13] = "tos_keypress1";
            soundNames[14] = "tos_keypress2";
            soundNames[15] = "tos_keypress3";
            soundNames[16] = "tos_keypress4";
            soundNames[17] = "tos_many_tribble";
            soundNames[18] = "tos_phaser_ricochet";
            soundNames[19] = "tos_phaser_stun";
            soundNames[20] = "tos_photon_torpedo";
            soundNames[21] = "tos_red_alert_3";
            soundNames[22] = "tos_tribble_angry";
            soundNames[23] = "tos_tribble_coos";

            backgrndRect = new Rectangle(500, 100, 240, 150);

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
            for (int i = 0;i < 24;i++)
            {
                sounds[i] = this.Content.Load<SoundEffect>(soundNames[i]);
            }
            titleFont = this.Content.Load<SpriteFont>("SongTitle");
            backgorund = this.Content.Load<Texture2D>("star-trek");
            this.IsMouseVisible = true;

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            if (kb.IsKeyDown(Keys.F1) &&
                !oldKb.IsKeyDown(Keys.F1))
                sounds[0].Play();
            else if (kb.IsKeyDown(Keys.F2) &&
                !oldKb.IsKeyDown(Keys.F2))
                sounds[1].Play();
            else if (kb.IsKeyDown(Keys.F3) &&
                !oldKb.IsKeyDown(Keys.F3))
                sounds[2].Play();
            else if (kb.IsKeyDown(Keys.F4) &&
                !oldKb.IsKeyDown(Keys.F4))
                sounds[3].Play();
            else if (kb.IsKeyDown(Keys.F5) &&
                !oldKb.IsKeyDown(Keys.F5))
                sounds[4].Play();
            else if (kb.IsKeyDown(Keys.F6) &&
                !oldKb.IsKeyDown(Keys.F6))
                sounds[5].Play();
            else if (kb.IsKeyDown(Keys.F7) &&
                !oldKb.IsKeyDown(Keys.F7))
                sounds[6].Play();
            else if (kb.IsKeyDown(Keys.F8) &&
                !oldKb.IsKeyDown(Keys.F8))
                sounds[7].Play();
            else if (kb.IsKeyDown(Keys.F9) &&
                !oldKb.IsKeyDown(Keys.F9))
                sounds[8].Play();
            else if (kb.IsKeyDown(Keys.F10) &&
                !oldKb.IsKeyDown(Keys.F10))
                sounds[9].Play();
            else if (kb.IsKeyDown(Keys.F11) &&
                !oldKb.IsKeyDown(Keys.F11))
                sounds[10].Play();
            else if (kb.IsKeyDown(Keys.F12) &&
                !oldKb.IsKeyDown(Keys.F12))
                sounds[11].Play();
            else if (kb.IsKeyDown(Keys.A) &&
                !oldKb.IsKeyDown(Keys.A))
                sounds[12].Play();
            else if (kb.IsKeyDown(Keys.B) &&
                !oldKb.IsKeyDown(Keys.B))
                sounds[13].Play();
            else if (kb.IsKeyDown(Keys.C) &&
                !oldKb.IsKeyDown(Keys.C))
                sounds[14].Play();
            else if (kb.IsKeyDown(Keys.D) &&
                !oldKb.IsKeyDown(Keys.D))
                sounds[15].Play();
            else if (kb.IsKeyDown(Keys.E) &&
                !oldKb.IsKeyDown(Keys.E))
                sounds[16].Play();
            else if (kb.IsKeyDown(Keys.F) &&
                !oldKb.IsKeyDown(Keys.F))
                sounds[17].Play();
            else if (kb.IsKeyDown(Keys.G) &&
                !oldKb.IsKeyDown(Keys.G))
                sounds[18].Play();
            else if (kb.IsKeyDown(Keys.H) &&
                !oldKb.IsKeyDown(Keys.H))
                sounds[19].Play();
            else if (kb.IsKeyDown(Keys.I) &&
                !oldKb.IsKeyDown(Keys.I))
                sounds[20].Play();
            else if (kb.IsKeyDown(Keys.J) &&
                !oldKb.IsKeyDown(Keys.J))
                sounds[21].Play();
            else if (kb.IsKeyDown(Keys.K) &&
                !oldKb.IsKeyDown(Keys.K))
                sounds[22].Play();
            else if (kb.IsKeyDown(Keys.L) &&
                !oldKb.IsKeyDown(Keys.L))
                sounds[23].Play();

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
            spriteBatch.Draw(backgorund, backgrndRect, Color.White);

            for(int i = 0;i < 12;i++)
            {
                spriteBatch.DrawString(titleFont, String.Concat(String.Concat("F", i + 1), String.Concat(" - ", soundNames[i])), new Vector2(140, i * 19), Color.White);
            }

            int charNum = 65;
            for (int i = 12; i < 24; i++)

            {
                spriteBatch.DrawString(titleFont, String.Concat((char)charNum, String.Concat(" - ", soundNames[i])), new Vector2(140, i * 19), Color.White);
                charNum++;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
