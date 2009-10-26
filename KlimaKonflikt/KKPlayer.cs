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



namespace KlimaKonflikt
{
    public class KKPlayer : Sprite
    {
        public Direction WantedDirection { get; set; }
        public int EjedeFelter { get; set; }
        protected Point m_startingPosition;
        protected int m_startingAmmo;

        public GameImage OutOfAmmoEffect { get; set; }

        public KKPlayer(GameImage gameImage, float speed, Point startingPosition, int ammo, GameImage outOfAmmoEffect):base(gameImage, speed, startingPosition)
        {
            m_startingPosition = startingPosition;
            m_startingAmmo = ammo;
            this.OutOfAmmoEffect = outOfAmmoEffect;
            Reset();
        }

        public float Health { get; set; }

        public int Ammunition { get; set; }

        public void Reset()
        {
            this.Ammunition = m_startingAmmo;
            this.Health = 100;
            this.SetPosition(m_startingPosition);
            this.WantedDirection = Direction.None;
            this.EjedeFelter = 0;
        }
        public override void Update(GameTime gameTime)
        {
            OutOfAmmoEffect.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Color colorEffect = Color.White;
            Texture2D currentTexture = null;
            if (Ammunition == 0)
            {
                //currentTexture = OutOfAmmoEffect.CurrentTexture;
                //GameDevGame.Current.SpriteBatch.Draw(currentTexture, new Rectangle(X - currentTexture.Width / 2 + GameImageOffset.X, Y - currentTexture.Height / 2 + GameImageOffset.Y, currentTexture.Width, currentTexture.Height), Color.White);
                colorEffect = Color.Red;
            }
             currentTexture = GameImage.CurrentTexture;
            GameDevGame.Current.SpriteBatch.Draw(currentTexture, new Rectangle(X - currentTexture.Width / 2 + GameImageOffset.X, Y - currentTexture.Height / 2 + GameImageOffset.Y, currentTexture.Width, currentTexture.Height), colorEffect);


            //base.Draw(gameTime);
        }

    }
}
