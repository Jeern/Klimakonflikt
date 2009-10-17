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

    public static class DirectionHelper8
    {
        public static DirectionChanger None = new DirectionChanger(0, 0);

        public static DirectionChanger North = new DirectionChanger(0, -1);
        public static DirectionChanger NorthEast = new DirectionChanger(1, -1);
        public static DirectionChanger East = new DirectionChanger(1, 0);
        public static DirectionChanger SouthEast = new DirectionChanger(1, 1);
        public static DirectionChanger South = new DirectionChanger(0, 1);
        public static DirectionChanger SouthWest = new DirectionChanger(-1, 1);
        public static DirectionChanger West = new DirectionChanger(-1, 0);
        public static DirectionChanger NorthWest = new DirectionChanger(-1, -1);

        public static Dictionary<Direction, DirectionChanger> Offsetters = null;
        public static Dictionary<Keys, DirectionChanger> KeyboardOffsetters = null;
        public static Dictionary<Keys, Direction> KeyboardDirections = null;
        public static Dictionary<DirectionChanger, Direction> Directions = null;

        static DirectionHelper8()
        {
            Offsetters = new Dictionary<Direction, DirectionChanger>();

            Offsetters.Add(Direction.North, North);
            Offsetters.Add(Direction.NorthEast, NorthEast);
            Offsetters.Add(Direction.East, East);
            Offsetters.Add(Direction.SouthEast, SouthEast);
            Offsetters.Add(Direction.South, South);
            Offsetters.Add(Direction.SouthWest, SouthWest);
            Offsetters.Add(Direction.West, West);
            Offsetters.Add(Direction.NorthWest, NorthWest);
            Offsetters.Add(Direction.None, None);


            Directions = new Dictionary<DirectionChanger, Direction>();
            Directions.Add(North, Direction.North);
            Directions.Add(NorthEast, Direction.NorthEast);
            Directions.Add(East, Direction.East);
            Directions.Add(SouthEast, Direction.SouthEast);
            Directions.Add(South, Direction.South);
            Directions.Add(SouthWest, Direction.SouthWest);
            Directions.Add(West, Direction.West);
            Directions.Add(NorthWest, Direction.NorthWest);
            Directions.Add(None, Direction.None);

            
            KeyboardOffsetters = new Dictionary<Keys, DirectionChanger>();
            KeyboardOffsetters.Add(Keys.Up, North);
            KeyboardOffsetters.Add(Keys.Right, East);
            KeyboardOffsetters.Add(Keys.Down, South);
            KeyboardOffsetters.Add(Keys.Left, West);

            
            KeyboardDirections = new Dictionary<Keys, Direction>();
            KeyboardDirections.Add(Keys.Up, Direction.North);
            KeyboardDirections.Add(Keys.Right, Direction.East);
            KeyboardDirections.Add(Keys.Down, Direction.South);
            KeyboardDirections.Add(Keys.Left, Direction.West);

        }

        public static Direction GetDirection(int deltaX, int deltaY)
        {
            //TODO:
            DirectionChanger changer = new DirectionChanger(Math.Sign(deltaX), Math.Sign(deltaY));
            return Directions[changer];
        }

    }
}
