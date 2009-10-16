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

namespace GameDev.Core
{
    public class Placeable : DrawableGameComponent, IPlaceable
    {
        private int _x, _xOffset;
        private int _y, _yOffset;

        public Placeable(Game game, int x, int y) : this (game, x, y, 0,0)
        {
        }

        public Placeable(Game game, int x, int y, int xOffset, int yOffset):base(game)
        {
            X = x;
            Y = y;
            XOffset = xOffset;
            YOffset = yOffset;
        }

        #region IPlaceable Members

        public int X
        {
            get { return _x; }
            set { _x = value;}
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int XOffset
        {
            get { return _xOffset; }
            set { _xOffset = value; }
        }

        public int YOffset
        {
            get { return _yOffset; }
            set { _yOffset = value; }
        }

        public void Move(Direction direction, float distance)
        {
            DirectionHelper.Offset(this, direction, distance);
        }

        public void ChangeOffset(Direction direction, float distance)
        {
            this.XOffset += DirectionHelper.Offsets[direction].DeltaX;
            this.YOffset += DirectionHelper.Offsets[direction].DeltaY;
        }


        #endregion
    }
}
