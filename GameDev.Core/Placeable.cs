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
        private int _x;
        private int _y;

        public Placeable(Game game) : this (game, 0,0)
        {
        }

        public Placeable(Game game, int x, int y):base(game)
        {
            X = x;
            Y = y;
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

        public void Move(Direction direction, float distance)
        {
            DirectionHelper4.Offset(this, direction, distance);
        }


        public override string ToString()
        {
            return this.GetType() + " (x:" + X + ",y:" + Y + ")";
        }

        #endregion
    }
}
