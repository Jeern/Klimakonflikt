using System;
using System.Collections.Generic;
using System.Linq;
using GameDev.Core;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.GameBoard.AI
{

    public class A_StarController : GameBoardControllerBase
    {
        public Path<WalledTile>  Path { get; private set; }
        private Func<WalledTile, double> m_estimate;
        public List<Path<WalledTile>> PathsTried = new List<Path<WalledTile>>();
        public int AcceptableCost { get; set; }
        public A_StarController(GameBoard board) : base(board) 
        {
            m_estimate = x => { return 0; };
            TargetTiles = new List<WalledTile>();
        }
        public A_StarController(GameBoard board, Func<WalledTile, double> estimate) : this(board)
        {
            m_estimate = estimate;
        }


        public List<WalledTile> TargetTiles { get; set; }

        public override void Update(GameTime gameTime, Sprite unitToUpdate)
        {
            base.Update(gameTime, unitToUpdate);
        }

        override protected void UpdateDirection(Sprite controllee)
        {

            Direction wantedDirection = controllee.WantedDirection;
            WalledTile currentTile = Board.GetTileFromPixelPosition(controllee.GetPosition());
            List<Direction> possibleDirections = currentTile.Exits;

            //if the controllee has a wish
            if (wantedDirection != Direction.None)
            {
                //...and it's possible to go that way
                if (possibleDirections.Contains(wantedDirection))
                {
                    //we do it
                    controllee.Direction = wantedDirection;
                    return;
                }
            }
            bool acceptablePathFound = false;
            int targetIndex = 0;

            Path<WalledTile> _path = null;
            PathsTried.Clear();
            while (!acceptablePathFound && targetIndex < TargetTiles.Count)
            {
                WalledTile target = TargetTiles[targetIndex];
               
                 _path = A_StarPathFinder.FindPath<WalledTile>(currentTile, target, (w1, w2) => { return w1.Center.DistanceTo(w2.Center); }, m_estimate);
                PathsTried.Add(_path);
                acceptablePathFound = (_path.TotalCost + m_estimate(target) )< AcceptableCost;
                targetIndex++;
            }
            if (!acceptablePathFound)
            {
                Path = PathsTried[0];
                if (TargetTiles.Count == 1)
                {
                    int i = 9;
                }
            }
            else
	{
                Path = _path;
	}

            
            List<WalledTile> reversed = Path.Reverse<WalledTile>().ToList();
            reversed.RemoveAt(0);
            WalledTile tileToGoto = null;
            if (reversed.Count() > 0)
            {
                tileToGoto = reversed.First();
                controllee.Direction = DirectionHelper4.GetDirection(controllee.GetPosition(), tileToGoto.Center);
            }
            else
            {
                controllee.Direction = currentTile.Exits[Random.Next(currentTile.Exits.Count)];
            }
        }

        /// <summary>
        /// Returns the wanted direction of the unit, in this case a random direction except the opposite of where the unit is going
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        override protected void SetWantedDirection(Sprite controllee)
        {
        }
    }
}