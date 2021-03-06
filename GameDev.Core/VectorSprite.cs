﻿using GameDev.Core.Graphics;
#if SILVERLIGHT
using SilverArcade.SilverSprite;
using SilverArcade.SilverSprite.Graphics;
#else
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

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
           RecalculateOrigin();
            }
        }
        private Vector2 m_scale = Vector2.One;

        public Vector2 Scale { get { return m_scale; }
            set {
                m_scale = value;

            }
        }
        public static implicit operator VectorSprite(GameImage image) 
   {
      return new VectorSprite(image);
   }
        private void RecalculateOrigin()
        {
            m_origin = GameImage.CurrentTexture.GetSize() * Scale /2;
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
