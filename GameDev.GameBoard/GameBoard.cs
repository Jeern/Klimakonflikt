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

        //public Texture2D CompleteGameBoard { get; set; }
        //private bool _autoUpdateTiles = true;
        
        //private bool _bigImageUpdated = false;

        //public bool AutoUpdateTiles {
        //    get { return _autoUpdateTiles; }
        //    set { this._autoUpdateTiles = value;}
        //}

        //int _xOffset, _yOffset;
        //public int XOffset { get { return _xOffset; }
        //    set {

        //        foreach (Tile tile in Tiles)
        //        {
        //            UpdateTileOffsets();
        //        }
        //    }
        //}

        //void UpdateTileOffsets()
        //{
        //    foreach (Tile tile in Tiles)
        //    {

        //    }
        //}

        //private void CreateBigImage(GameTime gametime)
        //{

        //    //source: http://www.krissteele.net/blogdetails.aspx?id=146

        //    // Create the new RenderTarget
        //    RenderTarget2D tileRenderTarget = new RenderTarget2D(Game.GraphicsDevice, this.WidthInPixels,this.HeightInPixels, 1, SurfaceFormat.Color);

        //    // Get the original target (so we can set it back later)
        //    RenderTarget oldTarget = Game.GraphicsDevice.GetRenderTarget(0);

        //    // Use the new RenderTarget for drawing
        //    Game.GraphicsDevice.SetRenderTarget(0, tileRenderTarget);

        //    // Clear with a transparent color so that our new Texture2D is transparent in areas
        //    // that don't have tiles on them.
        //    Game.GraphicsDevice.Clear(ClearOptions.Target, new Color(255, 255, 255, 0), 0, 0);

        //    Draw(gametime);
        //    // Set the graphics device back to status quo
        //    Game.GraphicsDevice.SetRenderTarget(0, oldTarget as RenderTarget2D);

        //    // Get our new tile texture
        //    CompleteGameBoard = tileRenderTarget.GetTexture();
        //}

        public string Name { get; set; }
        public WalledTile[,] Tiles { get; protected set; }

        public GameImage BaseImage{ get; set; }

        private Rectangle _outerRectangle;

        public Rectangle OuterRectangle { get { return _outerRectangle; } }
        
        private int _widthInPixels = int.MinValue;
        private int _heightInPixels = int.MinValue;

        public int WidthInPixels { get { return _widthInPixels; } }
        public int HeightInPixels { get { return _heightInPixels; } }

        public int TilesHorizontally { get { return Tiles.GetLength(0); } }
        public int TilesVertically { get { return Tiles.GetLength(1); } }

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

        public GameBoard(Game game, GameImage baseGameImage, SpriteBatch spriteBatch) : this(game, baseGameImage, spriteBatch, "Unnamed", 10, 10, 64) { }

        public GameBoard(Game game, GameImage baseGameImage, SpriteBatch spriteBatch, string name, int tilesHorizontally, int tilesVertically, int tileSizeInPixels)
            : base(game)
        {
            Name = name;
            SpriteBatch = spriteBatch;
            BaseImage = baseGameImage;
            Tiles = new WalledTile[tilesHorizontally, tilesVertically];
            for (int x = 0; x < TilesHorizontally; x++)
            {
                for (int y = 0; y < TilesVertically; y++)
                {
                    Tiles[x, y] = new WalledTile(Game, this, baseGameImage, SpriteBatch, x, y);
                }
            }
            TileSizeInPixels = tileSizeInPixels;
        
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
            int resultX = x / TileSizeInPixels;
            int resultY = y / TileSizeInPixels;
            if (this.ContainsPosition(resultX, resultY))
            {
                return this.Tiles[resultX, resultY];
            }
            else
            {
                throw new ArgumentException("Pixel at (" + x + "," + y + ") is not within bounds of GameBoard");
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
                        Tiles[x, y].Draw(gameTime);
                        //_batch.Draw(tileToDraw.Texture, tileToDraw.DestinationRectangle, Color.White);
                    }
                }
            //}

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

        public int X{get; set;}

        public int Y { get; set; }

        public void Move(Direction direction, float distance)
        {
            
        }

        #endregion
    }
}