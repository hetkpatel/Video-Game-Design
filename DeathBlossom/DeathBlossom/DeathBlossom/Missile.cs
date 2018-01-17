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

namespace DeathBlossom
{
    class Missile
    {
        Texture2D missileTex;
        Rectangle missileRect;
        Vector2 missileCenter;
        double heading;
        int posX, posY;

        public Missile(Texture2D texture, Vector2 location, double heading)
        {
            missileTex = texture;
            posX = (int)location.X;
            posY = (int)location.Y;
            missileRect = new Rectangle(posX, posY, 28, 50);
            missileCenter = new Vector2(missileRect.X + missileRect.Width / 2,
                                        missileRect.Y + missileRect.Height / 2);
            this.heading = heading;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(missileTex, missileCenter, null, Color.White,
                (float)heading,
                new Vector2(missileTex.Width/2, missileTex.Height/2), 0.2f,
                SpriteEffects.None, 0);
        }
    }
}
