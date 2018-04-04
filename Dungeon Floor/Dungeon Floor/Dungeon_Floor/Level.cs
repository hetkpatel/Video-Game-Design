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
using System.IO;

namespace Dungeon_Floor
{
    class Level : IDisposable
    {
        private Tile[,] tiles;
        private Dictionary<int, Rectangle> assestsRct;
        private Texture2D assestsSheet;
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        private const int TileWidth = 30;
        private const int TileHeight = 30;
        private const int Row = 100;
        private const int Col = 50;
        public int Rows
        {
            get { return Row; }
        }
        public int Columns
        {
            get { return Col; }
        }

        public Level(IServiceProvider _serviceprovider)
        {
            content = new ContentManager(_serviceprovider, "Content");
            assestsSheet = Content.Load<Texture2D>("Tiles/DungeonFloorMap");
            assestsRct = new Dictionary<int, Rectangle>();

            int X = 0, Y = 0;
            for (int i = 0;i < 60;i++)
            {
                if (X == 264)
                {
                    X = 0;
                    Y += 33;
                }
                assestsRct.Add(i, new Rectangle(X, Y, 32, 32));
                X += 33;
            }

            //LoadTiles(path);
        }

        //public void DrawTiles(SpriteBatch sb)
        //{
        //    for (int y = 0; y < Columns; ++y)
        //    {
        //        for (int x = 0; x < Rows; ++x)
        //        {
        //            if (tileSheets.ContainsKey(tiles[x, y].TileSheetName))
        //            {
        //                Vector2 position = new Vector2(x, y) * Tile.Size;
        //                sb.Draw(tileSheets[tiles[x, y].TileSheetName], position, TileSourceRects[tiles[x, y].TileSheetIndex], Color.White);
        //            }
        //        }
        //    }
        //}

        public void Dispose()
        {
            Content.Unload();
        }
    }
}
