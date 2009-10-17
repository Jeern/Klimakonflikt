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

namespace GameDev.Core
{
    public enum Direction4
    {
        Up = 1,
        Right = 4,
        Down = 16,
        Left = 64
    }

    public enum Direction8
    {
        North = 1,
        NorthEast = 2,
        East = 4,
        SouthEast = 8,
        South = 16,
        SouthWest  = 32,
        West = 64,
        NorthWest = 128
    }

    public struct Offsetter
    {
        public Offsetter (int deltaX, int deltaY)
	    {
            this.DeltaX = deltaX;
            this.DeltaY = deltaY;
	    }

        public int DeltaX;
        public int DeltaY;
    }

    public static class DirectionHelper
    {
        public static Offsetter North = new Offsetter(0, -1);
        public static Offsetter NorthEast = new Offsetter(1, -1);
        public static Offsetter East = new Offsetter(1, 0);
        public static Offsetter SouthEast = new Offsetter(1, 1);
        public static Offsetter South = new Offsetter(0, 1);
        public static Offsetter SouthWest = new Offsetter(-1, 1);
        public static Offsetter West = new Offsetter(-1, 0);
        public static Offsetter NorthWest = new Offsetter(-1, -1);

        public static Offsetter Up = new Offsetter(0, -1);
        public static Offsetter Right = new Offsetter(1, 0);
        public static Offsetter Down = new Offsetter(0, -1);
        public static Offsetter Left = new Offsetter(-1, 0);

        //public static IPlaceable Offset(this IPlaceable place, Direction4 direction)
        //{

        //}


    }
}
