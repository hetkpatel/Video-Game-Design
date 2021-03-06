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

namespace Breakout
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont menuFont, subtextFont;
        Vector2 screen;

        GameState gameState;

        Texture2D brick;

        Texture2D ball;
        Rectangle ballRct, paddleRct;
        Vector2 ballPosition, ballVelocity;
        int paddlePosition;

        Brick[,] gameGrid;
        string[,] strgrid_lv1;
        string[,] strgrid_lv2;

        readonly int[] GRID_COUNT = { 25, 9 };
        readonly int[] BLOCK_SIZE = { 50, 25 };

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GRID_COUNT[0] * BLOCK_SIZE[0];
            graphics.PreferredBackBufferHeight = GRID_COUNT[1] * BLOCK_SIZE[1] + 500;

            graphics.ApplyChanges();
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
            screen = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            gameState = GameState.MAIN_MENU;
            strgrid_lv1 = new string[GRID_COUNT[0], GRID_COUNT[1]];
            gameGrid = new Brick[GRID_COUNT[0], GRID_COUNT[1]];
            setGround();

            base.Initialize();
        }

        private void setGround()
        {
            //Random rnd = new Random();
            //ballVelocity = new Vector2(rnd.Next(5, 10), rnd.Next(5, 10));
            ballVelocity = new Vector2(5);
            ballPosition = new Vector2((GraphicsDevice.Viewport.Width / 2) - 10, GraphicsDevice.Viewport.Height - 100);
            ballRct = new Rectangle((int)ballPosition.X, (int)ballPosition.Y, 20, 20);
            paddlePosition = (GraphicsDevice.Viewport.Width / 2) - BLOCK_SIZE[0];
            paddleRct = new Rectangle(paddlePosition, GraphicsDevice.Viewport.Height - BLOCK_SIZE[1] - 5, BLOCK_SIZE[0] * 2, BLOCK_SIZE[1]);
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
            menuFont = this.Content.Load<SpriteFont>("MenuFont");
            subtextFont = this.Content.Load<SpriteFont>("SubtextFont");
            brick = this.Content.Load<Texture2D>("White");
            ball = this.Content.Load<Texture2D>("Ball");

            ReadFileAsString(@"Content/level1.txt", strgrid_lv1);
            ReadFileAsString(@"Content/level2.txt", strgrid_lv2);
        }

        private void ReadFileAsString(string path, string[,] strArr)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        for (int y = 0; y < GRID_COUNT[1]; y++)
                        {
                            string line = reader.ReadLine();
                            for (int x = 0; x < GRID_COUNT[0]; x++)
                            {
                                strArr[x, y] = line.Substring(x, 1);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void LoadElements(string[,] strArr)
        {
            for (int y = 0; y < GRID_COUNT[1]; y++)
            {
                for (int x = 0; x < GRID_COUNT[0]; x++)
                {
                    // bbb gg rr y rrr oooo
                    if (strArr[x, y].ToUpper().Equals("."))
                    {
                        gameGrid[x, y] = null;
                    }
                    else if (strArr[x, y].ToUpper().Equals("B"))
                    {
                        gameGrid[x, y] = new Brick(Color.Blue, new Rectangle(x * BLOCK_SIZE[0], y * BLOCK_SIZE[1], BLOCK_SIZE[0], BLOCK_SIZE[1]), 5);
                    }
                    else if (strArr[x, y].ToUpper().Equals("G"))
                    {
                        gameGrid[x, y] = new Brick(Color.Green, new Rectangle(x * BLOCK_SIZE[0], y * BLOCK_SIZE[1], BLOCK_SIZE[0], BLOCK_SIZE[1]), 10);
                    }
                    else if (strArr[x, y].ToUpper().Equals("R"))
                    {
                        gameGrid[x, y] = new Brick(Color.Red, new Rectangle(x * BLOCK_SIZE[0], y * BLOCK_SIZE[1], BLOCK_SIZE[0], BLOCK_SIZE[1]), 15);
                    }
                    else if (strArr[x, y].ToUpper().Equals("Y"))
                    {
                        gameGrid[x, y] = new Brick(Color.Yellow, new Rectangle(x * BLOCK_SIZE[0], y * BLOCK_SIZE[1], BLOCK_SIZE[0], BLOCK_SIZE[1]), 20);
                    }
                    else if (strArr[x, y].ToUpper().Equals("O"))
                    {
                        gameGrid[x, y] = new Brick(Color.Orange, new Rectangle(x * BLOCK_SIZE[0], y * BLOCK_SIZE[1], BLOCK_SIZE[0], BLOCK_SIZE[1]), 40);
                    }
                }
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameState.MAIN_MENU:
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        setGround();
                        LoadElements(strgrid_lv1);
                        gameState = GameState.LEVEL1;
                    }
                    break;
                case GameState.LEVEL1:
                    if (!(Keyboard.GetState().GetPressedKeys().Length >= 2))
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                            paddlePosition -= 10;
                        else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                            paddlePosition += 10;

                        if ((paddlePosition + (BLOCK_SIZE[0] * 2)) >= GraphicsDevice.Viewport.Width)
                            paddlePosition = GraphicsDevice.Viewport.Width - (BLOCK_SIZE[0] * 2);
                        else if (paddlePosition <= 0)
                        {
                            paddlePosition = 0;
                        }

                        ballPosition.X += ballVelocity.X;
                        ballPosition.Y -= ballVelocity.Y;

                        for (int y = 0; y < GRID_COUNT[1]; y++)
                        {
                            for (int x = 0; x < GRID_COUNT[0]; x++)
                            {
                                if (gameGrid[x, y] != null)
                                    if (ballRct.Intersects(gameGrid[x, y].GetRect))
                                    {
                                        if (ballPosition.Y <= gameGrid[x, y].GetRect.Bottom && ballPosition.X >= gameGrid[x, y].GetRect.X && (ballPosition.X + 20) <= (gameGrid[x, y].GetRect.X + BLOCK_SIZE[0]) ||
                                            (ballPosition.Y + 20) >= gameGrid[x, y].GetRect.Top && ballPosition.X >= gameGrid[x, y].GetRect.X && (ballPosition.X + 20) <= (gameGrid[x, y].GetRect.X + BLOCK_SIZE[0]))
                                            ballVelocity.Y *= -1;
                                        else if (ballPosition.X >= gameGrid[x, y].GetRect.Left && ballPosition.Y >= gameGrid[x, y].GetRect.Y && (ballPosition.Y + 20) <= (gameGrid[x, y].GetRect.Y + BLOCK_SIZE[0]) ||
                                            (ballPosition.X + 20) <= gameGrid[x, y].GetRect.Right && ballPosition.Y >= gameGrid[x, y].GetRect.Y && (ballPosition.Y + 20) <= (gameGrid[x, y].GetRect.Y + BLOCK_SIZE[0]))
                                            ballVelocity.X *= -1;

                                        gameGrid[x, y] = null;
                                    }
                            }
                        }

                        //Console.WriteLine(checkNullArray());

                        if (ballPosition.X >= (GraphicsDevice.Viewport.Width - 20) ||
                            ballPosition.X <= 0)
                            ballVelocity.X *= -1;

                        if (ballPosition.Y >= GraphicsDevice.Viewport.Height)
                            gameState = GameState.QUIT;
                        else if (ballPosition.Y <= 0 || ((ballPosition.Y + 20) == paddleRct.Top && (ballPosition.X - 10) >= (paddlePosition - 10) && (ballPosition.X + 30) <= (paddlePosition + BLOCK_SIZE[0] * 2 + 10)))
                            //else if (ballPosition.Y <= 0 || (ballPosition.Y + 20) == paddleRct.Top)
                            ballVelocity.Y *= -1;

                        ballRct = new Rectangle((int)ballPosition.X, (int)ballPosition.Y, 20, 20);
                        paddleRct = new Rectangle(paddlePosition, GraphicsDevice.Viewport.Height - BLOCK_SIZE[1] - 5, BLOCK_SIZE[0] * 2, BLOCK_SIZE[1]);
                    }
                    break;
                case GameState.QUIT:
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                        gameState = GameState.MAIN_MENU;
                    break;
            }

            base.Update(gameTime);
        }

        private bool checkNullArray()
        {
            bool result = true;
            for (int y = 0; y < GRID_COUNT[1]; y++)
            {
                for (int x = 0; x < GRID_COUNT[0]; x++)
                {
                    if (result && gameGrid[x, y] == null)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.MAIN_MENU:
                    spriteBatch.DrawString(menuFont, "Breakout", new Vector2(screen.X / 2 - menuFont.MeasureString("Breakout").X / 2, 50), Color.White);
                    spriteBatch.DrawString(subtextFont, "Play(P)", new Vector2(screen.X / 2 - subtextFont.MeasureString("Play(P)").X / 2, 200), Color.White);
                    break;
                case GameState.LEVEL1:
                    for (int y = 0; y < GRID_COUNT[1]; y++)
                    {
                        for (int x = 0; x < GRID_COUNT[0]; x++)
                        {
                            if (gameGrid[x, y] != null)
                                spriteBatch.Draw(brick, gameGrid[x, y].GetRect, gameGrid[x, y].GetText);
                        }
                    }

                    spriteBatch.Draw(ball, ballRct, Color.White);
                    spriteBatch.Draw(brick, paddleRct, Color.White);
                    break;
                case GameState.QUIT:
                    spriteBatch.DrawString(menuFont, "Game Over", new Vector2(screen.X / 2 - menuFont.MeasureString("Game Over").X / 2, 50), Color.White);
                    spriteBatch.DrawString(subtextFont, "Restart(R)", new Vector2(screen.X / 2 - subtextFont.MeasureString("Restart(R)").X / 2, 200), Color.White);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
