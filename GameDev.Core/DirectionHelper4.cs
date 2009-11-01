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
 
    public static class DirectionHelper4
    {
        public static DirectionChanger None = new DirectionChanger(0, 0);

        public static DirectionChanger Up = new DirectionChanger(0, -1);
        public static DirectionChanger Right = new DirectionChanger(1, 0);
        public static DirectionChanger Down = new DirectionChanger(0, 1);
        public static DirectionChanger Left = new DirectionChanger(-1, 0);

        static Random m_random;

        public static Dictionary<Direction, DirectionChanger> Offsets = null;
        public static Dictionary<Keys, DirectionChanger> KeyboardOffsets = null;
        public static Dictionary<Keys, Direction> KeyboardDirections = null;
        public static Dictionary<DirectionChanger, Direction> Directions = null;
        

        static DirectionHelper4()
        {

            m_random = new Random();

            Offsets = new Dictionary<Direction, DirectionChanger>();
            Offsets.Add(Direction.Up, Up);
            Offsets.Add(Direction.Right, Right);
            Offsets.Add(Direction.Down, Down);
            Offsets.Add(Direction.Left, Left);
            Offsets.Add(Direction.None, None);

            Directions = new Dictionary<DirectionChanger, Direction>();
            Directions.Add(Up, Direction.Up);
            Directions.Add(Right, Direction.Right);
            Directions.Add(Down, Direction.Down);
            Directions.Add(Left, Direction.Left);
            Directions.Add(None, Direction.None);

            
            KeyboardOffsets = new Dictionary<Keys, DirectionChanger>();
            KeyboardOffsets.Add(Keys.Up, Up);
            KeyboardOffsets.Add(Keys.Right, Right);
            KeyboardOffsets.Add(Keys.Down, Down);
            KeyboardOffsets.Add(Keys.Left, Left);

            
            KeyboardDirections = new Dictionary<Keys, Direction>();
            KeyboardDirections.Add(Keys.Up, Direction.Up);
            KeyboardDirections.Add(Keys.Right, Direction.Right);
            KeyboardDirections.Add(Keys.Down, Direction.Down);
            KeyboardDirections.Add(Keys.Left, Direction.Left);
        }

        #region ExtensionMethods
        public static void Offset(this IPlaceable place, Direction direction, float distance)
        {
            place.X += (int)(Offsets[direction].DeltaX * distance);
            place.Y += (int)(Offsets[direction].DeltaY * distance);
        }

        public static Point GetNewPosition(this IPlaceable place, Direction direction, float distance)
        {
            return new Point(place.X + (int)(Offsets[direction].DeltaX * distance), place.Y + (int)(Offsets[direction].DeltaY * distance));
        }

        public static Point GetNewPosition(this IPlaceable place, Direction direction)
        {
            return new Point(place.X + Offsets[direction].DeltaX, place.Y + Offsets[direction].DeltaY);
        }


        public static Point Offset(this Point point, Point offset)
        {
            return new Point(point.X + offset.X, point.Y + offset.Y);
        }

        #endregion


        public static Direction GetDirection(Point start, Point destination)
        {
            return GetDirection(destination.X - start.X, destination.Y - start.Y);
        }

        public static Direction GetDirection(int deltaX, int deltaY)
        {

            //if we are moving diagonally - restrict to east/west or north/south
            if (deltaX * deltaY != 0)
            {
                if (Math.Abs(deltaX) > Math.Abs(deltaY))
                {
                    deltaY = 0; 
                }
                else
                {
                    deltaX = 0;
                }
            }
            return DirectionHelper8.GetDirection(deltaX, deltaY);
        }

        public static Direction GetDirection(DirectionChanger deltaMove)
        {
            return GetDirection(deltaMove.DeltaX, deltaMove.DeltaY);
        }

        public static Direction GetOppositeDirection(Direction direction)
        {
            return DirectionHelper8.GetReverseDirection(direction);
        }

        public static Direction LimitDirection(Direction direction)
        {

            switch (direction)
            {
                case Direction.NorthEast:
                    return Direction.East;
                case Direction.SouthEast:
                    return Direction.East;
                case Direction.SouthWest:
                    return Direction.West;
                case Direction.NorthWest:
                    return Direction.West;
                default:
                    return direction;
            }
        }

        public static List<Direction> AllDirections
        {
            get
            {
                return
                    new List<Direction> { Direction.Up,Direction.Right,Direction.Down, Direction.Left};
            }
        }

        public static List<Direction> GetRightAndLeftTurns(Direction forward)
        {
            switch (forward)
            {
                case Direction.Up:
                    return new List<Direction>() {Direction.Right, Direction.Left };
                case Direction.Right:
                    return new List<Direction>() { Direction.Up, Direction.Down};
                case Direction.Down:
                    return new List<Direction>() { Direction.Right, Direction.Left };
                case Direction.Left:
                    return new List<Direction>() { Direction.Up, Direction.Down };
                default:
                    throw new Exception("Must use Up, Down, Right or Left!");
            }
        }

        public static Direction GetRandomDirection()
        {
            return AllDirections[m_random.Next(4)];

        }

    }
}
