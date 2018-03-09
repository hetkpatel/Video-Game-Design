﻿using System;
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

namespace ScribblePlatformer
{
    class Level : IDisposable
    {
        private Tile[,] tiles;
        private Dictionary<string, Texture2D> tileSheets;
        public Dictionary<int, Rectangle> TileSourceRecs;

        Player player;

        private Vector2 start;

        private const int TileWidth = 64;
        private const int TileHeight = 64;
        private const int TilesPerRow = 5;
        private const int NumRowsPerSheet = 5;

        private Random random = new Random(1337);

        public Level(IServiceProvider _serviceProvider, string path)
        {
            content = new ContentManager(_serviceProvider, "Content");

            tileSheets = new Dictionary<string, Texture2D>();
            tileSheets.Add("Blocks", Content.Load<Texture2D>("Tiles/Blocks"));
            tileSheets.Add("Platforms", Content.Load<Texture2D>("Tiles/Platforms"));

            TileSourceRecs = new Dictionary<int, Rectangle>();
            for (int i = 0;i < TilesPerRow * NumRowsPerSheet;i++)
            {
                Rectangle rectTile = new Rectangle(
                    (i % TilesPerRow) * TileWidth,
                    (i / TilesPerRow) * TileHeight,
                    TileWidth,
                    TileHeight);
                TileSourceRecs.Add(i, rectTile);
            }
            LoadTiles(path);
        }

        public TileCollision GetCollision(int _x, int _y)
        {
            if (_x < 0 || _x >= Width)
                return TileCollision.Impassable;
            if (_y < 0 || _y >= Height)
                return TileCollision.Passable;

            return tiles[_x, _y].Collision;
        }

        public Rectangle GetBounds(int _x, int _y)
        {
            if (_x < 0 || _y < 0 || _x >= Width || _y >= Height)
                return new Rectangle(_x * Tile.Width, _y * Tile.Height, Tile.Width, Tile.Height);
            if (tiles[_x, _y].Collision == TileCollision.Platform)
                return new Rectangle(_x * Tile.Width, (_y * Tile.Height) + 20, Tile.Width, Tile.Height - 20);

            return new Rectangle(_x * Tile.Width, (_y * Tile.Height) + 5, Tile.Width, Tile.Height - 5);
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
                            throw new Exception(String.Format(
                                "The length of line {0} is differenct from all preceeding lines.",
                                lines.Count));
                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            tiles = new Tile[numOfTilesAcross, lines.Count];

            for (int y = 0;y < Height; ++y)
            {
                for (int x = 0;x < Width; ++x)
                {
                    string currentRow = lines[y];
                    char tileType = currentRow[x];
                    tiles[x, y] = LoadTile(tileType, x, y);
                }
            }
        }

        private Tile LoadTile(char _tileType, int _x, int _y)
        {
            switch (_tileType)
            {
                case '.':
                    return new Tile(String.Empty, 0, TileCollision.Passable);

                case 'B':
                    return LoadVarietyTile("Platforms", 0, 5);
                case 'G':
                    return LoadVarietyTile("Platforms", 5, 5);
                case 'O':
                    return LoadVarietyTile("Platforms", 10, 5);
                case 'R':
                    return LoadVarietyTile("Platforms", 15, 5);
                case 'Y':
                    return LoadVarietyTile("Platforms", 20, 5);

                case 'b':
                    return LoadVarietyTile("Blocks", 0, 5);
                case 'g':
                    return LoadVarietyTile("Blocks", 5, 5);
                case 'o':
                    return LoadVarietyTile("Blocks", 10, 5);
                case 'r':
                    return LoadVarietyTile("Blocks", 15, 5);
                case 'y':
                    return LoadVarietyTile("Blocks", 20, 5);

                case '+':
                    return LoadStartTile(_x, _y);

                default:
                    throw new NotSupportedException(String.Format(
                        "Unsupported tile type character '{0}' at position {1}, {2}.", _tileType, _x, _y));
            }
        }

        private Tile LoadVarietyTile(string _tileSheetName, int _colorRow, int _variationCount)
        {
            int index = random.Next(_variationCount);
            int tileSheetIndex = _colorRow + index;
            if (_tileSheetName == "Blocks")
                return new Tile(_tileSheetName, tileSheetIndex, TileCollision.Impassable);

            return new Tile(_tileSheetName, tileSheetIndex, TileCollision.Platform);
        }

        private Tile LoadStartTile(int _x, int _y)
        {
            if (Player != null)
                throw new NotSupportedException("A level may only have one starting point.");

            start = new Vector2((_x * 64) + 48, (_y * 64) + 16);
            player = new Player(this, start);

            return new Tile(String.Empty, 0, TileCollision.Passable);
        }

        public void Update(GameTime _gameTime)
        {
            player.Update(_gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            DrawTiles(spriteBatch);
            player.Draw(gameTime, spriteBatch);
        }

        private void DrawTiles(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < Height; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    if (tiles[x, y].TileSheetName == "Blocks" || tiles[x, y].TileSheetName == "Platforms")
                    {
                        Vector2 position = new Vector2(x, y) * Tile.Size;
                        spriteBatch.Draw(
                            tileSheets[tiles[x, y].TileSheetName],
                            position,
                            TileSourceRecs[tiles[x, y].TileSheetIndex],
                            Color.White);
                    }
                }
            }
        }

        public Player Player
        {
            get { return player; }
        }

        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public int Width
        {
            get { return tiles.GetLength(0); }
        }

        public int Height
        {
            get { return tiles.GetLength(1); }
        }

        public void Dispose()
        {
            Content.Unload();
        }
    }
}
