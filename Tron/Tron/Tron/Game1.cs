using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tron
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont titleFont, subtextFont;
        Stopwatch stopwtch = new Stopwatch();

        Texture2D whiteBox;

        Rectangle player1, player2;
        List<Rectangle> player1Trail, player2Trail;
        Vector2 player1Pos, player2Pos;

        GameState gameState;
        MoveDirection player1Dir, player2Dir;
        Vector2 screen;

        int timer;
        String playerWon;

        Color player1Color = new Color(53, 204, 151),
            player2Color = new Color(255, 102, 252);

        const int SPEED = 5;

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
            screen = new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            player1Trail = new List<Rectangle>();
            player2Trail = new List<Rectangle>();

            gameState = GameState.MAIN_MENU;
            timer = 3;
            
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
            whiteBox = this.Content.Load<Texture2D>("White");

            titleFont = this.Content.Load<SpriteFont>("Title");
            subtextFont = this.Content.Load<SpriteFont>("Subtext");
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
            switch (gameState)
            {
                case GameState.MAIN_MENU:
                    if (kb.IsKeyDown(Keys.P))
                    {
                        stopwtch.Start();
                        gameState = GameState.COUNT_DOWN;
                    }
                    break;
                case GameState.COUNT_DOWN:
                    timer = 3 - stopwtch.Elapsed.Seconds;
                    if (timer == -1)
                    {
                        stopwtch.Stop();
                        gameState = GameState.SET_GAME;
                    }
                    break;
                case GameState.SET_GAME:
                    Random rnd = new Random();
                    int height = rnd.Next(0, (int)screen.Y-15);
                    player1Pos = new Vector2(screen.X - 16, height);
                    player2Pos = new Vector2(0, height);
                    setPlayerRectangles();
                    player1Dir = MoveDirection.LEFT;
                    player2Dir = MoveDirection.RIGHT;
                    gameState = GameState.GAME_PLAY;
                    break;
                case GameState.GAME_PLAY:
                    if (kb.IsKeyDown(Keys.Up))
                        if (player1Dir != MoveDirection.DOWN)
                            player1Dir = MoveDirection.UP;
                    if (kb.IsKeyDown(Keys.Down))
                        if (player1Dir != MoveDirection.UP)
                            player1Dir = MoveDirection.DOWN;
                    if (kb.IsKeyDown(Keys.Left))
                        if (player1Dir != MoveDirection.RIGHT)
                            player1Dir = MoveDirection.LEFT;
                    if (kb.IsKeyDown(Keys.Right))
                        if (player1Dir != MoveDirection.LEFT)
                            player1Dir = MoveDirection.RIGHT;

                    switch (player1Dir)
                    {
                        case MoveDirection.UP:
                            player1Pos = new Vector2(player1Pos.X, player1Pos.Y - SPEED);
                            break;
                        case MoveDirection.DOWN:
                            player1Pos = new Vector2(player1Pos.X, player1Pos.Y + SPEED);
                            break;
                        case MoveDirection.LEFT:
                            player1Pos = new Vector2(player1Pos.X - SPEED, player1Pos.Y);
                            break;
                        case MoveDirection.RIGHT:
                            player1Pos = new Vector2(player1Pos.X + SPEED, player1Pos.Y);
                            break;
                    }

                    if (kb.IsKeyDown(Keys.W))
                        if (player2Dir != MoveDirection.DOWN)
                            player2Dir = MoveDirection.UP;
                    if (kb.IsKeyDown(Keys.S))
                        if (player2Dir != MoveDirection.UP)
                            player2Dir = MoveDirection.DOWN;
                    if (kb.IsKeyDown(Keys.A))
                        if (player2Dir != MoveDirection.RIGHT)
                            player2Dir = MoveDirection.LEFT;
                    if (kb.IsKeyDown(Keys.D))
                        if (player2Dir != MoveDirection.LEFT)
                            player2Dir = MoveDirection.RIGHT;

                    switch (player2Dir)
                    {
                        case MoveDirection.UP:
                            player2Pos = new Vector2(player2Pos.X, player2Pos.Y - SPEED);
                            break;
                        case MoveDirection.DOWN:
                            player2Pos = new Vector2(player2Pos.X, player2Pos.Y + SPEED);
                            break;
                        case MoveDirection.LEFT:
                            player2Pos = new Vector2(player2Pos.X - SPEED, player2Pos.Y);
                            break;
                        case MoveDirection.RIGHT:
                            player2Pos = new Vector2(player2Pos.X + SPEED, player2Pos.Y);
                            break;
                    }
                    setPlayerRectangles();

                    if (player1.Top <= -5 || player1.Bottom >= screen.Y + 5 ||
                        player1.Left <= -5 || player1.Right >= screen.X + 5)
                    {
                        playerWon = "2";
                        gameState = GameState.GAME_OVER;
                    }

                    if (player2.Top <= -5 || player2.Bottom >= screen.Y + 5 ||
                        player2.Left <= -5 || player2.Right >= screen.X + 5)
                    {
                        playerWon = "1";
                        gameState = GameState.GAME_OVER;
                    }

                    if (player1.Intersects(player2) || player2.Intersects(player1))
                    {
                        playerWon = "N/A";
                        gameState = GameState.GAME_OVER;
                    }


                    for (int i = 0;i < player1Trail.Count-20;i++)
                    {
                        if (player1.Intersects(player1Trail[i]))
                        {
                            playerWon = "2";
                            gameState = GameState.GAME_OVER;
                        }
                        if (player2.Intersects(player1Trail[i]))
                        {
                            playerWon = "1";
                            gameState = GameState.GAME_OVER;
                        }
                    }

                    for (int i = 0; i < player2Trail.Count - 20; i++)
                    {
                        if (player1.Intersects(player2Trail[i]))
                        {
                            playerWon = "2";
                            gameState = GameState.GAME_OVER;
                        }
                        if (player2.Intersects(player2Trail[i]))
                        {
                            playerWon = "1";
                            gameState = GameState.GAME_OVER;
                        }
                    }
                    break;
                case GameState.GAME_OVER:
                    if (kb.IsKeyDown(Keys.P))
                    {
                        stopwtch.Reset();
                        stopwtch.Start();
                        gameState = GameState.COUNT_DOWN;
                    }
                    else if (kb.IsKeyDown(Keys.Q))
                    {
                        this.Exit();
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        private void setPlayerRectangles()
        {
            player1 = new Rectangle((int)player1Pos.X, (int)player1Pos.Y, 16, 16);
            player2 = new Rectangle((int)player2Pos.X, (int)player2Pos.Y, 16, 16);
            player1Trail.Add(player1);
            player2Trail.Add(player2);
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
            switch (gameState)
            {
                case GameState.MAIN_MENU:
                    spriteBatch.DrawString(titleFont, "Tron", new Vector2(screen.X/2 - titleFont.MeasureString("Tron").X/2, 50), Color.White);
                    spriteBatch.DrawString(subtextFont, "Play(P)", new Vector2(screen.X / 2 - subtextFont.MeasureString("Play(P)").X / 2, 200), Color.White);
                    break;
                case GameState.COUNT_DOWN:
                    String timerStr = "" + timer;
                    if (timerStr.Equals("0"))
                        timerStr = "GO!";
                    spriteBatch.DrawString(titleFont, timerStr, new Vector2(screen.X / 2 - titleFont.MeasureString(timerStr).X / 2, screen.Y / 2 - titleFont.MeasureString(timerStr).Y / 2), Color.White);
                    break;
                case GameState.GAME_PLAY:
                    foreach (Rectangle rect in player1Trail)
                    {
                        spriteBatch.Draw(whiteBox, rect, player1Color);
                    }
                    foreach (Rectangle rect in player2Trail)
                    {
                        spriteBatch.Draw(whiteBox, rect, player2Color);
                    }
                    spriteBatch.Draw(whiteBox, player1, player1Color);
                    spriteBatch.Draw(whiteBox, player2, player2Color);
                    break;
                case GameState.GAME_OVER:
                    spriteBatch.DrawString(titleFont, "Game Over", new Vector2(screen.X / 2 - titleFont.MeasureString("Game Over").X / 2, 50), Color.White);
                    if (playerWon != "N/A")
                        spriteBatch.DrawString(subtextFont, "Player " + playerWon + " wins.", new Vector2((screen.X / 2 - subtextFont.MeasureString("Player " + playerWon + " wins.").X / 2), 175), Color.White);
                    spriteBatch.DrawString(subtextFont, "Play Again(P)", new Vector2((screen.X / 2 - subtextFont.MeasureString("Play Again(P)").X / 2) - 100, 200), Color.White);
                    spriteBatch.DrawString(subtextFont, "Quit(Q)", new Vector2((screen.X / 2 - subtextFont.MeasureString("Quit(Q)").X / 2) + 100, 200), Color.White);
                    player1Trail.Clear();
                    player2Trail.Clear();
                    break;
                default:
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
