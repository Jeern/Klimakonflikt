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

    public delegate void MoveEventHandler(Sprite sprite, EventArgs e);

    public abstract class GameBoardControllerBase : IController
    {
        
        protected Random Random = new Random();
        public GameBoard Board { get; private set; }


        public GameBoardControllerBase(GameBoard board)
        {
            this.Board = board;
        }

        public event MoveEventHandler TileCenterCrossed;
        public event MoveEventHandler UnitMoved;

        protected void OnTileCenterCrossed(Sprite sprite)
        {
            if (TileCenterCrossed != null)
            {
                TileCenterCrossed(sprite, EventArgs.Empty);
            }
        }

        protected void OnUnitMoved(Sprite sprite)
        {
            if (UnitMoved != null)
            {
                UnitMoved(sprite, EventArgs.Empty);
            }
        }

        public virtual void Update(GameTime gameTime, Sprite unitToUpdate)
        {

            int pixelsToMove = (int)(unitToUpdate.Speed * gameTime.ElapsedGameTime.Milliseconds * GameDevGame.Current.GameSpeed);
            Point newPosition = unitToUpdate.GetNewPosition(unitToUpdate.Direction, pixelsToMove);
            Point oldPosition = unitToUpdate.GetPosition();

            Point centerOfPlayersTile = Board.GetTileFromPixelPosition(unitToUpdate.GetPosition().X, unitToUpdate.GetPosition().Y).Center;
            WalledTile tile = Board.GetTileFromPixelPosition(unitToUpdate.GetPosition());

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
                
                SetWantedDirection(unitToUpdate);
                UpdateDirection(unitToUpdate);
                unitToUpdate.Move(unitToUpdate.Direction, pixelMovesLeft);

                OnTileCenterCrossed(unitToUpdate);
            }
            else
            {
                unitToUpdate.X = newPosition.X;
                unitToUpdate.Y = newPosition.Y;
            }
            OnUnitMoved(unitToUpdate);
        }

        protected abstract void UpdateDirection(Sprite controllee);
   

        protected abstract void SetWantedDirection(Sprite controllee);

        protected void WriteDebugInfo(Sprite controllee)
        {
            Tile currentTile = Board.GetTileFromPixelPosition(controllee.GetPosition());
           Console.WriteLine("Unit is at tile [" +currentTile.HorizontalIndex + "," + currentTile.VerticalIndex + "] is heading " + controllee.Direction + " and wants to go " + controllee.WantedDirection );
        }

    }
}
