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
        private List<int> tiles;
        private List<Rectangle> assestsRct;
        private List<Vector2> tilePositions;
        private Texture2D assestsSheet;
        private const int XPadding = 1180, YPadding = 550;
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;
        
        private const int Row = 100;
        private const int Columns = 50;
        public int Rows
        {
            get { return Row; }
        }
        public int Col
        {
            get { return Columns; }
        }

        public Level(IServiceProvider _serviceprovider)
        {
            content = new ContentManager(_serviceprovider, "Content");
            tiles = new List<int>();
            assestsSheet = Content.Load<Texture2D>("Tiles/DungeonFloorMap");
            assestsRct = new List<Rectangle>();
            tilePositions = new List<Vector2>();

            int X = 0, Y = 0;
            for (int i = 0;i < 60;i++)
            {
                if (X == 264)
                {
                    X = 0;
                    Y += 33;
                }
                assestsRct.Add(new Rectangle(X, Y, 30, 30));
                X += 33;
            }
            
            for (int y = 0; y < Col; ++y)
            {
                for (int x = 0; x < Rows; ++x)
                {
                    tilePositions.Add(new Vector2(x, y) * new Vector2(30));
                }
            }

            for (int i = 0; i < tilePositions.Count; i++)
                tilePositions[i] = new Vector2(tilePositions[i].X - XPadding, tilePositions[i].Y - YPadding);

            LoadTiles(@"Content/Levels/Level01.txt");
        }

        private void LoadTiles(string path)
        {
            int numOfTilesAcross = 0;
            List<string> lines = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = reader.ReadLine();
                    numOfTilesAcross = line.Length;
                    while (line != null)
                    {
                        lines.Add(line);
                        int nextLineWidth = line.Length;
                        if (nextLineWidth != numOfTilesAcross)
                            throw new Exception(String.Format("The length of line {0} is different from all preceding lines.", lines.Count));
                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            for (int y = 0; y < Col; ++y)
            {
                for (int x = 0; x < Row; ++x)
                {
                    string currentRow = lines[y];
                    char tileType = currentRow[x];
                    tiles.Add((int)Char.GetNumericValue(tileType));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            const int movemnetInt = 5;
            if (kb.IsKeyDown(Keys.Right))
            {
                for (int i = 0; i < tilePositions.Count; i++)
                    tilePositions[i] = new Vector2(tilePositions[i].X - movemnetInt, tilePositions[i].Y);
            }
            else if (kb.IsKeyDown(Keys.Left))
            {
                for (int i = 0; i < tilePositions.Count; i++)
                    tilePositions[i] = new Vector2(tilePositions[i].X + movemnetInt, tilePositions[i].Y);
            }
            if (kb.IsKeyDown(Keys.Up))
            {
                for (int i = 0; i < tilePositions.Count; i++)
                    tilePositions[i] = new Vector2(tilePositions[i].X, tilePositions[i].Y + movemnetInt);
            }
            else if (kb.IsKeyDown(Keys.Down))
            {
                for (int i = 0; i < tilePositions.Count; i++)
                    tilePositions[i] = new Vector2(tilePositions[i].X, tilePositions[i].Y - movemnetInt);
            }
        }

        public void DrawTiles(SpriteBatch sb)
        {
            int index = 0;
            for (int y = 0; y < Col; ++y)
            {
                for (int x = 0; x < Rows; ++x)
                {
                    sb.Draw(assestsSheet, tilePositions[index], assestsRct[tiles[index]], Color.White);
                    index++;
                }
            }
        }

        public void Dispose()
        {
            Content.Unload();
        }
    }
}
