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
using GameDev.Core.Menus;
using GameDev.Core.SceneManagement;
using GameDev.Core.Sequencing;
using GameDev.Core.Particles;

#endregion

namespace GameDev.Core
{
    public class VectorSprite : DrawableGameComponent
    {

        private Vector2 m_origin;
        public Vector2  Position { get; set; }
        public Vector2 Speed { get; set; }
        public float Rotation { get; set; }
        private GameImage m_gameImage;
        public GameImage GameImage {
            get { return m_gameImage; }
            set { m_gameImage = value;
            m_origin = new Vector2(GameImage.CurrentTexture.Width / 2, GameImage.CurrentTexture.Height / 2);
            }
        }
        public Vector2 Scale { get; set; }
        public static implicit operator VectorSprite(GameImage image) 
   {
      return new VectorSprite(image);
   }



        public VectorSprite(Vector2 location, Vector2 speed, GameImage gameImage) : base(GameDevGame.Current)
        {
            this.Position = location;
            this.Speed = speed;
            this.GameImage = gameImage;
            this.Scale = Vector2.One;
        }

        public VectorSprite(GameImage gameImage)
            : this(Vector2.Zero, Vector2.Zero,gameImage)
        {

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Position += (this.Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            this.GameImage.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GameDevGame.Current.SpriteBatch.Draw(GameImage.CurrentTexture, Position, null, Color.White,
                    Rotation, m_origin, Scale, SpriteEffects.None, 0.0f);
        }

    }
}
