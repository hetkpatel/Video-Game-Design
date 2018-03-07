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
    public enum TileCollision
    {
        Passable = 0,
        Impassable = 1,
        Platform = 2,
    }

    class Tile
    {
        public string TileSheetName;
        public int TileSheetIndex;

        public TileCollision Collision;

        public const int Width = 64;
        public const int Height = 64;
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(string tileSheetName, int tileSheetIndex, TileCollision _collision)
        {
            TileSheetName = tileSheetName;
            TileSheetIndex = tileSheetIndex;
            Collision = _collision;
        }
    }
}
