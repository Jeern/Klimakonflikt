#region Usings
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion


namespace GameDev.Core.Graphics
{
    public class GraphicTimer : DrawableGameComponent
    {
        private event EventHandler<EventArgs> m_TimesUp = delegate { };

        public event EventHandler<EventArgs> TimesUp
        {
            add { m_TimesUp += value; }
            remove { m_TimesUp -= value; }
        }

        public bool SimpleShadowBackground { get; set; }
        public bool Centered { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public SpriteFont Font { get; set; }
        private int m_secondsToCountDown, m_originalSecondsToCountDown;
        private string m_secondsLeftString = string.Empty;
        private Vector2 m_topLeftOfBackgroundCalculated, m_topLeftOfTextCalculated;
        private static Color m_baseShadowColor = new Color(0, 0, 0, 200);

        public int SecondsToCountDown
        {
            get { return m_secondsToCountDown; }
            set
            {
                m_secondsToCountDown = value;
                m_originalSecondsToCountDown = SecondsToCountDown;
                m_millisecondsLeft = SecondsToCountDown * 1000;
            }
        }
        public Color Color { get; set; }
        public Color BackgroundColor { get; set; }
        private DateTime m_beginTime;
        private int m_millisecondsLeft;
        private Vector2 m_stringSize;
        protected Rectangle m_drawRectangle;

        public int SecondsLeft
        {
            get { return (int)(m_millisecondsLeft + 999) / 1000; }
        }

public GraphicTimer(SpriteFont font, int secondsToCountDown, Vector2 size)
            : this(font, secondsToCountDown)
{
    this.Centered = true;
    this.Size = size;
}


        public GraphicTimer(SpriteFont font, int secondsToCountDown, Vector2 position, Vector2 size, Color color, Color backgroundColor)
            : base(GameDevGame.Current)
        {
            Font = font;
            SecondsToCountDown = secondsToCountDown;
            Position = position;
            Size = size;
            Color = color;
            BackgroundColor = backgroundColor;
        }

        public GraphicTimer(SpriteFont font, int secondsToCountDown)
            : this(font, secondsToCountDown, Vector2.Zero, Vector2.Zero, Color.White, m_baseShadowColor)
        {
            Centered = true;
            SimpleShadowBackground = true;
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            m_millisecondsLeft -= (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            m_secondsLeftString = SecondsLeft.ToString();
            if (m_millisecondsLeft <= 0)
            {
                OnTimesUp();
            }
        }

        protected void CalculateDrawRectangle()
        {

            m_stringSize = Font.MeasureString(m_secondsLeftString);
            if (Size == Vector2.Zero)
            {
                Size = m_stringSize * 1.2F;
            }

            if (Centered)
            {
                m_topLeftOfBackgroundCalculated = new Vector2((GameDevGame.Current.GraphicsDevice.Viewport.Width - Size.X) / 2, (GameDevGame.Current.GraphicsDevice.Viewport.Height - Size.Y) / 2);
            }
            else
            {
                m_topLeftOfBackgroundCalculated = Position;
            }
            m_topLeftOfTextCalculated = m_topLeftOfBackgroundCalculated + (Size - m_stringSize) / 2;

            m_drawRectangle = new Rectangle((int)m_topLeftOfBackgroundCalculated.X, (int)m_topLeftOfBackgroundCalculated.Y, (int)Size.X, (int)Size.Y);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            CalculateDrawRectangle();
            DrawBackground(gameTime);
            DrawForeground(gameTime);
        }

        protected virtual void DrawForeground(GameTime gameTime)
        {
            GameDevGame.Current.SpriteBatch.DrawString(Font, m_secondsLeftString, m_topLeftOfTextCalculated, Color);
        }

        protected virtual void DrawBackground(GameTime gameTime)
        {
            if (SimpleShadowBackground)
            {
                GameDevGame.Current.SpriteBatch.DrawString(Font, m_secondsLeftString, m_topLeftOfTextCalculated + Vector2.One * 4, BackgroundColor);
            }
            else
            {
                GameDevGame.Current.SpriteBatch.Draw(BaseTextures.Square_128x128, m_drawRectangle, BackgroundColor);
            }
        }

        public void Reset()
        {
            m_secondsToCountDown = m_originalSecondsToCountDown;
            this.Visible = true;
            this.Enabled = true;
        }

        public void Begin()
        {
            m_beginTime = DateTime.Now;
        }

        protected void OnTimesUp()
        {
            m_TimesUp(this, EventArgs.Empty);
            this.Visible = false;
            this.Enabled = false;
        }
    }
}
