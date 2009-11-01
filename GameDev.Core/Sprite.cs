﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace GameDev.Core
{
    //mangler Image. 

    public class Sprite : Placeable
    {
        public Point OriginalPosition { get; private set; }
        public Direction Direction { get; set; }
        public Direction WantedDirection { get; set; }
        public float Speed { get; set; }
        public Point GameImageOffset { get; set; }
        public GameImage GameImage { get; set; }

        // Lidt i tvivl om hvorledes Texture-sizen sættes til texturefilen's size
        public Sprite(GameImage gameImage, float speed, Point startingPosition) : this(gameImage, speed, startingPosition.X, startingPosition.Y) { }
        public Sprite(GameImage gameImage, float speed) : this(gameImage, speed, 0, 0) { }

        public Sprite(GameImage gameImage, float speed, int x, int y)
            : base(x, y)
        {
            this.GameImage = gameImage;
            this.Speed = speed;
            this.OriginalPosition = new Point(x, y);

        }

        public override void Update(GameTime gameTime)
        {
            GameImage.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Texture2D currentTexture = GameImage.CurrentTexture;
            Rectangle position = new Rectangle(X - currentTexture.Width / 2 + GameImageOffset.X, Y - currentTexture.Height / 2 + GameImageOffset.Y, currentTexture.Width, currentTexture.Height);
            GameDevGame.Current.SpriteBatch.Draw(currentTexture, position, Color.White);
            
            //Vector2 textPos = new Vector2(position.Left + 30, position.Top + 30);
            //GameDevGame.Current.SpriteBatch.DrawString(GameDevGame.Current.DebugFont, "want: " + WantedDirection ,textPos , Color.White);
            //textPos.Y += 20;
            //GameDevGame.Current.SpriteBatch.DrawString(GameDevGame.Current.DebugFont, "dir: " + Direction, textPos, Color.White);
            base.Draw(gameTime);
        }

        public virtual void Reset()
        {
            this.WantedDirection = Direction.None;
            this.Direction = Direction.None;
            this.SetPosition(this.OriginalPosition);
        }
    }
}
