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

namespace Star_Field
{
    public class Star
    {
        Texture2D texture;
        Rectangle rectangle;
        Color color;
        double xPos, yPos;
        double speedx, speedy;

        public Star(ContentManager cm, int width, int height, Random rnd)
        {
            texture = cm.Load<Texture2D>("Star");
            rectangle = new Rectangle();
            color = Color.White;
            xPos = rnd.Next(width);
            yPos = rnd.Next(height);
            int multiple;
            if (rnd.Next(2) == 1)
                multiple = -1;
            else
                multiple = 1;
            speedx = rnd.NextDouble() * multiple;
            if (rnd.Next(2) == 1)
                multiple = -1;
            else
                multiple = 1;
            speedy = rnd.NextDouble() * multiple;
        }

        public Rectangle Update()
        {
            xPos += speedx;
            yPos += speedy;
            rectangle = new Rectangle((int)xPos, (int)yPos, 6, 6);
            return rectangle;
        }

        public void Draw(GraphicsDevice gd)
        {
            SpriteBatch sb = new SpriteBatch(gd);
            sb.Begin();
            sb.Draw(texture, rectangle, color);
            sb.End();
        }
    }
}
