using GameDev.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDev.Core.Menus
{
    public class CheckBoxMenuItem : TextMenuItem
    {

        public bool Checked { get; set; }
        public GameImage CheckBoxBackground { get; set; }
        public GameImage CheckMark { get; set; }
        private Vector2 m_textSize, m_textPosition;
        private Rectangle m_checkboxRectangle;

        public CheckBoxMenuItem(string name, SpriteFont font, string text) : this(name, font, text, Vector2.Zero, Color.White, Color.Gray, false, true) { }


        public CheckBoxMenuItem(string name, SpriteFont font, string text, Vector2 position, Color activeColor, Color inactiveColor, bool isChecked, bool centered)
            : base(name, font, text, position, activeColor, inactiveColor, centered)
        {
            Checked = isChecked;
            NeedsPositionRecalculation = true;
        }

        public override void Draw(GameTime gameTime)
        {
            if (CheckBoxBackground == null)
            {
                CheckBoxBackground = BaseTextures.Box_128x128;
            }
            if (CheckMark == null)
            {
                CheckMark = BaseTextures.CheckMark_128x128;
            }

            GameDevGame.Current.SpriteBatch.Draw(CheckBoxBackground, m_checkboxRectangle, CurrentColor);

            if (Checked)
            {
                GameDevGame.Current.SpriteBatch.Draw(CheckMark, m_checkboxRectangle, CurrentColor);
            }


            GameDevGame.Current.SpriteBatch.DrawString(Font, Text, m_textPosition, CurrentColor);

        }

        protected override void RecalculatePosition()
        {
            float checkBoxToTextRatio =  .6F;
            float checkboxTopMarginRatio = .2F;
            int widthOfCheckBox = 0;
            int space = 10;
            
            int x = (int)Position.X;
            int y = (int)Position.Y;
            
            m_textSize = Font.MeasureString(this.Text);
            int checkboxSize = (int)(m_textSize.Y * checkBoxToTextRatio);
            widthOfCheckBox = checkboxSize;

            if (Centered)
            {
                x = (int)(GameDevGame.Current.GraphicsDevice.Viewport.GetCenter().X - (checkboxSize + space + m_textSize.X)/ 2);
            }
            this.Position = new Vector2(x, y);

            m_checkboxRectangle = new Rectangle((int)Position.X, (int)(Position.Y + checkboxTopMarginRatio * m_textSize.Y), checkboxSize,checkboxSize);
            
            
            m_textPosition = new Vector2(Position.X + checkboxSize + space, Position.Y);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsSelected && IsReadyForKeyboardInteraction)
            {
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Keys.Space) || state.IsKeyDown(Keys.Enter))
                {
                    this.Checked = !this.Checked;
                    if (Checked)
                    {
                        OnActivate();
                    }
                    ResetKeyboardIntervalTimer();
                }
            }
        }

    }
}
