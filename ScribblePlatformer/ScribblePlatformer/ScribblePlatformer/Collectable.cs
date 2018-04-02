using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ScribblePlatformer
{
    class Collectable : AnimatedSprite
    {
        private string currentAnim = "Idle";
        private Rectangle localBounds;
        bool isAlive;
        Level level;
        Vector2 position;
        private Color colorOffset;
        /// <summary>
        /// Is the collectable alive, or has been collected?
        /// </summary>
        public bool IsAlive
        {
            get { return isAlive; }
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
        /// What position the collectable is in the world
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        /// <summary>
        /// Gets a rectangle which bounds this collectable in world space.
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
        public Collectable(Level _level, Vector2 _position, string _collectable) : base(32, 32, 4)
        {
            level = _level;
            position = _position;
            isAlive = true;
            LoadContent(_collectable);
        }
        public void LoadContent(string _collectable)
        {
            string sheetString = string.Empty;
            switch (_collectable)
            {
                case "s":
                    sheetString = "Sprites/Collectables/scribbles";
                    colorOffset = Color.Black;
                    break;
                case "S":
                    sheetString = "Sprites/Collectables/scribbles";
                    colorOffset = Color.Gold;
                    break;
                default:
                    sheetString = "Sprites/Collectables/scribbles";
                    colorOffset = Color.White;
                    break;
            }
            // Load animated textures.
            Texture2D tex = Level.Content.Load<Texture2D>(sheetString);
            if (!SpriteTextures.Contains(tex))
                SpriteTextures.Add(tex);
            Animation anim = new Animation();
            anim.LoadAnimation("Idle", 0, new List<int>
{
0,
1,
2,
3
}, 16, true);
            SpriteAnimations.Add("Idle", anim);
            // Calculate bounds within texture size.
            // subtract 4 from height to remove a 2px buffer
            // around the collectable.
            int width = FrameWidth;
            int left = (FrameWidth - width) / 2;
            int height = FrameHeight - 4;
            int top = FrameHeight - height;
            localBounds = new Rectangle(left, top, width, height);
            SpriteAnimations[currentAnim].ResetPlay();
        }
        public void OnKilled()
        {
            isAlive = false;
        }
        public void Update(GameTime _gameTime)
        {
            SpriteAnimations[currentAnim].Update(_gameTime);
        }
        public void Draw(GameTime _gameTime, SpriteBatch _spriteBatch)
        {
            Rectangle source = GetFrameRectangle(SpriteAnimations[currentAnim].FrameToDraw);
            // Draw the enemy.
            _spriteBatch.Draw(SpriteTextures[0], position, source, colorOffset, 0.0f, Origin, 1.0f, SpriteEffects.None, 0.0f);
        }

    }
}
