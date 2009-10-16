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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using GameDev.Core;

namespace GameDev.GameBoard
{
    public class GameBoard : DrawableGameComponent
    {

        public string Name { get; set; }
        public Tile[,] Tiles { get; private set; }
        
        private int _widthInPixels = int.MinValue;
        private int _heightInPixels = int.MinValue;

        public int WidthInPixels { get { return _widthInPixels; } }
        public int HeightInPixels { get { return _heightInPixels; } }

        
        private void RecalculateDimensions()
        {
            _widthInPixels = TilesWide * TileSizeInPixels;
            _heightInPixels = TilesHigh * TileSizeInPixels;
        }

        
        private int _tileSizeInPixels;
        public int TileSizeInPixels 
        { 
            get
            {
                return _tileSizeInPixels;
            }
            private set
            {
                _tileSizeInPixels = value; 
                RecalculateDimensions();
            } 
        }
         

        public int TilesHigh { get { return Tiles.GetLength(0); } }
        public int TilesWide { get { return Tiles.GetLength(1); } }


        public GameBoard(Game game) : this(game, "Unnamed", 32, 32, 64){}

        public GameBoard(Game game, string name, int tilesHorizontally, int tilesVertically, int tileSizeInPixels): base(game)
        {
            Name = name;

            Tiles = new Tile[tilesHorizontally, tilesVertically];
            for (int x = 0; x < tilesVertically; x++)
            {
                for (int y = 0; y < tilesHorizontally; y++)
                {
                    Tiles[x, y] = new Tile(game, this, null);
                }
            }

            TileSizeInPixels = TileSizeInPixels;
        }

        public bool ContainsPosition(int x, int y)
        {
            return x >= 0 && x < TilesWide && y >= 0 && y < TilesHigh;
        }

        public bool ContainsPosition(Point point)
        {
            return ContainsPosition(point.X, point.Y);
        }

        public bool ContainsPosition(IPlaceable place)
        {
            return ContainsPosition(place.X, place.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Tile tile in Tiles)
            {
                tile.Update(gameTime);
            }
        }
    }
}