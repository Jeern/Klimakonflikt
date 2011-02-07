using System;
using System.Collections.Generic;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
using SilverArcade.SilverSprite.Input;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endif

namespace GameDev.Core.Menus
{
    public class IntervalMenuItem : TextMenuItem
    {
        public int MaxValue { get; private set; }
        public int MinValue { get; private set; }
        public int TickInterval { get; private set; }

        private int m_currentValue;
        public int CurrentValue
        {
            get { return m_currentValue; }
            set
            {
                if (value >= MinValue && value <= MaxValue)
                {
                    m_currentValue = value;
                }
                else
                {
                    throw new Exception("CurrentValue must be between MinValue and MaxValue! Trying to set CurrentValue=" + value + " while MinValue=" + MinValue + " and MaxValue=" + MaxValue);
                }
            }
        }

        public void TickUp()
        {
            if (CurrentValue < MaxValue)
            {
                CurrentValue+= TickInterval;
            }
        }

        public void TickDown()
        {
            if (CurrentValue > MinValue)
            {
                CurrentValue -= TickInterval;
            }
        }


        private List<Rectangle> m_tickMarks = new List<Rectangle>();
        Vector2 m_textSize;

        public IntervalMenuItem(string name, SpriteFont font, string text, int maxValue, int minValue, int tickInterval) : this(name, font, text, Vector2.Zero, Color.White, Color.Gray, true, maxValue, minValue, tickInterval)
        {

        }

        
        public IntervalMenuItem(string name, SpriteFont font, string text, Vector2 position, Color activeColor, Color inactiveColor, bool centered, int maxValue, int minValue, int tickInterval)
            : base(name,font, text,position, activeColor, inactiveColor,centered)
        {

            if (minValue >= maxValue)
            {
                throw new ArgumentException("minValue must be smaller than maxValue");
            }
            MaxValue = maxValue;
            MinValue = minValue;
            CurrentValue = minValue;
            TickInterval = tickInterval;
            NeedsPositionRecalculation = true;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GameDevGame.Current.SpriteBatch.DrawString(Font, Text + " < " + CurrentValue.ToString() + " >", Position, CurrentColor);
            //GameDevGame.Current.SpriteBatch.DrawRectangle(m_sliderBar, Color.Gray);
            //foreach (Rectangle  rect in m_tickMarks)
            //{
            //    GameDevGame.Current.SpriteBatch.DrawRectangle(rect, Color.Gray);
            //}
            //GameDevGame.Current.SpriteBatch.DrawRectangle(m_tickMarks[], Color.Gray);

        }

        protected override void RecalculatePosition()
        {
            if (Centered)
            {
                m_textSize = Font.MeasureString(this.Text);
                int x = (int)(GameDevGame.Current.GraphicsDevice.Viewport.Width - m_textSize.X) / 2;
                int y = (int)Position.Y;
                this.Position = new Vector2(x, y);
            }

            //int sliderRange = MaxValue - MinValue;
            //int numberOfTicks = sliderRange / TickInterval;
            
            //int pixelsBetweenTicks =(int)( m_textSize.X / numberOfTicks);
            //m_tickMarks.Clear();
            //for (int i = 0; i <= numberOfTicks; i++)
            //{
            //    m_tickMarks.Add(new Rectangle(i * pixelsBetweenTicks + (int)Position.X, (int)(Position.Y + m_textSize.Y), 3, 15));
            //}
            //m_sliderBar = new Rectangle((int)Position.X, (int)(Position.Y + m_textSize.Y) +8,(int) m_textSize.X, 3);


        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsSelected && IsReadyForKeyboardInteraction)
            {
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.Subtract) || state.IsKeyDown(Keys.OemMinus))
                {
                    TickDown();
                    ResetKeyboardIntervalTimer();
                }
                else
                {
                    if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.Add) || state.IsKeyDown(Keys.OemPlus))
                    {
                        TickUp();
                        ResetKeyboardIntervalTimer();
                    }

                }
            }
        }
 
    }
}
