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
    public enum Direction
    {
        Up = 1,
        Right = 4,
        Down = 16,
        Left = 64,
        North = 1,
        NorthEast = 2,
        East = 4,
        SouthEast = 8,
        South = 16,
        SouthWest  = 32,
        West = 64,
        NorthWest = 128
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

        public static Dictionary<Direction, Offsetter> Offsets = null;

        static DirectionHelper()
        {
            Offsets = new Dictionary<Direction, Offsetter>();
            Offsets.Add(Direction.Up, Up);
            Offsets.Add(Direction.Right, Right);
            Offsets.Add(Direction.Down, Down);
            Offsets.Add(Direction.Left, Left);
            Offsets.Add(Direction.North, North);
            Offsets.Add(Direction.NorthEast, NorthEast);
            Offsets.Add(Direction.East, East);
            Offsets.Add(Direction.SouthEast, SouthEast);
            Offsets.Add(Direction.South, South);
            Offsets.Add(Direction.SouthWest, SouthWest);
            Offsets.Add(Direction.West, West);
            Offsets.Add(Direction.NorthWest, NorthWest);
        }

        public static void Offset(this IPlaceable place, Direction direction, float distance)
        {
            place.X += (int)(Offsets[direction].DeltaX * distance);
            place.Y += (int)( Offsets[direction].DeltaY * distance);
        }

        public static Point GetNewPosition(this IPlaceable place, Direction direction, float distance)
        {
            return new Point((int)(Offsets[direction].DeltaX * distance), (int)(Offsets[direction].DeltaY * distance));
        }

        public static Point GetNewPosition(this IPlaceable place, Direction direction)
        {
            return new Point(Offsets[direction].DeltaX , Offsets[direction].DeltaY);
        }

    }
}
