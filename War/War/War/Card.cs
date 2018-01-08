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
    public class Card
    {
        ContentManager cm;
        SpriteFont sf;

        Texture2D cardTxtre;
        String suite, value;

        public Card(ContentManager c)
        {
            cm = c;
            sf = cm.Load<SpriteFont>("CardFont");
        }

        public void LoadContent(String contentName, String suite, String value)
        {
            cardTxtre = cm.Load<Texture2D>(contentName);
            this.suite = suite;
            this.value = value;
        }

        public void Draw(GraphicsDevice gb, Rectangle cardRctgl)
        {
            SpriteBatch sb = new SpriteBatch(gb);
            sb.Begin();
            sb.Draw(cardTxtre, cardRctgl, Color.White);
            sb.DrawString(sf, "Suite: " + suite + "\nValue: " + value, new Vector2(50), Color.White);
            sb.End();
        }
    }

}
