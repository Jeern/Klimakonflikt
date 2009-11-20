using GameDev.Core;
using GameDev.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace KlimaKonflikt
{
    public class KKPlayer : Sprite
    {

        public int RoundsWon { get; set; }
        public int EjedeFelter { get; set; }
        protected int m_startingAmmo;
        public SoundEffect TakeoverTileSound { get; set; }
        public SoundEffect RefuelSound { get; set; }
        public float MaxHealth { get; set; }
        public Ejerskab EjerskabsType { get; set; }

        public KKPlayer(GameImage gameImage, float speed, Point startingPosition, int ammo, float maxHealth, SoundEffect takeTileSound, SoundEffect refuelSound, Ejerskab ejerskabsType):base(gameImage, speed, startingPosition)
        {
            m_startingAmmo = ammo;
            this.RefuelSound = refuelSound;
            this.TakeoverTileSound = takeTileSound;
            this.EjerskabsType = ejerskabsType;
            this.MaxHealth = maxHealth;
            Reset();
        }

        private float m_health;
        public float Health { get{return m_health;}
            set
            {
                if (value > MaxHealth)
                {
                    m_health = MaxHealth;
                }
                else
                {
                    m_health = value;
                }
            }
        }

        public int Ammunition { get; set; }

        public override void Reset()
        {
            base.Reset();
            this.Ammunition = m_startingAmmo;
            this.Health = MaxHealth;
            this.EjedeFelter = 0;
            
        }

        public void ResetGame()
        {
            this.RoundsWon = 0;
        }
        

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Color colorEffect = Color.White;
            Texture2D currentTexture = null;
            if (Ammunition == 0)
            {
                colorEffect = Color.Red;
            }
             currentTexture = GameImage.CurrentTexture;
            GameDevGame.Current.SpriteBatch.Draw(currentTexture, new Rectangle(X - currentTexture.Width / 2 + GameImageOffset.X, Y - currentTexture.Height / 2 + GameImageOffset.Y, currentTexture.Width, currentTexture.Height), colorEffect);


        }

    }
}
