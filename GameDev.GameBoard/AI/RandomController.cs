﻿using System;
using System.Collections.Generic;
using GameDev.Core;

namespace GameDev.GameBoard.AI
{

    public class RandomController : GameBoardControllerBase
    {
        public RandomController(GameBoard board) : base(board){}

        override protected void UpdateDirection(Sprite controllee)
        {
            Direction wantedDirection = controllee.WantedDirection;
            WalledTile tile = Board.GetTileFromPixelPosition(controllee.GetPosition());
            List<Direction> possibleDirections = tile.Exits;


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

            //is it possible to continue where it's headed now?
            if (possibleDirections.Contains(controllee.Direction))
            {
                //just continue
                return;
            }

            else //we check to see if we can go another way without reversing
            {
                List<Direction> exits = tile.Exits;
                Direction reverse = DirectionHelper4.GetOppositeDirection(controllee.Direction);
                bool hadExitOpposite = exits.Remove(reverse);
                if (exits.Count > 0)
                {
                    controllee.Direction = exits[Random.Next(exits.Count)];
                }
                else
                {
                    if (hadExitOpposite)
                    {
                        controllee.Direction = reverse;
                    }
                    else
                    {
                        controllee.Direction = Direction.None;
                    }
                }
            }
   
        }

        /// <summary>
        /// Returns the wanted direction of the unit, in this case a random direction except the opposite of where the unit is going
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        override protected void SetWantedDirection(Sprite controllee)
        {

            WalledTile tile = Board.GetTileFromPixelPosition(controllee.GetPosition());
            List<Direction> possibleDirections = tile.Exits;
            if (controllee.Direction != Direction.None)
            {
                possibleDirections.Remove(DirectionHelper4.GetOppositeDirection(controllee.Direction));
            }
            
            if (possibleDirections.Count > 0)
            {
                controllee.WantedDirection = possibleDirections[Random.Next(possibleDirections.Count)];
            }
            else
            {
                controllee.WantedDirection = Direction.None;
            }
                
        }

    }
}
