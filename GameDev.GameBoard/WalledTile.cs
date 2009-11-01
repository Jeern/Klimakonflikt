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
using GameDev.Core.Graphics;

namespace GameDev.GameBoard
{
	public class WalledTile : Tile
	{
        private static Random m_random = new Random();

        public WallSet Walls { get; private set; }

        public WalledTile(GameBoard board, GameImage gameImage) : this(board, gameImage, int.MinValue, int.MinValue) { }

        public WalledTile(GameBoard board, GameImage gameImage, int horizontalIndex, int verticalIndex)
            : base(board , gameImage, horizontalIndex , verticalIndex)
        {
            this.Walls = new WallSet();
            this.Walls.HasLeftBorder = (HorizontalIndex == 0);
            this.Walls.HasTopBorder = (VerticalIndex == 0);
            this.Walls.HasBottomBorder = (verticalIndex == board.TilesVertically - 1);
            this.Walls.HasRightBorder = (horizontalIndex == board.TilesHorizontally - 1);
        }


        public bool HasBorder(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:  //also North
                    return Walls.HasTopBorder;
                case Direction.Right:   //also East
                    return Walls.HasRightBorder;
                case Direction.Down:    //also South
                    return Walls.HasBottomBorder;
                case Direction.Left:    //also West
                    return Walls.HasLeftBorder;
                case Direction.None:    //also West
                    return false;
                default:
                    throw new Exception("Direction '" + direction + "' is not valid! Only None, Up, Down, Left, Right, North, South, East and West are supported!");
            }
        }

        public override string ToString()
        {
            return base.ToString() + " Top:" + Walls.HasTopBorder + ", Right:" + Walls.HasRightBorder + ", Bottom:" + Walls.HasBottomBorder + ", Left:" + Walls.HasLeftBorder;
        }
        List<Direction> m_exits;
        public List<Direction> Exits
        {
            
            get 
            {
                if (m_exits == null)
                {
                    m_exits = new List<Direction>();
                    foreach (Direction dir in DirectionHelper4.AllDirections)
                    {
                        if (!this.HasBorder(dir))
                        {
                            m_exits.Add(dir);
                        }
                    }
                }
                //remember to give a copy back, so people don't edit the cached copy
                return  new List<Direction>( m_exits);
            }
        }

        public Direction GetRandomExit()
        {
            List<Direction> exits = Exits;
            if (exits.Count > 0)
            {
                return exits[m_random.Next(exits.Count - 1)];
            }
            else return Direction.None;
        }

        public Direction GetExitClosestTo(Direction directionWanted)
        {
            List<Direction> exits = this.Exits;
            if (exits.Contains(directionWanted))
            {
                return directionWanted;
            }
            else
            {
                List<Direction> rightAndLeft = GetAvailableExitsRightOrLeftOfExit(directionWanted);
                if (rightAndLeft.Count > 0)
                {
                    return rightAndLeft[m_random.Next(rightAndLeft.Count - 1)];
                }
                else
                {
                    Direction oppositeDirection = DirectionHelper4.GetOppositeDirection(directionWanted);
                    if (exits.Contains (oppositeDirection))
                    {
                        return oppositeDirection;
                    }
                }
            }
            return Direction.None;
        }

        public List<Direction> GetAvailableExitsRightOrLeftOfExit(Direction direction)
        {
            List<Direction> openExits = this.Exits;
            List<Direction> rightAndLeft = DirectionHelper4.GetRightAndLeftTurns(direction);
            List<Direction> possibleSelections = new List<Direction>();
            foreach (Direction dir in rightAndLeft)
            {
                if (openExits.Contains(dir))
                {
                    possibleSelections.Add(dir);
                }
            }
            return possibleSelections;

        }

	}
}
