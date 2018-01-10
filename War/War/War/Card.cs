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
        ContentManager contentManager;
        SpriteFont spriteFont;

        Texture2D cardTxtre;
        String suite, value;

        public Card(ContentManager c)
        {
            contentManager = c;
            spriteFont = contentManager.Load<SpriteFont>("CardFont");
        }

        public void LoadContent(String contentName, String suite, String value)
        {
            cardTxtre = contentManager.Load<Texture2D>(contentName);
            this.suite = suite;
            this.value = value;
        }

        public void Draw(GraphicsDevice gb, int order)
        {
            SpriteBatch sb = new SpriteBatch(gb);
            sb.Begin();
            Rectangle cardRctgl = new Rectangle();
            if (order == 0)
            {
                cardRctgl = new Rectangle(20, 20, 300, 400);
                sb.DrawString(spriteFont, "Suite: " + suite + "\nValue: " + value, new Vector2(20, 420), Color.White);
            }
            else
            {
                cardRctgl = new Rectangle(425, 20, 300, 400);
                sb.DrawString(spriteFont, "Suite: " + suite + "\nValue: " + value, new Vector2(425, 420), Color.White);
            }
            sb.Draw(cardTxtre, cardRctgl, Color.White);
            sb.End();
        }
    }

}
