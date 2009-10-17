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
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using GameDev.Core;

namespace GameDev.GameBoard
{
    public class Tile : Placeable
    {
        private bool[] borders;
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

        public int TilePixelWidth { get { return this.Texture.Width; }}
        public int TilePixelHeight{ get { return this.Texture.Height;}}

        private int tileIndexHorizontally, tileIndexVertically;
        public int HorizontalIndex { get { return this.tileIndexHorizontally; }  set { this.tileIndexHorizontally = value; RecalculateLayout(); } }
        public int VerticalIndex { get { return this.tileIndexVertically; }  set { tileIndexVertically = value; RecalculateLayout(); } }

        private Texture2D _texture;
        public Texture2D Texture { get { return _texture; } set { _texture = value; RecalculateLayout(); } }

        //public Tile(Game game, GameBoard board, Texture2D texture, SpriteBatch spriteBatch, bool hasLeftBorder) : this(game, board, texture, spriteBatch, int.MinValue, int.MinValue) { }

        public Tile(Game game, GameBoard board, Texture2D texture, SpriteBatch spriteBatch, int horizontalIndex, int verticalIndex,bool[] borders)
            : base(game, horizontalIndex * texture.Width, verticalIndex * texture.Height)
        {
            this.borders = borders;
            this.GameBoard = board;
            this.Texture = texture;
            this.SpriteBatch = spriteBatch;
            this.HorizontalIndex = horizontalIndex;
            this.VerticalIndex = verticalIndex;
           
        }


        protected void RecalculateLayout()
        {
            DestinationRectangle = new Rectangle(HorizontalIndex * Texture.Width, VerticalIndex * Texture.Height, Texture.Width, Texture.Height);
            
        }


        #region ICloneable Members

        //public object Clone()
        //{
        //    Tile newTile = new Tile(Game, GameBoard, Texture, SpriteBatch, HorizontalIndex, VerticalIndex);
        //    newTile.DestinationRectangle = this.DestinationRectangle;
        //    return newTile;
        //}

        #endregion


        public override void Draw(GameTime gameTime)
        {

            SpriteBatch.Draw(Texture, DestinationRectangle, Color.White);
                base.Draw(gameTime);
        }
    }
}