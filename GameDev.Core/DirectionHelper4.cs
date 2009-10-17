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
        public static DirectionChanger Down = new DirectionChanger(0, -1);
        public static DirectionChanger Left = new DirectionChanger(-1, 0);

        public static Dictionary<Direction, DirectionChanger> Offsets = null;
        public static Dictionary<Keys, DirectionChanger> KeyboardOffsets = null;
        public static Dictionary<Keys, Direction> KeyboardDirections = null;
        public static Dictionary<DirectionChanger, Direction> Directions = null;

        static DirectionHelper4()
        {
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
            return new Point((int)(Offsets[direction].DeltaX * distance), (int)(Offsets[direction].DeltaY * distance));
        }

        public static Point GetNewPosition(this IPlaceable place, Direction direction)
        {
            return new Point(Offsets[direction].DeltaX, Offsets[direction].DeltaY);
        }


        public static Point Offset(this Point point, Point offset)
        {
            return new Point(point.X + offset.X, point.Y + offset.Y);
        }

        #endregion


        public static Direction GetDirection(int deltaX, int deltaY)
        {
            //TODO:
            DirectionChanger changer = new DirectionChanger(Math.Sign(deltaX), Math.Sign(deltaY));
            return Directions[changer];
        }

        public static Direction GetDirection(DirectionChanger deltaMove)
        {
            return GetDirection(deltaMove.DeltaX, deltaMove.DeltaY);
        }


    }
}
