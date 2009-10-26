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
        private Direction m_WantedDirection = Direction.Right;

        public Direction WantedDirection(WalledTile tile)
        {
            m_WantedDirection = RandomDirection(m_WantedDirection, tile);  
            return m_WantedDirection; 
            //Direction m_currentDirectionChoice = m_DirectionIterator.Current;
            //while (tile.HasBorder(m_currentDirectionChoice))
            //{
            //    m_DirectionIterator.MoveNext();
            //    m_currentDirectionChoice = m_DirectionIterator.Current;  
            //}

            //if (m_WantedDirection == Direction.None)
            //    m_WantedDirection = m_currentDirectionChoice;

            //if (tile.HasBorder(m_WantedDirection))
            //    m_WantedDirection = m_currentDirectionChoice;

            //return m_WantedDirection; 
        }

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

        //private TimeSpan m_LastChanged = TimeSpan.MinValue;

        //public override void Update(GameTime time)
        //{
        //    //TimeSpan newTime = time.TotalGameTime;
        //    //if ((m_LastChanged == TimeSpan.MinValue || m_LastChanged.Add(new TimeSpan(0, 0, 0, 0, m_DelayMillisecondsIterator.Current)) <= newTime))
        //    //{
        //    //    m_DirectionChangeAllowed = false;
        //    //    m_LastChanged = newTime;
        //        m_DelayMillisecondsIterator.MoveNext();
        //        m_DirectionIterator.MoveNext();
        //    //}
        //    base.Update(time);
        //}

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
            Random r = new Random();
            while (directions.Count > 0)
            {
                //RealRandom random = new RealRandom(0, directions.Count-1);
                Direction newDirection = directions[r.Next(0, directions.Count-1)];
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
