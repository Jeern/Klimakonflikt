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
using GameDev.GameBoard;
using GameDev.Core.Graphics;
using System.Diagnostics;
using GameDev.Core.Control;


namespace GameDev.GameBoard
{

    public class A_StarController : GameBoardControllerBase
    {
        public Path<WalledTile>  Path { get; private set; }
        private Func<WalledTile, double> m_estimate;
        public A_StarController(GameBoard board) : base(board) 
        {
            m_estimate = x => { return 0; };
        }
        public A_StarController(GameBoard board, Func<WalledTile, double> estimate) : base(board)
        {
            m_estimate = estimate;
        }


        public WalledTile TargetTile { get; set; }

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
//            DateTime start = DateTime.Now;
            Path = A_StarPathFinder.FindPath<WalledTile>(currentTile, TargetTile, (w1, w2) => { return w1.Center.DistanceTo(w2.Center); }, m_estimate);
            if (Path.TotalCost > 100)
            {
                int i = 9;
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


//            Console.WriteLine(DateTime.Now.Second);
  //          DateTime end = DateTime.Now;
    //        TimeSpan timetaken = end - start;
      //      Console.WriteLine((int)timetaken.TotalMilliseconds);

            //foreach (WalledTile atile in reversed)
            //{
            //    Console.WriteLine(atile.ToString());
            //}

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