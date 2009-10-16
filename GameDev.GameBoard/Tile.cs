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
    public class Tile : Placeable,  ICloneable
    {
        Rectangle _destinationRectangle;
        public Rectangle DestinationRectangle { get{return _destinationRectangle;}
            private set { _destinationRectangle = value; }
        }

        public GameBoard GameBoard { get; set; }
        
        public bool HasBorderTop { get; set; }
        public bool HasBorderLeft { get; set; }
        public bool HasBorderBottom { get; set; }
        public bool HasBorderRight { get; set; }

        public bool HasNeighbor(Direction direction) 
        {
            return GameBoard.ContainsPosition(this.GetNewPosition(direction, 1));
        }


        public int Width { get {return this.Texture.Width; }}
        public int Height { get { return this.Texture.Height;}}

        private int _tileIndexHorizontally, _tileIndexVertically;
        public int HorizontalIndex { get { return _tileIndexHorizontally; }  set { _tileIndexHorizontally = value; RecalculateDestinationRectangle(); } }
        public int VerticalIndex { get { return _tileIndexVertically; }  set { _tileIndexVertically = value; RecalculateDestinationRectangle(); } }

        private Texture2D _texture;
        public Texture2D Texture { get { return _texture; } set { _texture = value; RecalculateDestinationRectangle(); } }

        public Tile(Game game, GameBoard board, Texture2D texture) : this(game, board, texture, int.MinValue, int.MinValue) {}

        public Tile(Game game, GameBoard board, Texture2D texture, int horizontalIndex, int verticalIndex): base (game, horizontalIndex * texture.Width, verticalIndex * texture.Height)
        {
            this.GameBoard = board;
            this.Texture = texture;
            this.HorizontalIndex = horizontalIndex;
            this.VerticalIndex = verticalIndex;
           
        }


        protected void RecalculateDestinationRectangle()
        {
            DestinationRectangle = new Rectangle(HorizontalIndex * Texture.Width, VerticalIndex * Texture.Height, Texture.Width, Texture.Height);
        }


        #region ICloneable Members

        public object Clone()
        {
            Tile newTile = new Tile(Game, GameBoard, Texture, HorizontalIndex, VerticalIndex);
            newTile.DestinationRectangle = this.DestinationRectangle;
            return newTile;
        }

        #endregion


        public override void Draw(GameTime gameTime)
        {
                base.Draw(gameTime);

        }
    }
}
