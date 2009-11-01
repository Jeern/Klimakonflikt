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

    public class KeyboardController : GameBoardControllerBase
    {
        public enum KeySet
        {
            Custom = 0,
            ArrowKeys = 1,
            WASD = 2,
        }

        public KeySet KeySetUsed { get; private set; }

        public Keys UpKey { get; private set; }
        public Keys RightKey { get; private set; }
        public Keys DownKey { get; private set; }
        public Keys LeftKey { get; private set; }

        protected Direction wantedDirection;

        public KeyboardController(GameBoard board, KeySet keysToUse) : base(board)
        {
            switch (keysToUse)
            {
                case KeySet.ArrowKeys:
                    UpKey = Keys.Up;
                    RightKey = Keys.Right;
                    DownKey = Keys.Down;
                    LeftKey = Keys.Left;
                    break;
                case KeySet.WASD:
                    UpKey = Keys.W;
                    RightKey = Keys.D;
                    DownKey = Keys.S;
                    LeftKey = Keys.A;
                    break;

            }

        }

        public KeyboardController(GameBoard board) : this(board, Keys.Up, Keys.Right, Keys.Down, Keys.Left){}

        public KeyboardController(GameBoard board, Keys upKey, Keys rightKey, Keys downKey, Keys leftKey) : base (board)
        {
            UpKey = upKey;
            RightKey = rightKey;
            DownKey = downKey;
            LeftKey = leftKey;
        }


        public override void Update(GameTime gameTime, Sprite unitToUpdate)
        {
            //this override is done to read keyboard more often than
            ReadKeyboard();
            SetWantedDirection(unitToUpdate);
            if (unitToUpdate.WantedDirection == DirectionHelper4.GetOppositeDirection(unitToUpdate.Direction))
            {
                unitToUpdate.Direction = unitToUpdate.WantedDirection;
            }
            base.Update(gameTime, unitToUpdate);
        }
      
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
            else
            {
                controllee.Direction = Direction.None;
            }
        }
        protected void ReadKeyboard()
        {
            KeyboardState state = Keyboard.GetState();
            Direction keyboardDirection = Direction.None;

            switch (KeySetUsed)
            {
                case KeySet.Custom:
                    if (state.IsKeyDown(LeftKey))
                    {
                        keyboardDirection = Direction.Left;
                    }
                    if (state.IsKeyDown(RightKey))
                    {
                        keyboardDirection = Direction.Right;
                    }
                    if (state.IsKeyDown(DownKey))
                    {
                        keyboardDirection = Direction.Down;
                    }
                    if (state.IsKeyDown(UpKey))
                    {
                        keyboardDirection = Direction.Up;
                    }

                    break;
                case KeySet.ArrowKeys:
                    keyboardDirection = state.GetDirectionFromArrowKeys();
                    break;
                case KeySet.WASD:
                    keyboardDirection = state.GetDirectionFromWASDKeys();
                    break;
            }


                if (keyboardDirection != Direction.None)
                {
                    wantedDirection = keyboardDirection;
                }
        }

        override protected void SetWantedDirection(Sprite controllee)
        {
            controllee.WantedDirection = wantedDirection;
        }

    }
}
