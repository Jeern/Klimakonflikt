#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

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
using GameDev.Core.Graphics;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;
#endregion

namespace GameDev.Core.Menus
{
    public class TextMenuItem : MenuItem
    {
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
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

        private bool m_centered = true;
        public bool Centered
        {
            get 
            {
                return m_centered; 
            }
            set 
            {
                m_centered = value;
            RecalculatePosition();
            }
        }

        public TextMenuItem(string name, SpriteFont font, string text) : this(name, font, text, new Vector2(), Color.White, Color.Gray)
        {}

        public TextMenuItem(string name, SpriteFont font, string text, Vector2 position, Color activeColor, Color inactiveColor)
            : base(name, position)
        {
            this.Font = font;
            this.Text = text;
            this.ActiveColor = activeColor;
            this.InactiveColor = inactiveColor;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GameDevGame.Current.SpriteBatch.DrawString(Font, Text, Position, CurrentColor);
        }

        protected void RecalculatePosition()
        {
            if (Centered)
            {
                Vector2 fontSize = Font.MeasureString(this.Text);
                int x  =(int) (fontSize.X + GameDevGame.Current.GraphicsDevice.Viewport.Width) / 2;
                int y = (int)Position.Y;
                this.Position = new Vector2(x, y);
            }
        }
    }
}
