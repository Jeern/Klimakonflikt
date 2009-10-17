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
    public class Tile : Placeable,  ICloneable
    {
        private Rectangle _destinationRectangle;
        public Point Center { get { return DestinationRectangle.Center; } }
        public Rectangle DestinationRectangle { get{return _destinationRectangle;}
            private set { _destinationRectangle = value; }
        }
        public GameBoard GameBoard { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        public bool HasNeighbor(Direction direction) 
        {
            return GameBoard.ContainsTile(this.GetNewPosition(direction));
        }

        public int Width { get {return this.BackgroundGameImage.CurrentTexture.Width; }}
        public int Height { get { return this.BackgroundGameImage.CurrentTexture.Height;}}

        private int _tileIndexHorizontally, _tileIndexVertically;
        public int HorizontalIndex { get { return _tileIndexHorizontally; }  set { _tileIndexHorizontally = value; RecalculateLayout(); } }
        public int VerticalIndex { get { return _tileIndexVertically; }  set { _tileIndexVertically = value; RecalculateLayout(); } }

        private GameImage _gameImage;
        public GameImage BackgroundGameImage { get { return _gameImage; } set { _gameImage = value; RecalculateLayout(); } }
        public GameImage ContentGameImage { get; set; }

        public Tile(Game game, GameBoard board, GameImage gameImage, SpriteBatch spriteBatch) : this(game, board, gameImage, spriteBatch, int.MinValue, int.MinValue) { }

        public Tile(Game game, GameBoard board, GameImage gameImage, SpriteBatch spriteBatch, int horizontalIndex, int verticalIndex)
            : base(game, horizontalIndex * gameImage.CurrentTexture.Width, verticalIndex * gameImage.CurrentTexture.Height)
        {
            this.GameBoard = board;
            this.BackgroundGameImage = gameImage;
            this.SpriteBatch = spriteBatch;
            this.HorizontalIndex = horizontalIndex;
            this.VerticalIndex = verticalIndex;
           
        }


        internal void RecalculateLayout()
        {
            DestinationRectangle = new Rectangle(HorizontalIndex * BackgroundGameImage.CurrentTexture.Width + GameBoard.X,
                VerticalIndex * BackgroundGameImage.CurrentTexture.Height + GameBoard.Y, BackgroundGameImage.CurrentTexture.Width, BackgroundGameImage.CurrentTexture.Height);
            
        }


        #region ICloneable Members

        public object Clone()
        {
            Tile newTile = new Tile(Game, GameBoard, BackgroundGameImage, SpriteBatch, HorizontalIndex, VerticalIndex);
            newTile.DestinationRectangle = this.DestinationRectangle;
            return newTile;
        }

        #endregion


        public override void Update(GameTime gameTime)
        {
            BackgroundGameImage.Update(gameTime);
            if(ContentGameImage != null) ContentGameImage.Update(gameTime);
            base.Update(gameTime);
        }

        public void DrawBackGround(GameTime gameTime)
        { 
        }

        public override void Draw(GameTime gameTime)
        {

            SpriteBatch.Draw(BackgroundGameImage.CurrentTexture, DestinationRectangle, Color.White);
            if(ContentGameImage != null) SpriteBatch.Draw(ContentGameImage.CurrentTexture, DestinationRectangle, Color.White);

                base.Draw(gameTime);
        }

        public override string ToString()
        {
            return base.ToString() + " Index: " + HorizontalIndex + "," + VerticalIndex;
        }

    }
}
