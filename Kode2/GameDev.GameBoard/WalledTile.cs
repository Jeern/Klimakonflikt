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
    //public class WalledTile : Tile
    //{

    //    public WallSet Walls { get; private set; }

        //public WalledTile(Game game, GameBoard board, Texture2D texture, SpriteBatch spriteBatch) : this(game, board, texture, spriteBatch, int.MinValue, int.MinValue) { }

//<<<<<<< .mine
    //    public WalledTile(Game game, GameBoard board, Texture2D texture, SpriteBatch spriteBatch, int horizontalIndex, int verticalIndex)
    //        : base(game, board , texture, spriteBatch, horizontalIndex , verticalIndex)
    //    {
    //        this.Walls = new WallSet();
    //    //    this.Walls.HasLeftBorder = (HorizontalIndex == 0);
    //    //    this.Walls.HasTopBorder = (VerticalIndex == 0);
    //    //    this.Walls.HasBottomBorder = (X == board.TilesHorizontally-1);
    //    //    this.Walls.HasLeftBorder = (X == board.TilesVertically - 1);
    //    //
    //        this.Walls.HasLeftBorder(true);
    //    }
//=======
//        public WalledTile(Game game, GameBoard board, Texture2D texture, SpriteBatch spriteBatch, int horizontalIndex, int verticalIndex)
//            : base(game, board , texture, spriteBatch, horizontalIndex , verticalIndex)
//        {
//            this.Walls = new WallSet();
//            this.Walls.HasLeftBorder = (HorizontalIndex == 0);
//            this.Walls.HasTopBorder = (VerticalIndex == 0);
//            this.Walls.HasBottomBorder = (verticalIndex == board.TilesVertically - 1);
//            this.Walls.HasRightBorder = (horizontalIndex == board.TilesHorizontally - 1);
//        }
//>>>>>>> .r35546


    //    public bool HasBorder(Direction direction)
    //    {
    //        switch (direction)
    //        {
    //            case Direction.Up:  //also North
    //                return Walls.HasTopBorder;
    //            case Direction.Right:   //also East
    //                return Walls.HasRightBorder;
    //            case Direction.Down:    //also South
    //                return Walls.HasBottomBorder;
    //            case Direction.Left:    //also West
    //                return Walls.HasLeftBorder;
    //            default:
    //                throw new Exception("Direction '" + direction + "' is not valid! Only Up, Down, Left, Right, North, South, East and West are supported!");
    //        }
    //    }

        public override string ToString()
        {
            return base.ToString() + " Top:" + Walls.HasTopBorder + ", Right:" + Walls.HasRightBorder + ", Bottom:" + Walls.HasBottomBorder + ", Left:" + Walls.HasLeftBorder;
        }
    //}
}