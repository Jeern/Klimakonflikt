using System;
using GameDev.Core;
using GameDev.Core.Control;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
#else
using Microsoft.Xna.Framework;
#endif

namespace GameDev.GameBoard
{


    public abstract class GameBoardControllerBase : IController
    {
        
        protected Random Random = new Random();
        public GameBoard Board { get; private set; }


        public GameBoardControllerBase(GameBoard board)
        {
            this.Board = board;
        }

        private event EventHandler<EventArgs<Sprite>> m_UnitMoved = delegate { };

        public event EventHandler<EventArgs<Sprite>> UnitMoved
        {
            add { m_UnitMoved += value; }
            remove { m_UnitMoved -= value; }
        }

        private event EventHandler<EventArgs<Sprite>> m_TileCenterCrossed = delegate { };
        
        public event EventHandler<EventArgs<Sprite>> TileCenterCrossed
        {
            add { m_TileCenterCrossed += value; }
            remove { m_TileCenterCrossed -= value; }
        }
        
        //TODO: implement EventArgs specialized to include the Sprite
        protected void OnTileCenterCrossed(Sprite sprite)
        {
            m_TileCenterCrossed(this, new EventArgs<Sprite>(sprite));
        }

        protected void OnUnitMoved(Sprite sprite)
        {
            m_UnitMoved(this, new EventArgs<Sprite>(sprite));
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
