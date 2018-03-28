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

namespace ScribblePlatformer
{
    class Player : AnimatedSprite
    {
        private string currentAnim = "Idle";
        private SpriteEffects flip = SpriteEffects.None;
        public Level Level
        {
            get { return level; }
        }
        Level level;
        public bool IsAlive
        {
            get { return isAlive; }
        }
        bool isAlive;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        Vector2 position;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        Vector2 velocity;
        private const float MoveAcceleration = 14000.0f;
        private const float MaxMoveSpeed = 2000.0f;
        private const float GroundDragFactor = .58f;
        private const float AirDragFactor = .65f;
        private const float MaxJumpTime = .35f;
        private const float JumpLaunchVelocity = -4000.0f;
        private const float GravityAcceleration = 3500.0f;
        private const float MaxFallSpeed = 600.0f;
        private const float JumpControlPower = .14f;
        private const float MoveStickScale = 1.0f;
        private const Buttons JumpButton = Buttons.A;
        public bool IsOnGround
        {
            get { return isOnGround; }
        }
        bool isOnGround;
        private float movement;
        private bool isJumping;
        private bool wasJumping;
        private float jumpTime;
        private Rectangle localBounds;
        private int previousBottom;
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X - Origin.X) + localBounds.X;
                int top = (int)Math.Round(Position.Y - Origin.Y) + localBounds.Y;
                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }
        
        public Player(Level _level, Vector2 _position)
        {
            level = _level;
            LoadContent();
            Reset(_position);
        }
        public void LoadContent()
        {
            // Load animated textures.
            SpriteTextures.Add(Level.Content.Load<Texture2D>("Sprites/Player/player"));
            Animation anim = new Animation();
            anim.LoadAnimation("Idle", 0, new List<int>
            {0,
             11,
             0,
             12
            }, 7, true);
            SpriteAnimations.Add("Idle", anim);
            anim = new Animation();
            anim.LoadAnimation("Walking", 0, new List<int>
            {
            0,
            1,
            2,
            3,
            4,
            5,
            6
            }, 7, true);
            SpriteAnimations.Add("Walking", anim);
            anim = new Animation();
            anim.LoadAnimation("Jump", 0, new List<int>
            {
            7,
            8,
            9,
            10,
            9,
            8,
            7
            }, 20, false);
            anim.AnimationCallBack(JumpAnimEnd);
            SpriteAnimations.Add("Jump", anim);
            // Calculate bounds within texture size.
            // subtract 4 from width and height to remove a 2px buffer
            // around the player.
            int width = FrameWidth - 4;
            int left = (FrameWidth - width) / 2;
            int height = FrameHeight - 4;
            int top = FrameHeight - height;
            localBounds = new Rectangle(left, top, width, height);
            anim = new Animation();
            anim.LoadAnimation("Dead", 0, new List<int>
            {
            13,
            14,
            15
            }, 6, false);
            anim.AnimationCallBack(DeadAnimEnd);
            SpriteAnimations.Add("Dead", anim);

        }
        public void OnKilled()
        {
            isAlive = false;
            SpriteAnimations[currentAnim].Stop();
            currentAnim = "Dead";
            SpriteAnimations[currentAnim].ResetPlay();
        }

        public void DeadAnimEnd()
        {
            isCompletelyDead = true;
        }

        public void JumpAnimEnd()
        {
            currentAnim = "Idle";
            SpriteAnimations[currentAnim].Play();
        }
        public void Reset(Vector2 _position)
        {
            Position = _position;
            Velocity = Vector2.Zero;
            isAlive = true;
            isCompletelyDead = false;
            SpriteAnimations[currentAnim].Stop();
            currentAnim = "Idle";
            SpriteAnimations[currentAnim].ResetPlay();
        }

        public void Update(GameTime _gameTime)
        {
            if (isAlive)
            {
                GetInput();
            }
            ApplyPhysics(_gameTime);
            SpriteAnimations[currentAnim].Update(_gameTime);
            // Clear input.
            movement = 0.0f;
            isJumping = false;
        }

        private void GetInput()
        {
            // Get input state.
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            // Get analog horizontal movement.
            movement = gamePadState.ThumbSticks.Left.X * MoveStickScale;
            // Ignore small movements to prevent running in place.
            if (Math.Abs(movement) < 0.5f)
                movement = 0.0f;
            // If any digital horizontal movement input is found, override the analog movement.
            if (gamePadState.IsButtonDown(Buttons.DPadLeft) ||
            keyboardState.IsKeyDown(Keys.Left) ||
            keyboardState.IsKeyDown(Keys.A))
            {
                movement = -1.0f;
            }
            else if (gamePadState.IsButtonDown(Buttons.DPadRight) ||
            keyboardState.IsKeyDown(Keys.Right) ||
            keyboardState.IsKeyDown(Keys.D))
            {
                movement = 1.0f;
            }
            // Check if the player wants to jump.
            isJumping =
            gamePadState.IsButtonDown(JumpButton) ||
            keyboardState.IsKeyDown(Keys.Space) ||
            keyboardState.IsKeyDown(Keys.Up) ||
            keyboardState.IsKeyDown(Keys.W);
            if (isJumping && currentAnim != "Jump")
            {
                SpriteAnimations[currentAnim].Stop();
                currentAnim = "Jump";
                SpriteAnimations[currentAnim].ResetPlay();
            }
            if (movement != 0 && currentAnim != "Jump" && currentAnim != "Walking")
            {
                SpriteAnimations[currentAnim].Stop();
                currentAnim = "Walking";
                SpriteAnimations[currentAnim].ResetPlay();
            }
            else if (currentAnim != "Jump" && movement == 0 && currentAnim == "Walking")
            {
                SpriteAnimations[currentAnim].Stop();
                currentAnim = "Idle";
                SpriteAnimations[currentAnim].ResetPlay();
            }
        }
        public bool IsCompletelyDead
        {
            get { return isCompletelyDead; }
        }
        bool isCompletelyDead;

        public void ApplyPhysics(GameTime _gameTime)
        {
            float elapsed = (float)_gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 previousPosition = Position;
            velocity.X += movement * MoveAcceleration * elapsed;
            velocity.Y = MathHelper.Clamp(velocity.Y + GravityAcceleration * elapsed, -MaxFallSpeed, MaxFallSpeed);
            velocity.Y = DoJump(velocity.Y, _gameTime);
            if (isOnGround)
                velocity.X *= GroundDragFactor;
            else
                velocity.X *= AirDragFactor;
            velocity.X = MathHelper.Clamp(velocity.X, -MaxMoveSpeed, MaxMoveSpeed);
            Position += velocity * elapsed;
            Position = new Vector2((float)Math.Round(Position.X), (float)Math.Round(Position.Y));
            HandleCollisions();
            if (Position.X == previousPosition.X)
                velocity.X = 0;
            if (Position.Y == previousPosition.Y)
                velocity.Y = 0;
        }
        private float DoJump(float _velocityY, GameTime _gameTime)
        {
            if (isJumping)
            {
                if ((!wasJumping && IsOnGround) || jumpTime > 0.0f)
                    jumpTime += (float)_gameTime.ElapsedGameTime.TotalSeconds;
                if (0.0f < jumpTime && jumpTime <= MaxJumpTime)
                    _velocityY = JumpLaunchVelocity * (1.0f - (float)Math.Pow(jumpTime / MaxJumpTime, JumpControlPower));
                else
                    jumpTime = 0.0f;
            }
            else
                jumpTime = 0.0f;
            wasJumping = isJumping;
            return _velocityY;
        }
        private void HandleCollisions()
        {
            Rectangle bounds = BoundingRectangle;
            int leftTile = (int)Math.Floor((float)bounds.Left / Tile.Width);
            int rightTile = (int)Math.Ceiling((float)bounds.Right / Tile.Width) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / Tile.Height);
            int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / Tile.Height) - 1;
            isOnGround = false;
            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    TileCollision collision = Level.GetCollision(x, y);
                    if (collision != TileCollision.Passable)
                    {
                        Rectangle tileBounds = Level.GetBounds(x, y);
                        Vector2 depth = bounds.GetIntersectionDepth(tileBounds);
                        if (depth != Vector2.Zero)
                        {
                            float absDepthX = Math.Abs(depth.X);
                            float absDepthY = Math.Abs(depth.Y);
                            if (absDepthY < absDepthX || collision == TileCollision.Platform)
                            {
                                if (previousBottom <= tileBounds.Bottom)
                                    isOnGround = true;
                                if (collision == TileCollision.Impassable || IsOnGround)
                                {
                                    Position = new Vector2(Position.X, Position.Y + depth.Y);
                                    bounds = BoundingRectangle;
                                }
                            }
                            else if (collision == TileCollision.Impassable)
                            {
                                Position = new Vector2(Position.X + depth.X, Position.Y);
                                bounds = BoundingRectangle;
                            }
                        }
                    }
                }
            }
            previousBottom = bounds.Bottom;
        }

        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            Rectangle source = GetFrameRectangle(SpriteAnimations[currentAnim].FrameToDraw);
            // Flip the sprite to face the way we are moving.
            if (Velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X > 0)
                flip = SpriteEffects.None;
            // Draw the player.
            _spriteBatch.Draw(SpriteTextures[0], position, source, Color.White, 0.0f, Origin, 1.0f, flip, 0.0f);
        }
    }

}