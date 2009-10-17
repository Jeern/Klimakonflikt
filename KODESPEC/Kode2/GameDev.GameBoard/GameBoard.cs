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
    public class GameBoard : DrawableGameComponent, IPlaceable
    {
        public string Name { get; set; }
        


        private Tile[,] gameBoardTiles = new Tile[10,10]; 

        public Texture2D BaseTileTexture { get; set; }

        private Rectangle _outerRectangle;

        public Rectangle OuterRectangle { get { return _outerRectangle; } }
        
        private int _widthInPixels = int.MinValue;
        private int _heightInPixels = int.MinValue;

        public int WidthInPixels { get { return _widthInPixels; } }
        public int HeightInPixels { get { return _heightInPixels; } }

        public int TilesHorizontally { get { return gameBoardTiles.GetLength(0); } }
        public int TilesVertically { get { return gameBoardTiles.GetLength(1); } }

        public SpriteBatch SpriteBatch { get; set; }
        
        private void RecalculateDimensions()
        {
            _widthInPixels = TilesHorizontally * TileSizeInPixels;
            _heightInPixels = TilesVertically * TileSizeInPixels;

            this._outerRectangle = new Rectangle(X, Y, X + this.WidthInPixels, Y + this.HeightInPixels);
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

        public GameBoard(Game game, Texture2D baseTileTexture, SpriteBatch spriteBatch) : this(game, baseTileTexture, spriteBatch, "Unnamed", 10, 10, 64) { }

        public GameBoard(Game game, Texture2D baseTileTexture, SpriteBatch spriteBatch, string name, int tilesHorizontally, int tilesVertically, int tileSizeInPixels)
            : base(game)
        {
            Name = name;
            SpriteBatch = spriteBatch;
            BaseTileTexture = baseTileTexture;
            gameBoardTiles = new Tile[tilesHorizontally, tilesVertically];
            bool[] temp = new bool[4];
            temp[0] = false;
            temp[1] = false;
            temp[2] = false;
            temp[3] = false;

            for (int x = 0; x < TilesHorizontally; x++)
            {
                for (int y = 0; y < TilesVertically; y++)
                {
                    gameBoardTiles[x, y] = new Tile(Game, this, BaseTileTexture, SpriteBatch, x, y,temp);
                }
            }
            TileSizeInPixels = tileSizeInPixels;
        
        }


        #endregion


        //public void SetBorder(Tile borderTile)
        //{
        //    WalledTile newTile = null;

        //    for (int x = 0; x < TilesHorizontally; x++)
        //    {
        //        for (int y = 0; y < TilesVertically; y++)
        //        {
        //            if (x == 0 || y == 0 || x == TilesHorizontally-1 || y == TilesVertically -1)
        //            {
        //                newTile = (WalledTile)borderTile.Clone();
        //                newTile.HorizontalIndex = x;
        //                newTile.VerticalIndex = y;
        //                Tiles[x, y] = newTile;
        //            }
        //        }
        //    }
        //}

        public bool ContainsPosition(int x, int y)
        {
            return x >= 0 && x < TilesHorizontally && y >= 0 && y < TilesVertically;
        }

        //public Tile GetTileFromPixelPosition(int x, int y)
        //{
        //    int resultX = x / TileSizeInPixels;
        //    int resultY = y / TileSizeInPixels;
        //    if (this.ContainsPosition(resultX, resultY))
        //    {
        //        return this.Tiles[resultX, resultY];
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Pixel at (" + x + "," + y + ") is not within bounds of GameBoard");
        //    }
        //}


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
            
            //if (!AutoUpdateTiles)
            //{
            //    if (!_bigImageUpdated)
            //    {
            //        CreateBigImage(gameTime);
            //        _bigImageUpdated = true;
            //    }

            //    _batch.Draw(CompleteGameBoard, new Rectangle(0,0,CompleteGameBoard.Width, CompleteGameBoard.Height), Color.White);

            //}
            //else
            //{
                for (int x = 0; x < TilesHorizontally; x++)
                {
                    for (int y = 0; y < TilesVertically; y++)
                    {
                        //TODO: let the tiles draw themselves as they are DrawableGameComponents
                        //must each Tile know the SpriteBatch?
                        //what about performance?
                        gameBoardTiles[x, y].Draw(gameTime);
                        //_batch.Draw(tileToDraw.Texture, tileToDraw.DestinationRectangle, Color.White);
                    }
                }
            //}

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (Tile tile in gameBoardTiles)
            {
                tile.Update(gameTime);
            }
        }

        #region IPlaceable Members

        public int X{get; set;}

        public int Y { get; set; }

        public void Move(Direction direction, float distance)
        {
            
        }

        #endregion
    }
}