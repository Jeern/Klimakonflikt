using System;
using System.Collections.Generic;
using GameDev.Core;
#if SILVERLIGHT
using SilverlightHelpers;
#endif

namespace GameDev.GameBoard.AI
{
    public class GameBoardPath : IComparable<GameBoardPath>, ICloneable
    {
        public Tile FirstTile { get; set; }
        public Tile LastTile { get; set; }
        public List<Direction> Path { get; set; }
        public int DistanceToTarget { get; private set; }

        public GameBoardPath()
        {
            Path = new List<Direction>();
        }

        public GameBoardPath(Tile firstTile): this(firstTile, firstTile) {}


        public GameBoardPath(Tile firstTile, Tile lastTile) : this()
        {
            this.FirstTile = firstTile;
            this.LastTile = lastTile;
        }

        #region IComparable<GameBoardPath> Members

        public int CompareTo(GameBoardPath other)
        {
            return this.DistanceToTarget.CompareTo(other.DistanceToTarget);
        }

        #endregion

        #region ICloneable Members

        public object Clone()
        {
            GameBoardPath clone = new GameBoardPath(this.FirstTile, this.LastTile);
            foreach (Direction dir in Path)
            {
                clone.Path.Add(dir);
            }

            return clone;
        }

        #endregion
    }
}
