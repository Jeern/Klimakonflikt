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
        public Tile[,] Tiles { get; private set; }
        
        private int _widthInPixels = int.MinValue;
        private int _heightInPixels = int.MinValue;

        public int WidthInPixels { get { return _widthInPixels; } }
        public int HeightInPixels { get { return _heightInPixels; } }

        public int TilesWide { get { return Tiles.GetLength(0); } }
        public int TilesHigh { get { return Tiles.GetLength(1); } }

        public SpriteBatch SpriteBatch { get; set; }
        
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

        #region Constructors

        public GameBoard(Game game, Texture2D baseTileTexture, SpriteBatch spriteBatch) : this(game, baseTileTexture, spriteBatch, "Unnamed", 32, 32, 64) { }

        public GameBoard(Game game, Texture2D baseTileTexture, SpriteBatch spriteBatch, string name, int tilesHorizontally, int tilesVertically, int tileSizeInPixels)
            : base(game)
        {
            Name = name;
            SpriteBatch = spriteBatch;
            
            Tiles = new Tile[tilesHorizontally, tilesVertically];
            for (int x = 0; x < TilesWide; x++)
            {
                for (int y = 0; y < TilesHigh; y++)
                {
                    Tiles[x, y] = new Tile(game, this, baseTileTexture, SpriteBatch, x, y);
                }
            }
        
            TileSizeInPixels = TileSizeInPixels;
        
        }

        #endregion


        public void SetBorder(Texture2D borderTexture)
        {
            SetBorder(new Tile(Game, this, borderTexture, SpriteBatch));
        }

        public void SetBorder(Tile borderTile)
        {
            Tile newTile = null;

            for (int x = 0; x < TilesWide; x++)
            {
                for (int y = 0; y < TilesHigh; y++)
                {
                    if (x == 0 || y == 0 || x == TilesWide-1 || y == TilesHigh -1)
                    {
                        newTile = (Tile)borderTile.Clone();
                        newTile.HorizontalIndex = x;
                        newTile.VerticalIndex = y;
                        Tiles[x, y] = newTile;
                    }
                }
            }
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
                for (int x = 0; x < TilesWide; x++)
                {
                    for (int y = 0; y < TilesHigh; y++)
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