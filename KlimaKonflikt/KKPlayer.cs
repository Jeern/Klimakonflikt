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
        public int EjedeFelter { get; set; }
        protected int m_startingAmmo;
        public SoundEffect TakeoverTileSound { get; set; }
        public SoundEffect RefuelSound { get; set; }

        public Ejerskab EjerskabsType { get; set; }
        public GameImage OutOfAmmoEffect { get; set; }

        public KKPlayer(GameImage gameImage, float speed, Point startingPosition, int ammo, GameImage outOfAmmoEffect, SoundEffect takeTileSound, SoundEffect refuelSound, Ejerskab ejerskabsType):base(gameImage, speed, startingPosition)
        {
            m_startingAmmo = ammo;
            this.OutOfAmmoEffect = outOfAmmoEffect;
            this.RefuelSound = refuelSound;
            this.TakeoverTileSound = takeTileSound;
            this.EjerskabsType = ejerskabsType;
            Reset();
        }

        public float Health { get; set; }

        public int Ammunition { get; set; }

        public override void Reset()
        {
            base.Reset();
            this.Ammunition = m_startingAmmo;
            this.Health = 100;
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

        }

    }
}
