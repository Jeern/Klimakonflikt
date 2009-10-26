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

namespace KlimaKonflikt
{
    public class AIController
    {
        //public void CalculateAIMove(GameTime gameTime, GameBoard board, KKMonster monster)
        //{
        //    int pixelsToMove = (int)(monster.Speed * gameTime.ElapsedGameTime.Milliseconds * m_speedFactor);
        //    Point newPosition = monster.GetNewPosition(monster.Direction, pixelsToMove);
        //    Point oldPosition = monster.GetPosition();

        //    Point centerOfPlayersTile = board.GetTileFromPixelPosition(monster.GetPosition().X, monster.GetPosition().Y).Center;
        //    WalledTile tile = board.GetTileFromPixelPosition(monster.GetPosition());


        //    if (GeometryTools.IsBetweenPoints(centerOfPlayersTile, newPosition, oldPosition))
        //    {
        //        //we are going to cross the center
        //        //first move to center
        //        Point tempPosition = centerOfPlayersTile;
        //        monster.SetPosition(centerOfPlayersTile);

        //        int pixelMovesLeft = int.MinValue;
        //        DirectionChanger deltaMoves = DirectionHelper4.Offsets[monster.Direction];
        //        if (deltaMoves.DeltaX != 0) //we are moving horizontally
        //        {
        //            pixelMovesLeft = Math.Abs(newPosition.X - oldPosition.X);
        //        }
        //        else //we are moving vertically
        //        {
        //            pixelMovesLeft = Math.Abs(newPosition.Y - oldPosition.Y);
        //        }


        //        Direction wantedDirection = monster.WantedDirection(tile);
        //        Direction monsterDirection = monster.Direction;

        //        if (wantedDirection != Direction.None)
        //        {
        //            if (!tile.HasBorder(wantedDirection))
        //            {
        //                monster.Direction = wantedDirection;
        //                monster.Move(monster.Direction, pixelMovesLeft);
        //            }
        //            else
        //            {
        //                //monster.AllowDirectionChange();
        //                if (!tile.HasBorder(monsterDirection))
        //                {
        //                    monster.Move(monsterDirection, pixelMovesLeft);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        //monster.WantedDirection(tile);
        //        monster.X = newPosition.X;
        //        monster.Y = newPosition.Y;
        //    }
        //}

    }
}
