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

namespace Space_Invaders
{
    class Alien
    {
        Texture2D text;
        Rectangle rct;
        int x = 0;
        int y = 0;

        public Alien()
        {}

        public void setPosition(int amount, bool coord)
        {
            if (coord)
            {
                x += amount;
            } else
            {
                y += amount;
            }
            Rectangle tmp = rct;
            rct = new Rectangle(x, y, tmp.Width, tmp.Height);
        }

        public Texture2D Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public Rectangle Rect
        {
            get
            {
                return rct;
            }
            set
            {
                rct = value;
                x = rct.X;
                y = rct.Y;
            }
        }
    }
}
