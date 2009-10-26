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


        public bool ShouldDrawBackground { get; set; }
        public bool ShouldDrawContents { get; set; }
        public Point Center { get { return BackgroundDestinationRectangle.Center; } }

        public Rectangle ContentDestinationRectangle { get; set; }
        public Rectangle BackgroundDestinationRectangle { get; set; }
        
        public GameBoard GameBoard { get; set; }

        public SpriteBatch SpriteBatch { get { return GameDevGame.Current.SpriteBatch; } }


        public bool HasNeighbor(Direction direction) 
        {
            return GameBoard.ContainsTile(this.GetNewPosition(direction));
        }

        public int Width { get {return this.BackgroundGameImage.CurrentTexture.Width; }}
        public int Height { get { return this.BackgroundGameImage.CurrentTexture.Height;}}

        private int _tileIndexHorizontally, _tileIndexVertically;
        public int HorizontalIndex { get { return _tileIndexHorizontally; }  set { _tileIndexHorizontally = value; RecalculateLayout(); } }
        public int VerticalIndex { get { return _tileIndexVertically; }  set { _tileIndexVertically = value; RecalculateLayout(); } }

        private GameImage _backgroundImage, _originalBackgroundImage, _contentImage;
        public GameImage BackgroundGameImage { get { return _backgroundImage; } set { _backgroundImage = value; RecalculateLayout(); } }
        public GameImage ContentGameImage { get { return _contentImage; } set { 
            
            _contentImage = value;
            if (_contentImage != null)
            {
                RecalculateContentLayout();
            }
            
        } }
        

        public Tile(GameBoard board, GameImage gameImage) : this(board, gameImage, int.MinValue, int.MinValue) { }

        public Tile(GameBoard board, GameImage gameImage, int horizontalIndex, int verticalIndex)
            : base(horizontalIndex * gameImage.CurrentTexture.Width, verticalIndex * gameImage.CurrentTexture.Height)
        {
            this.GameBoard = board;
            this._originalBackgroundImage = this.BackgroundGameImage = gameImage;
            this.HorizontalIndex = horizontalIndex;
            this.VerticalIndex = verticalIndex;
            this.ShouldDrawBackground = GameBoard.CompleteBackground == null;
            this.ShouldDrawContents = true;
           
        }

        internal void RecalculateContentLayout()
        {
            ContentDestinationRectangle = new Rectangle(this.Center.X - ContentGameImage.CurrentTexture.Width/2 ,
                this.Center.Y - ContentGameImage.CurrentTexture.Height/2 ,
        ContentGameImage.CurrentTexture.Width, ContentGameImage.CurrentTexture.Height);

        }

        internal void RecalculateLayout()
        {
            BackgroundDestinationRectangle = new Rectangle(HorizontalIndex * BackgroundGameImage.CurrentTexture.Width + GameBoard.X,
                VerticalIndex * BackgroundGameImage.CurrentTexture.Height + GameBoard.Y, BackgroundGameImage.CurrentTexture.Width, BackgroundGameImage.CurrentTexture.Height);
            
        }


        #region ICloneable Members

        public object Clone()
        {
            Tile newTile = new Tile(GameBoard, BackgroundGameImage, HorizontalIndex, VerticalIndex);
            newTile.BackgroundDestinationRectangle = this.BackgroundDestinationRectangle;
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
            SpriteBatch.Draw(BackgroundGameImage.CurrentTexture, BackgroundDestinationRectangle, Color.White);
        }

        public void DrawContents(GameTime gametime)
        {
            SpriteBatch.Draw(ContentGameImage.CurrentTexture,ContentDestinationRectangle , Color.White);
        }

        public override void Draw(GameTime gameTime)
        {

            if (ShouldDrawBackground)
            {
                DrawBackGround(gameTime);
            }

            if (ShouldDrawContents && ContentGameImage != null)
            {
                DrawContents(gameTime);
            }

            base.Draw(gameTime);
        }

        public override string ToString()
        {
            return base.ToString() + " Index: " + HorizontalIndex + "," + VerticalIndex;
        }


        internal void Reset()
        {
            this.BackgroundGameImage = _originalBackgroundImage;
            this.ContentGameImage = null;
        }
    }
}
