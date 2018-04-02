using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ScribblePlatformer
{
    class Enemy : AnimatedSprite
    {
        private SpriteEffects flip = SpriteEffects.None;
        private string currentAnim = "Walk";
        private const float MoveSpeed = 128.0f;
        private Rectangle localBounds;
        bool isAlive;
        bool isCompletelyDead;
        Level level;
        Vector2 position;
        Vector2 velocity;
        public bool IsAlive
        {
            get { return isAlive; }
        }
        /// <summary>
        /// Is enemy completely dead, if so, flag for removal from level
        /// </summary>
        public bool IsCompletelyDead
        {
            get { return isCompletelyDead; }
        }
        /// <summary>
        /// What level are we on, used for collision and interaction
        /// with the level's entities.
        /// </summary>
        public Level Level
        {
            get { return level; }
        }
        /// <summary>
        /// What position the enemy is in the world
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// The enemy's movement velocity
        /// </summary>
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
        /// <summary>
        /// Gets a rectangle which bounds this enemy in world space.
        /// </summary>
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int)Math.Round(Position.X - Origin.X) + localBounds.X;
                int top = (int)Math.Round(Position.Y - Origin.Y) + localBounds.Y;
                return new Rectangle(left, top, localBounds.Width, localBounds.Height);
            }
        }
        public Enemy(Level _level, Vector2 _position, string _enemy) : base(96, 96, 4)
        {
            level = _level;
            position = _position;
            isAlive = true;
            isCompletelyDead = false;
            LoadContent(_enemy);
        }
        public void LoadContent(string _enemy)
        {
            string enemyString = string.Empty;
            switch (_enemy)
            {
                case "e":
                    enemyString = "Sprites/Enemies/muffinman";
                    break;
                default:
                    enemyString = "Sprites/Enemies/muffinman";
                    break;
            }
            // Load animated textures.
            Texture2D tex = Level.Content.Load<Texture2D>(enemyString);
            if (!SpriteTextures.Contains(tex))
                SpriteTextures.Add(tex);
            Animation anim = new Animation();
            anim.LoadAnimation("Walk", 0, new List<int>
            {
            0,
            1
            }, 4, true);
            SpriteAnimations.Add("Walk", anim);
            anim = new Animation();
            anim.LoadAnimation("Dead", 0, new List<int>
            {
            2,
            3,
            4,
            5
            }, 36, false);
            anim.AnimationCallBack(DeadAnimEnd);
            SpriteAnimations.Add("Dead", anim);
            // Calculate bounds within texture size.
            // subtract 10 from width and height to remove a 5px buffer
            // around the enemy.
            int width = FrameWidth - 10;
            int left = (FrameWidth - width) / 2;
            int height = FrameHeight - 10;
            int top = FrameHeight - height;
            localBounds = new Rectangle(left, top, width, height);
            SpriteAnimations[currentAnim].ResetPlay();
        }
        public void OnKilled()
        {
            isAlive = false;
            SpriteAnimations[currentAnim].Stop();
            currentAnim = "Dead";
            SpriteAnimations[currentAnim].ResetPlay();
        }
        /// <summary>
        /// Called when Dead animation is done, letting the level know to remove.
        /// </summary>
        public void DeadAnimEnd()
        {
            isCompletelyDead = true;
        }
        public void Update(GameTime _gameTime)
        {
            SpriteAnimations[currentAnim].Update(_gameTime);
            if (isAlive)
            {
                float elapsed = (float)_gameTime.ElapsedGameTime.TotalSeconds;
                // Calculate tile position based on the side we are walking towards.
                int direction = 1;
                if (Velocity.X < 0)
                    direction = -1;
                float posX = Position.X + localBounds.Width / 2 * direction;
                int tileX = (int)Math.Floor(posX / Tile.Width) - direction;
                int tileY = (int)Math.Floor(Position.Y / Tile.Height);
                // If we are about to run into a wall or off a cliff, reverse direction.
                if (Level.GetCollision(tileX + direction, tileY - 1) == TileCollision.Impassable ||
                Level.GetCollision(tileX + direction, tileY + 1) == TileCollision.Passable ||
                Level.GetCollision(tileX + direction, tileY) == TileCollision.Impassable)
                {
                    direction *= -1;
                }
                // Move in the current direction.
                velocity = new Vector2(direction * MoveSpeed * elapsed, 0.0f);
                position = position + velocity;
            }
        }
        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            Rectangle source = GetFrameRectangle(SpriteAnimations[currentAnim].FrameToDraw);
            // Flip the sprite to face the way it is moving.
            if (Velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            else if (Velocity.X < 0)
                flip = SpriteEffects.None;
            // Draw the enemy.
            _spriteBatch.Draw(SpriteTextures[0], position, source, Color.White, 0.0f, Origin, 1.0f, flip, 0.0f);
        }

    }
}
