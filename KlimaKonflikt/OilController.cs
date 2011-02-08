using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameDev.Core;
using Microsoft.Xna.Framework.Input;

namespace KlimaKonflikt
{
    /// <summary>
    /// Controller for the Oilbarrel. Joins together the Keyboard and the first gamepad.
    /// </summary>
    public class OilController : GameComponent
    {
        public OilController(Game game) : base(game) { }

        private KeyboardState m_CurrentKeyboardState = new KeyboardState();
        private GamePadState m_CurrentGamepadState = new GamePadState();

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_CurrentKeyboardState = Keyboard.GetState();
            m_CurrentGamepadState = GamePad.GetState(PlayerIndex.One);
            CalculateSpeedAndDirection();
        }

        private void CalculateSpeedAndDirection()
        {
            Vector2 gamepadLeft = m_CurrentGamepadState.ThumbSticks.Left;
            if (Math.Abs(gamepadLeft.X) <= 0.1f && Math.Abs(gamepadLeft.Y) <= 0.1f)
            {
                m_ChosenDirection = m_CurrentKeyboardState.GetDirectionFromWASDKeys();
                if (m_ChosenDirection == Direction.None)
                {
                    m_Speed = 0f;
                }
                else
                {
                    m_Speed = 1f;
                }
            }
            else if (Math.Abs(gamepadLeft.X) >= Math.Abs(gamepadLeft.Y))
            {
                m_Speed = Math.Abs(gamepadLeft.X);
                if (gamepadLeft.X < 0)
                {
                    m_ChosenDirection = Direction.Left;
                }
                else
                {
                    m_ChosenDirection = Direction.Right;
                }
            }
            else
            {
                m_Speed = Math.Abs(gamepadLeft.Y);
                if (gamepadLeft.Y < 0)
                {
                    m_ChosenDirection = Direction.Up;
                }
                else
                {
                    m_ChosenDirection = Direction.Down;
                }
            }
        }

        private Direction m_ChosenDirection;
        public Direction ChosenDirection
        {
            get
            {
                return m_ChosenDirection;
            }
        }

        private float m_Speed;
        public float ChosenSpeed
        {
            get
            {
                return m_Speed;
            }
        }


    }
}
