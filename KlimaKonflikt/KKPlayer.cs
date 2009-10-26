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

        public KKPlayer(GameImage gameImage, float speed, Point startingPosition, int ammo ):base(gameImage, speed, startingPosition)
        {
            m_startingPosition = startingPosition;
            m_startingAmmo = ammo;
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
    }
}
