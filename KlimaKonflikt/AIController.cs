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

namespace KlimaKonflikt
{
    public class RandomController : IController
    {
        private Random Random = new Random();
        public GameBoard Board { get; private set; }

        public RandomController(GameBoard board)
        {
            this.Board = board;
        }

        public void Update(GameTime gameTime, Sprite unitToUpdate)
        {

            int pixelsToMove = (int)(unitToUpdate.Speed * gameTime.ElapsedGameTime.Milliseconds * GameDevGame.Current.GameSpeed);
            Point newPosition = unitToUpdate.GetNewPosition(unitToUpdate.Direction, pixelsToMove);
            Point oldPosition = unitToUpdate.GetPosition();

            Point centerOfPlayersTile = Board.GetTileFromPixelPosition(unitToUpdate.GetPosition().X, unitToUpdate.GetPosition().Y).Center;
            WalledTile tile = Board.GetTileFromPixelPosition(unitToUpdate.GetPosition());

            if (unitToUpdate.Direction == Direction.None)
            {
                unitToUpdate.Direction = tile.GetRandomExit();
            }

            if (GeometryTools.IsBetweenPoints(centerOfPlayersTile, newPosition, oldPosition))
            {
                //we are going to cross the center
                //first move to center
                Point tempPosition = centerOfPlayersTile;
                unitToUpdate.SetPosition(centerOfPlayersTile);

                int pixelMovesLeft = int.MinValue;
                DirectionChanger deltaMoves = DirectionHelper4.Offsets[unitToUpdate.Direction];
                if (deltaMoves.DeltaX != 0) //we are moving horizontally
                {
                    pixelMovesLeft = Math.Abs(newPosition.X - oldPosition.X);
                }
                else //we are moving vertically
                {
                    pixelMovesLeft = Math.Abs(newPosition.Y - oldPosition.Y);
                }

                unitToUpdate.WantedDirection = GetWantedDirection(unitToUpdate);
                unitToUpdate.Direction = unitToUpdate.WantedDirection;
                unitToUpdate.Move(unitToUpdate.Direction, pixelMovesLeft);
                //debug info
            }
            else
            {
                unitToUpdate.X = newPosition.X;
                unitToUpdate.Y = newPosition.Y;
            }
        }

        private Direction GetWantedDirection(Sprite unit)
        {

            WalledTile tile = Board.GetTileFromPixelPosition(unit.GetPosition());
            List<Direction> possibleDirections = tile.Exits;

            Direction wantedDirection = unit.WantedDirection;
            
            //we are currently standing still
            if (wantedDirection == Direction.None)
            {
                //try to find an available exit from the tile
                if (possibleDirections.Count > 0)
                {
                    wantedDirection = possibleDirections[Random.Next(possibleDirections.Count - 1)];
                }

            }
            //first check if we can exit right or left from this tile
            List<Direction> rightAndLeft = DirectionHelper4.GetRightAndLeftTurns(unit.Direction);

            //if it's possible to turn ... and we want to (70 % chance) ...
            if ((possibleDirections.Contains(rightAndLeft[0]) || possibleDirections.Contains(rightAndLeft[1])) && Random.Next(10) < 7)
            {
                List<Direction> possibleSelections = new List<Direction>();
                foreach (Direction dir in rightAndLeft)
                {
                    if (possibleDirections.Contains(dir))
                    {
                        possibleSelections.Add(dir);
                    }
                }

                return possibleSelections[Random.Next(possibleSelections.Count - 1)];

            }
            else
            {
                //we can't turn right or left!
                //if we can't go forward either - then try to turn back
                if (!possibleDirections.Contains(unit.Direction))
                {
                    wantedDirection = DirectionHelper4.GetOppositeDirection(unit.Direction);
                    if (!possibleDirections.Contains(wantedDirection))
                    {
                        wantedDirection = wantedDirection = rightAndLeft[Random.Next(rightAndLeft.Count - 1)];
                    }
                }
                //can we turn back?
            }
            return wantedDirection;
        }
    }
}
