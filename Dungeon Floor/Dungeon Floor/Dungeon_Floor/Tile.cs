using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Dungeon_Floor
{
    class Tile
    {
        public string AssetName;

        public const int Width = 30;
        public const int Height = 30;
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(string assestName)
        {
            AssetName = assestName;
        }
    }
}
