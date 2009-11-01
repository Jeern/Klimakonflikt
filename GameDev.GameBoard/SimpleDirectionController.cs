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

    public class SimpleDirectionController : GameBoardControllerBase
    {
        public SimpleDirectionController(GameBoard board) : base(board) { }

        public Tile TargetTile{get;set;}

        public override void Update(GameTime gameTime, Sprite unitToUpdate)
        {
            
            base.Update(gameTime, unitToUpdate);
            
        }

        override protected void UpdateDirection(Sprite controllee)
        {
           //console.write("[ UpdateDirection begin ] ");
            Direction wantedDirection = controllee.WantedDirection;
            WalledTile tile = Board.GetTileFromPixelPosition(controllee.GetPosition());
            List<Direction> possibleDirections = tile.Exits;

           //console.write("possibleDirections :");
            foreach (Direction dir in possibleDirections)
            {
               //console.write(dir + ", ");
            }
           //console.writeLine();

            
            //if the controllee has a wish
            if (wantedDirection != Direction.None)
            {
                //...and it's possible to go that way
                if (possibleDirections.Contains(wantedDirection) && wantedDirection != DirectionHelper4.GetOppositeDirection(controllee.Direction))
                {
                    //we do it
                    controllee.Direction = wantedDirection;
                   //console.writeLine("Found wanteddirection " + wantedDirection + " open - taking it!");
                    return;
                }
            }

            //is it possible to continue where it's headed now?
            if (possibleDirections.Contains(controllee.Direction))
            {
               //console.writeLine("Usual direction " + wantedDirection + " open - taking it!");
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
           //console.write("UpdateDirection end - ");
            //WriteDebugInfo(controllee);

   
        }

        /// <summary>
        /// Returns the wanted direction of the unit, in this case a random direction except the opposite of where the unit is going
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        override protected void SetWantedDirection(Sprite controllee)
        {
            Direction mostWantedDirection = DirectionHelper4.GetDirection(controllee.GetPosition(), TargetTile.Center);
            WalledTile currentTile = Board.GetTileFromPixelPosition(controllee.GetPosition());
            Tile mostPromisingTile = currentTile.GetNeighbor(mostWantedDirection);

            List<Direction> possibleDirections = new List<Direction>();

            //find the most promising and add it
            possibleDirections.Add(mostWantedDirection);

            //now check to see if turning will provide an equally promising
            float distanceToWantedTileFromMostPromising = mostPromisingTile.Center.DistanceTo(TargetTile.Center);

            if (mostWantedDirection != Direction.None)
	{

	
            foreach (Direction dir in currentTile.GetAvailableExitsRightOrLeftOfExit(mostWantedDirection))
            {
                Tile otherPossibleTile = currentTile.GetNeighbor(dir);
                if (distanceToWantedTileFromMostPromising == otherPossibleTile.Center.DistanceTo(TargetTile.Center))
                {
                    possibleDirections.Add(dir);
                }
            }
    }        
            //foreach (Direction dir in currentTile.Exits)
            //{
            //    possibleTiles.Add((WalledTile)currentTile.GetNeighbor(dir));

            //}

            controllee.WantedDirection =  possibleDirections[Random.Next(possibleDirections.Count)];// mostWantedDirection;
            
           //console.write("[ SetWantedDirection ] ");
            //WriteDebugInfo(controllee);

        }

    }
}
