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

namespace War
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        List<Card> cards;

        bool pick = false;

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
            cards = new List<Card>(52);

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

            // Clubs
            for (int i = 0; i < cards.Capacity / 4; i++)
            {
                String index = String.Concat("", (i + 1));
                if (i <= 8)
                    index = String.Concat("0", index);
                Card c = new Card(this.Content);
                c.LoadContent(String.Concat("c", index), "Clubs", index);
                cards.Add(c);
            }
            // Diamonds
            for (int i = 0; i < cards.Capacity / 4; i++)
            {
                String index = String.Concat("", (i + 1));
                if (i <= 8)
                    index = String.Concat("0", index);
                Card c = new Card(this.Content);
                c.LoadContent(String.Concat("d", index), "Diamonds", index);
                cards.Add(c);
            }
            // Hearts
            for (int i = 0; i < cards.Capacity / 4; i++)
            {
                String index = String.Concat("", (i + 1));
                if (i <= 8)
                    index = String.Concat("0", index);
                Card c = new Card(this.Content);
                c.LoadContent(String.Concat("h", index), "Hearts", index);
                cards.Add(c);
            }
            // Spades
            for (int i = 0; i < cards.Capacity / 4; i++)
            {
                String index = String.Concat("", (i + 1));
                if (i <= 8)
                    index = String.Concat("0", index);
                Card c = new Card(this.Content);
                c.LoadContent(String.Concat("s", index), "Spades", index);
                cards.Add(c);
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
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                pick = false;

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        public List<T> Shuffle_Cards<T>(T Value, List<T> CList)
        {
            // Local Vars
            int I, R;
            bool Flag;
            Random Rand = new Random();
            // Local List of T type
            var CardList = new List<T>();
            // Build Local List as big as passed in list and fill it with default value
            for (I = 0; I < CList.Count; I++)
                CardList.Add(Value);
            // Shuffle the list of cards
            for (I = 0; I < CList.Count; I++)
            {
                Flag = false;
                // Loop until an empty spot is found
                do
                {
                    R = Rand.Next(0, CList.Count);
                    if (CardList[R].Equals(Value))
                    {
                        Flag = true;
                        CardList[R] = CList[I];
                    }
                } while (!Flag);
            }
            // Return the shuffled list
            return CardList;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        int index1, index2;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            
            if (!pick)
            {
                Random rnd = new Random();
                cards = Shuffle_Cards(cards[0], cards);
                index2 = rnd.Next(52);
                cards = Shuffle_Cards(cards[0], cards);
                index1 = rnd.Next(52);
                pick = true;
            }

            cards[index1].Draw(graphics.GraphicsDevice, 0);
            cards[index2].Draw(graphics.GraphicsDevice, 1);

            base.Draw(gameTime);
        }
    }
}
