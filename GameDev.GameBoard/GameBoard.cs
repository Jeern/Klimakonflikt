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
using GameDev.Core.Graphics;



namespace GameDev.GameBoard
{
    public class GameBoard : DrawableGameComponent, IPlaceable
    {
        private Random m_random;
        public string Name { get; set; }
        public WalledTile[,] Tiles { get; protected set; }

        public GameImage BaseImage{ get; set; }
        private GameImage _completeBackground;
        public GameImage CompleteBackground { get{return _completeBackground;}
             set{_completeBackground = value;
                SetTileBackgroundDrawing(CompleteBackground == null);
             }
        }

        public Tile RandomTile
        {
            get {return Tiles[m_random.Next(this.TilesHorizontally), m_random.Next(this.TilesVertically)]; }
        }

 private void SetTileBackgroundDrawing(bool shouldDrawBackground)
{
    foreach (Tile tile in Tiles)
    {
        tile.ShouldDrawBackground = shouldDrawBackground;
    }
}

 private void SetTileContentDrawing(bool shouldDrawContents)
 {
     foreach (Tile tile in Tiles)
     {
         tile.ShouldDrawContents = shouldDrawContents;
     }
 }


        private Rectangle _outerRectangle;

        public Rectangle OuterRectangle { get { return _outerRectangle; } }
        
        private int _widthInPixels = int.MinValue;
        private int _heightInPixels = int.MinValue;

        public int WidthInPixels { get { return _widthInPixels; } }
        public int HeightInPixels { get { return _heightInPixels; } }

        public int TilesHorizontally { get { return Tiles.GetLength(0); } }
        public int TilesVertically { get { return Tiles.GetLength(1); } }

        private string m_LevelImageFileName;
        public string LevelImageFileName
        {
            get { return m_LevelImageFileName; }
        }

        public SpriteBatch SpriteBatch { get { return GameDevGame.Current.SpriteBatch; } }
        
        protected void RecalculateDimensions()
        {
            _widthInPixels = TilesHorizontally * TileSizeInPixels;
            _heightInPixels = TilesVertically * TileSizeInPixels;

            this._outerRectangle = new Rectangle(X, Y, this.WidthInPixels, this.HeightInPixels);

        }

        protected void RecalculateTileRectangles()
        {
            foreach (WalledTile tile in Tiles)
            {
                tile.RecalculateLayout();
            }
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

        #region Constructors

        public GameBoard(GameImage baseGameImage) : this(baseGameImage, "Unnamed", 10, 10, 64, "Unnamed") { }

        public GameBoard(GameImage baseGameImage, string name, int tilesHorizontally, int tilesVertically,
            int tileSizeInPixels, string levelImageFileName)
            : base(GameDevGame.Current)
        {
            Name = name;
            BaseImage = baseGameImage;
            Tiles = new WalledTile[tilesHorizontally, tilesVertically];
            for (int x = 0; x < TilesHorizontally; x++)
            {
                for (int y = 0; y < TilesVertically; y++)
                {
                    Tiles[x, y] = new WalledTile(this, baseGameImage, x, y);
                }
            }
            TileSizeInPixels = tileSizeInPixels;
            m_LevelImageFileName = levelImageFileName;
            m_random = new Random();
        }


        #endregion


        public bool ContainsPosition(int x, int y)
        {
            return x >= 0 && x < TilesHorizontally && y >= 0 && y < TilesVertically;
        }

        public WalledTile GetTileFromPixelPosition(Point p)
        {
            return GetTileFromPixelPosition(p.X, p.Y);
        }

        public WalledTile GetTileFromPixelPosition(int x, int y)
        {
            int resultX = (x- this.X) / TileSizeInPixels;
            int resultY = (y - this.Y) / TileSizeInPixels;
            if (this.ContainsPosition(resultX, resultY))
            {
                return this.Tiles[resultX, resultY];
            }
            else
            {
                throw new ArgumentException("Pixel at (" + resultX + "," + resultY + ") is not within bounds of GameBoard");
            }
        }


        public bool ContainsTile(int x, int y)
        {
            return ContainsPosition(x, y);
        }

        public bool ContainsTile(Point point)
        {
            return ContainsPosition(point.X, point.Y);
        }

        public bool ContainsTile(IPlaceable place)
        {
            return ContainsPosition(place.X, place.Y);
        }


        public bool ContainsPixel(int x, int y)
        {
            return this.OuterRectangle.Contains(x, y);
        }

        public bool ContainsPixel(Point position)
        {
            return this.OuterRectangle.Contains(position);
        }

        public bool ContainsPixel(IPlaceable place)
        {
            return this.OuterRectangle.Contains(place.X, place.Y);
        }



        public override void Draw(GameTime gameTime)
        {

            if (CompleteBackground != null)
            {
                SpriteBatch.Draw(CompleteBackground.CurrentTexture, OuterRectangle, Color.White);
            }
            for (int x = 0; x < TilesHorizontally; x++)
            {
               for (int y = 0; y < TilesVertically; y++)
               {
                   Tiles[x, y].Draw(gameTime);
               }
            }
            

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

        #region IPlaceable Members


        Point _position;
        public int X { get { return _position.X; } set { _position.X = value; RecalculateTileRectangles(); RecalculateDimensions(); } }

        public int Y { get { return _position.Y; } set { _position.Y = value; RecalculateTileRectangles(); RecalculateDimensions(); } }


        public void Move(Direction direction, float distance)
        {
            DirectionHelper4.Offset(this, direction, distance);
        }
        #endregion


        public void Reset()
        {
            foreach (Tile tile in Tiles)
            {
                tile.Reset();
            }
        }
    }
}