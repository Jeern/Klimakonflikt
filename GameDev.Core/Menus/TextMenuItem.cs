#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

namespace GameDev.Core.Menus
{
    public class TextMenuItem : MenuItem
    {
        private SpriteFont m_font;
        public SpriteFont Font {
            get { return m_font; }
            set
            {
                m_font = value;
                NeedsPositionRecalculation = true;
            }
        }
        private string m_text;
        public string Text {

            get { return m_text; }

            set {
                m_text = value;
                NeedsPositionRecalculation = true;
            }
        }
        public Color ActiveColor { get; set; }
        public Color InactiveColor { get; set; }
        public Color CurrentColor { get {
            if (this.IsSelected)
            {
                return this.ActiveColor;
            }
            else
            { return this.InactiveColor; }

        } }

        public TextMenuItem(string name, SpriteFont font)
            : this(name, font, name, true)
        { }


        public TextMenuItem(string name, SpriteFont font, string text)
            : this(name, font, text, true)
        { }


        public TextMenuItem(string name, SpriteFont font, string text, bool centered) : this(name, font, text, new Vector2(), Color.White, Color.Gray, centered)
        {}

        public TextMenuItem(string name, SpriteFont font, string text, Vector2 position, Color activeColor, Color inactiveColor, bool centered)
            : base(name, position, centered)
        {
            this.Font = font;
            this.Text = text;
            this.ActiveColor = activeColor;
            this.InactiveColor = inactiveColor;
            NeedsPositionRecalculation = true;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GameDevGame.Current.SpriteBatch.DrawString(Font, Text, Position, CurrentColor);
        }

        protected override void RecalculatePosition()
        {
            if (Centered)
            {
                Vector2 fontSize = Font.MeasureString(this.Text);
                int x = (int)(GameDevGame.Current.GraphicsDevice.Viewport.Width - fontSize.X) / 2;
                int y = (int)Position.Y;
                this.Position = new Vector2(x, y);
            }
            NeedsPositionRecalculation = false;

        }

 
    }
}
