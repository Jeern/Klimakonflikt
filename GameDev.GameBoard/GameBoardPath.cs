#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
using GameDev.GameBoard;
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;

#endregion


namespace GameDev.GameBoard
{
    public class GameBoardPath : IComparable<GameBoardPath> , ICloneable
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
