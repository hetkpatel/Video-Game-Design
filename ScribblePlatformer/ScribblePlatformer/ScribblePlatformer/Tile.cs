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

    public class Tile
    {
        //name of spriteSheet
        public string TileSheetName;
        //index of rectangle for sourceRect
        public int TileSheetIndex;
        public TileCollision Collision;


        public const int Width = 64;
        public const int Height = 64;
        public static readonly Vector2 Size = new Vector2(Width, Height);
        public Tile(string _tileSheetName, int _tileSheetIndex, TileCollision _collision)
        {
            TileSheetName = _tileSheetName;
            TileSheetIndex = _tileSheetIndex;
            Collision = _collision;
        }
     }
            
}

