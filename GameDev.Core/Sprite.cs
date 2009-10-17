using System;
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
        public Direction Direction { get; set; }
        public float Speed { get; set; }
        public GameImage GameImage { get; set; }
        public SpriteBatch SpriteBatch { get; set; }

        // Lidt i tvivl om hvorledes Textture-sizen sættes til texturefilen's size
        public Sprite(Game game, GameImage gameImage, SpriteBatch spriteBatch, float speed, Point startingPosition) : this(game, gameImage, spriteBatch, speed, startingPosition.X, startingPosition.Y) { }
        public Sprite(Game game, GameImage gameImage, SpriteBatch spriteBatch, float speed) : this(game, gameImage, spriteBatch, speed, 0, 0) { }

        public Sprite(Game game, GameImage gameImage, SpriteBatch spriteBatch, float speed, int x, int y)
            : base(game, x, y)
        {
            this.GameImage = gameImage;
            this.Speed = speed;
            SpriteBatch = spriteBatch;

        }

        public override void Update(GameTime gameTime)
        {
            GameImage.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Texture2D currentTexture = GameImage.CurrentTexture;

            SpriteBatch.Draw(currentTexture, new Rectangle(X -  currentTexture.Width/2, Y - currentTexture.Height/2, currentTexture.Width, currentTexture.Height), Color.White);
            base.Draw(gameTime);
        }

    }
}
