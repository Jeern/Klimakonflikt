using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using GameDev.Core;
using GameDev.Core.Graphics;
using Microsoft.Xna.Framework;
using GameDev.Core.Sequencing;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using GameDev.GameBoard;
using System.Collections.Generic;

namespace KlimaKonflikt
{
    public class KKMonster : Sprite
    {
        public Direction WantedDirection = Direction.None;
        public Random Random = new Random();
        //public Direction WantedDirection(WalledTile tile)
        //{
        //    m_WantedDirection = RandomDirection(m_WantedDirection, tile);  
        //    return m_WantedDirection; 
        //}

        private SequencedIterator<Direction> m_DirectionIterator;
        private SequencedIterator<int> m_DelayMillisecondsIterator;

        public KKMonster(GameImage gameImage, float speed, Point startingPosition, SequencedIterator<Direction> directionIterator, SequencedIterator<int> delayMillisecondsIterator)
            : base(gameImage, speed, startingPosition)
        {
            m_DirectionIterator = directionIterator;
            m_DelayMillisecondsIterator = delayMillisecondsIterator;
        }

        public KKMonster(GameImage gameImage, float speed, Point startingPosition, SequencedIterator<Direction> directionIterator, int delayMilliseconds)
            : this(gameImage, speed, startingPosition, directionIterator, new SequencedIterator<int>(new StaticSequencer(), delayMilliseconds))
        {
        }

          private Direction RandomDirection(Direction current, WalledTile tile)
        {
            var directions = new List<Direction>()
            {
                Direction.Up,
                Direction.Down,
                Direction.Left,
                Direction.Right 
            };

            directions.Remove(OppositeDirection(current));
            
            while (directions.Count > 0)
            {
                //RealRandom random = new RealRandom(0, directions.Count-1);
                Direction newDirection = directions[Random.Next(0, directions.Count-1)];
                Console.WriteLine("NewDirection: " + newDirection);
                int directionIndex = DirectionIndex(newDirection, tile);  
                if (!tile.HasBorder(newDirection) && directionIndex > 0 && directionIndex < 9)
                    return newDirection;

                directions.Remove(newDirection); 
            }
            return OppositeDirection(current);                                    
        }

        private Direction OppositeDirection(Direction current)
        {
            if (current == Direction.Left)
                return Direction.Right;

            if (current == Direction.Right)
                return Direction.Left;

            if (current == Direction.Up)
                return Direction.Down;

            if (current == Direction.Down)
                return Direction.Up;

            return Direction.None;
        }

        private int DirectionIndex(Direction current, Tile tile)
        {
            if (current == Direction.Down)
                return tile.VerticalIndex + 1;
            if (current == Direction.Up)
                return tile.VerticalIndex - 1;
            if (current == Direction.Right)
                return tile.HorizontalIndex + 1;
            if (current == Direction.Left)
                return tile.HorizontalIndex - 1;
            return 0;
        }



    }        
}
