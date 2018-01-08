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

namespace Cards
{
    public class Card
    {

        Texture2D cardTxtre;
        Rectangle cardRctgl;

        public Card(ContentManager c, String contentName, Rectangle cardR)
        {
            cardTxtre = c.Load<Texture2D>(contentName);
            cardRctgl = cardR;
        }

        public void Draw(GraphicsDevice gb)
        {
            SpriteBatch sb = new SpriteBatch(gb);
            sb.Begin();
            sb.Draw(cardTxtre, cardRctgl, Color.White);
            sb.End();
        }
    }
}
