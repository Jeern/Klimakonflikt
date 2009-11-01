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
    public class WalledBoardPathFinder
    {

        public GameBoardPath GetShortestPath(WalledTile startTile, WalledTile endTile)
        {
            GameBoardPath pathSoFar = new GameBoardPath();


            return pathSoFar;
        }

        protected void GetShortestPath(WalledTile targetTile, GameBoardPath pathSoFar)
        {
            //try all neighbortiles and see if they are closer to the target

            foreach (Direction dir in ((WalledTile)pathSoFar.LastTile).Exits)
            {
                Tile lastTile = pathSoFar.LastTile;
                pathSoFar.Path.Add(dir);
                pathSoFar.LastTile = pathSoFar.LastTile.GetNeighbor(dir);
                if (pathSoFar.LastTile == targetTile)
                {
                    return;
                }
                else
                {
                    GetShortestPath(targetTile, pathSoFar);
                }
                pathSoFar.Path.RemoveAt(pathSoFar.Path.Count - 1);
                pathSoFar.LastTile = lastTile;
            }
        }
    }
}
