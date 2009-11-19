using GameDev.Core;

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
